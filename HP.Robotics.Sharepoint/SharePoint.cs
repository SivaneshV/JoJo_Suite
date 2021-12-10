using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.IO;
using System.Text.RegularExpressions;

namespace HP.Robotics.Sharepoint
{
    public class SharePoint
    {

        public void UploadToSharePoint(string FilePath, string SiteUrl, string FolderName, string UserName, string Password, bool OverWrite, out string newUrl)  //p is path to file to load
        {
            try
            {
                string siteUrl = SiteUrl;
                //Insert Credentials
                ClientContext context = new ClientContext(siteUrl);
                using (ClientContext clientcontext = new AuthenticationManager().GetAppOnlyAuthenticatedContext(URL, clientId, secretId))
                {
                }
                    SecureString passWord = new SecureString();
                foreach (var c in Password) passWord.AppendChar(c);
                context.Credentials = new SharePointOnlineCredentials(UserName, passWord);


                Web site = context.Web;
                //Get the required RootFolder
                string barRootFolderRelativeUrl = FolderName;
                Folder barFolder = site.GetFolderByServerRelativeUrl(barRootFolderRelativeUrl);
                if (context.HasPendingRequest)
                    context.ExecuteQuery();

                using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(FilePath)))
                {
                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(context, barRootFolderRelativeUrl + Path.GetFileName(FilePath), stream, OverWrite);
                }
                Uri myUri = new Uri(siteUrl);
                string host = myUri.Host;
                var http = siteUrl.Split('/')[0];
                newUrl = http + "//" + host + barRootFolderRelativeUrl + Path.GetFileName(@FilePath);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateFolder(string FolderName, string SiteUrl, string RootFolder, string UserName, string Password, out string newUrl)  //p is path to file to load
        {
            try
            {
                string siteUrl = SiteUrl;
                //Insert Credentials
                ClientContext context = new ClientContext(siteUrl);

                SecureString passWord = new SecureString();
                foreach (var c in Password) passWord.AppendChar(c);
                context.Credentials = new SharePointOnlineCredentials(UserName, passWord);
                Web site = context.Web;

                //Get the required RootFolder
                string barRootFolderRelativeUrl = RootFolder;
                Folder barFolder = site.GetFolderByServerRelativeUrl(barRootFolderRelativeUrl);
                if (context.HasPendingRequest)
                    context.ExecuteQuery();
                //Add folder to Root Folder
                Folder currentRunFolder = site.GetFolderByServerRelativeUrl(barRootFolderRelativeUrl);
                currentRunFolder.Folders.Add(FolderName);
                currentRunFolder.Update();

                context.ExecuteQuery();

                //Return the URL of the new uploaded file
                newUrl = barRootFolderRelativeUrl + FolderName;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Get file information from Sharepoint.
        /// </summary>
        /// <param name="documentUrl">Document Url.</param>
        /// <returns>Returns file information object.</returns>
        public void DownloadFile(string DownloadLocation, string SiteUrl, string FolderPath, string FileName, string UserName, string Password, string Type)
        {
            using (ClientContext clientContext = GetContextObject(SiteUrl, UserName, Password))
            {

                Web web = clientContext.Web;
                clientContext.Load(web, website => website.ServerRelativeUrl);
                clientContext.ExecuteQuery();
                Regex regex = new Regex(SiteUrl, RegexOptions.IgnoreCase);
                string strSiteRelavtiveURL = regex.Replace(FolderPath, string.Empty);
                string strServerRelativeURL = CombineUrl(web.ServerRelativeUrl, strSiteRelavtiveURL);

                if (Type == "Specific")
                {
                    Microsoft.SharePoint.Client.Folder oFolder = web.GetFolderByServerRelativeUrl(strServerRelativeURL);
                    clientContext.Load(oFolder);
                    clientContext.Load(oFolder.Files);
                    clientContext.ExecuteQuery();
                    var File = oFolder.Files.Where(x => Path.GetFileNameWithoutExtension(x.Name).ToLower() == FileName.ToLower()).FirstOrDefault();
                    ClientResult<Stream> stream = File.OpenBinaryStream();
                    clientContext.ExecuteQuery();

                    var filestrem = this.ReadFully(stream.Value);
                    string fileName = System.IO.Path.GetFileName(File.Name);
                    string filepath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(DownloadLocation), fileName);

                    using (FileStream fileStream = System.IO.File.Create(filepath, (int)filestrem.Length))
                    {
                        // Initialize the bytes array with the stream length and then fill it with data
                        byte[] bytesInStream = new byte[filestrem.Length];
                        filestrem.Read(bytesInStream, 0, bytesInStream.Length);
                        // Use write method to write to the file specified above
                        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    }


                }
                else if (Type == "SpecificWithExtension")
                {
                    Microsoft.SharePoint.Client.Folder oFolder = web.GetFolderByServerRelativeUrl(strServerRelativeURL);
                    clientContext.Load(oFolder);
                    clientContext.Load(oFolder.Files);
                    clientContext.ExecuteQuery();
                    var File = oFolder.Files.Where(x => x.Name.ToLower() == FileName.ToLower()).FirstOrDefault();
                    ClientResult<Stream> stream = File.OpenBinaryStream();
                    clientContext.ExecuteQuery();

                    var filestrem = this.ReadFully(stream.Value);
                    string fileName = System.IO.Path.GetFileName(File.Name);
                    string filepath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(DownloadLocation), fileName);

                    using (FileStream fileStream = System.IO.File.Create(filepath, (int)filestrem.Length))
                    {
                        // Initialize the bytes array with the stream length and then fill it with data
                        byte[] bytesInStream = new byte[filestrem.Length];
                        filestrem.Read(bytesInStream, 0, bytesInStream.Length);
                        // Use write method to write to the file specified above
                        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    }


                }
                else if (Type == "Latest")
                {
                    Microsoft.SharePoint.Client.Folder oFolder = web.GetFolderByServerRelativeUrl(strServerRelativeURL);
                    clientContext.Load(oFolder);
                    clientContext.Load(oFolder.Files);
                    clientContext.ExecuteQuery();
                    var File = oFolder.Files.OrderByDescending(x => x.TimeCreated).FirstOrDefault();
                    ClientResult<Stream> stream = File.OpenBinaryStream();
                    clientContext.ExecuteQuery();

                    var filestrem = this.ReadFully(stream.Value);
                    string fileName = System.IO.Path.GetFileName(File.Name);
                    string filepath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(DownloadLocation), fileName);

                    using (FileStream fileStream = System.IO.File.Create(filepath, (int)filestrem.Length))
                    {
                        // Initialize the bytes array with the stream length and then fill it with data
                        byte[] bytesInStream = new byte[filestrem.Length];
                        filestrem.Read(bytesInStream, 0, bytesInStream.Length);
                        // Use write method to write to the file specified above
                        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    }
                }
                else if (Type == "Contains")
                {
                    Microsoft.SharePoint.Client.Folder oFolder = web.GetFolderByServerRelativeUrl(strServerRelativeURL);
                    clientContext.Load(oFolder);
                    clientContext.Load(oFolder.Files);
                    clientContext.ExecuteQuery();
                    var Files = oFolder.Files.ToList().Where(x => Path.GetFileName(x.Name).ToLower().Contains(FileName.ToLower())).ToList();
                    clientContext.ExecuteQuery();
                    foreach (var File in Files)
                    {
                        ClientResult<Stream> stream = File.OpenBinaryStream();
                        clientContext.ExecuteQuery();

                        var filestrem = this.ReadFully(stream.Value);
                        string fileName = System.IO.Path.GetFileName(File.Name);
                        string filepath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(DownloadLocation), fileName);

                        using (FileStream fileStream = System.IO.File.Create(filepath, (int)filestrem.Length))
                        {
                            // Initialize the bytes array with the stream length and then fill it with data
                            byte[] bytesInStream = new byte[filestrem.Length];
                            filestrem.Read(bytesInStream, 0, bytesInStream.Length);
                            // Use write method to write to the file specified above
                            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                        }

                    }
                }
                else if (Type == "All")
                {

                    Microsoft.SharePoint.Client.Folder oFolder = web.GetFolderByServerRelativeUrl(strServerRelativeURL);
                    clientContext.Load(oFolder);
                    clientContext.Load(oFolder.Files);
                    clientContext.ExecuteQuery();
                    foreach (var File in oFolder.Files)
                    {
                        ClientResult<Stream> stream = File.OpenBinaryStream();
                        clientContext.ExecuteQuery();

                        var filestrem = this.ReadFully(stream.Value);
                        string fileName = System.IO.Path.GetFileName(File.Name);
                        string filepath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(DownloadLocation), fileName);

                        using (FileStream fileStream = System.IO.File.Create(filepath, (int)filestrem.Length))
                        {
                            // Initialize the bytes array with the stream length and then fill it with data
                            byte[] bytesInStream = new byte[filestrem.Length];
                            filestrem.Read(bytesInStream, 0, bytesInStream.Length);
                            // Use write method to write to the file specified above
                            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                        }

                    }

                }

            }
        }

        private ClientContext GetContextObject(string SiteUrl, string UserName, string Password)
        {
            ClientContext context = new ClientContext(SiteUrl);
            context.Credentials = new SharePointOnlineCredentials(UserName, GetPasswordFromConsoleInput(Password));
            return context;
        }

        private static SecureString GetPasswordFromConsoleInput(string password)
        {
            //Get the user's password as a SecureString
            SecureString securePassword = new SecureString();
            char[] arrPassword = password.ToCharArray();
            foreach (char c in arrPassword)
            {
                securePassword.AppendChar(c);
            }

            return securePassword;
        }

        public string CombineUrl(string path1, string path2)
        {

            return path1.TrimEnd('/') + '/' + path2.TrimStart('/');
        }

        private Stream ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return new MemoryStream(ms.ToArray()); ;
            }
        }
    }
}
