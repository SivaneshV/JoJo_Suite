using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.IO;
using System.Text.RegularExpressions;
using OfficeDevPnP.Core;
using System.Net.Security;
using System.Data;

namespace HP.Robotics.Sharepoint
{
    public class SharePoint
    {
        public SharePoint()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });
        }

        public void UploadDatasetToList(DataSet dataset, string siteUrl, string listName, string username, string password, bool isDeleteExisting)
        {
            if (dataset != null)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        QueryJsonEntity queryJsonEntity = new QueryJsonEntity();
                        List<Sharepoint> sharepoints = new List<Sharepoint>();
                        Insertquery insertquery = new Insertquery();
                        List<RecordItem> recordItems = new List<RecordItem>();

                        foreach (DataRow item in dataset.Tables[0].Rows)
                        {
                            List<Set> sets = new List<Set>();
                            foreach (DataColumn col in dataset.Tables[0].Columns)
                            {
                                sets.Add(new Set
                                {
                                    FieldName = col.ColumnName,
                                    Value = item[col.ColumnName].ToString()
                                });
                            }
                            recordItems.Add(new RecordItem
                            {
                                Set = sets.ToArray()
                            });
                        }

                        insertquery.RecordItem = recordItems.ToArray();

                        sharepoints.Add(new Sharepoint
                        {
                            URL = siteUrl,
                            ListTitle = listName,
                            Username = username,
                            Passwd = password,
                            InsertQuery = insertquery
                        });
                        queryJsonEntity.Sharepoint = sharepoints.ToArray();


                        if (isDeleteExisting)
                        {
                            try
                            {
                                foreach (var sharePointItem in queryJsonEntity.Sharepoint)
                                {
                                    using (ClientContext clientContext = new ClientContext(sharePointItem.URL))
                                    {
                                        clientContext.AuthenticationMode = ClientAuthenticationMode.Default;
                                        SecureString passWord = new SecureString();
                                        string pwd = sharePointItem.Passwd;
                                        foreach (char c in pwd.ToCharArray()) passWord.AppendChar(c);
                                        clientContext.Credentials = new SharePointOnlineCredentials(sharePointItem.Username, passWord);
                                        //clientContext.AuthenticationMode = ClientAuthenticationMode.Default;
                                        //clientContext.Credentials = CredentialCache.DefaultCredentials;

                                        List targetList = clientContext.Web.Lists.GetByTitle(sharePointItem.ListTitle);

                                        ListItemCollection listItems = targetList.GetItems(CamlQuery.CreateAllItemsQuery());

                                        clientContext.Load(listItems,
                                            eachItem => eachItem.Include(
                                            item => item));
                                        clientContext.ExecuteQuery();

                                        var totalListItems = listItems.Count;

                                        if (totalListItems > 0)
                                        {
                                            for (var counter = totalListItems - 1; counter > -1; counter--)
                                            {
                                                listItems[counter].DeleteObject();
                                                clientContext.ExecuteQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        try
                        {
                            foreach (var sharePointItem in queryJsonEntity.Sharepoint)
                            {

                                using (ClientContext clientContext = new ClientContext(sharePointItem.URL))
                                {

                                    clientContext.AuthenticationMode = ClientAuthenticationMode.Default;
                                    SecureString passWord = new SecureString();
                                    string pwd = sharePointItem.Passwd;
                                    foreach (char c in pwd.ToCharArray()) passWord.AppendChar(c);
                                    //clientContext.Credentials = CredentialCache.DefaultCredentials;
                                    clientContext.Credentials = new SharePointOnlineCredentials(sharePointItem.Username, passWord);

                                    List targetList = clientContext.Web.Lists.GetByTitle(sharePointItem.ListTitle);

                                    FieldCollection fields = targetList.Fields;
                                    clientContext.Load(fields);
                                    clientContext.ExecuteQuery();


                                    CamlQuery oQuery = new CamlQuery();

                                    if (sharePointItem.InsertQuery.RecordItem != null)
                                    {
                                        if (sharePointItem.InsertQuery.RecordItem.Length > 0)
                                        {

                                            foreach (var insertItem in sharePointItem.InsertQuery.RecordItem)
                                            {
                                                ListItemCreationInformation listItemCreationInformation = new ListItemCreationInformation();
                                                ListItem listItem = targetList.AddItem(listItemCreationInformation);

                                                foreach (var insertField in insertItem.Set)
                                                {
                                                    if (fields.Where(f => f.FromBaseType == false && f.Title.ToLower() == insertField.FieldName.ToLower()).Any())
                                                    {
                                                        if (!string.IsNullOrEmpty(insertField.Value.ToString().Trim()))
                                                        {
                                                            //User user = userCollection.FirstOrDefault(p => p.UserId.ToString().ToLower().Trim() == insertField.Value.ToString().ToLower().Trim());

                                                            //FieldUserValue userValue = new FieldUserValue();

                                                            //var rr = userValue.LookupId;

                                                            //-1;#

                                                            listItem[insertField.FieldName] = insertField.Value.ToString().Contains("@hp.com") ? ("-1;#" + insertField.Value) : insertField.Value;
                                                        }
                                                    }
                                                }

                                                listItem.Update();
                                                clientContext.ExecuteQuery();
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }

        public void UploadToSharePoint(string FilePath, string SiteUrl, string FolderName, string ClientId, string ClientSecret, bool OverWrite, string AuthType, out string newUrl)  //p is path to file to load
        {
            try
            {
                string siteUrl = SiteUrl;
                ClientContext context = null;
                if (AuthType.ToLower() == "windowsauth")
                {
                    context = new ClientContext(SiteUrl);
                    context.Credentials = new SharePointOnlineCredentials(ClientId, GetPasswordFromConsoleInput(ClientSecret));
                }
                else
                {
                    context = new AuthenticationManager().GetAppOnlyAuthenticatedContext(siteUrl, ClientId, ClientSecret);
                }

                Web site = context.Web;
                //Get the required RootFolder
                string barRootFolderRelativeUrl = FolderName;
                Folder barFolder = site.GetFolderByServerRelativeUrl(barRootFolderRelativeUrl);
                if (context.HasPendingRequest)
                    context.ExecuteQuery();
                using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                {
                    FileCreationInformation fileInfo = new FileCreationInformation();
                    fileInfo.ContentStream = fs;
                    fileInfo.Url = Path.GetFileName(FilePath);
                    fileInfo.Overwrite = true;
                    if (context.HasPendingRequest)
                        context.ExecuteQuery();
                    Microsoft.SharePoint.Client.File uploadFile = barFolder.Files.Add(fileInfo);
                    context.Load(uploadFile);
                    context.ExecuteQuery();
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
        public void CreateFolder(string FolderName, string SiteUrl, string RootFolder, string ClientId, string ClientSecret, string AuthType, out string newUrl)  //p is path to file to load
        {
            try
            {
                string siteUrl = SiteUrl;
                //Insert Credentials

                ClientContext context = null;
                if (AuthType.ToLower() == "windowsauth")
                {
                    context = new ClientContext(SiteUrl);
                    context.Credentials = new SharePointOnlineCredentials(ClientId, GetPasswordFromConsoleInput(ClientSecret));
                }
                else
                {
                    context = new AuthenticationManager().GetAppOnlyAuthenticatedContext(siteUrl, ClientId, ClientSecret);
                }

                //SecureString passWord = new SecureString();
                //foreach (var c in Password) passWord.AppendChar(c);
                //context.Credentials = new SharePointOnlineCredentials(UserName, passWord);
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
        public void DownloadFile(string DownloadLocation, string SiteUrl, string FolderPath, string FileName, string ClientId, string ClientSecret, string Type, string r2rAuthType)
        {
            using (ClientContext clientContext = GetContextObject(SiteUrl, ClientId, ClientSecret, r2rAuthType))
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

        private ClientContext GetContextObject(string SiteUrl, string ClientId, string ClientSecret, string r2rAuthType)
        {
            ClientContext context = null;
            if (r2rAuthType.ToLower() == "windowsauth")
            {
                context = new ClientContext(SiteUrl);
                context.Credentials = new SharePointOnlineCredentials(ClientId, GetPasswordFromConsoleInput(ClientSecret));
            }
            else
            {
                context = new AuthenticationManager().GetAppOnlyAuthenticatedContext(SiteUrl, ClientId, ClientSecret);
            }

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

    public class QueryJsonEntity
    {
        public Sharepoint[] Sharepoint { get; set; }
    }

    public class Sharepoint
    {
        public string URL { get; set; }
        public string Username { get; set; }
        public string Passwd { get; set; }
        public string ListTitle { get; set; }
        public Updatequery UpdateQuery { get; set; }
        public Insertquery InsertQuery { get; set; }
    }

    public class Insertquery
    {
        public RecordItem[] RecordItem { get; set; }
    }

    public class Updatequery
    {
        public Where[] Where { get; set; }
        public Set[] Set { get; set; }
    }

    public class Where
    {
        public string FieldName { get; set; }
        public object Value { get; set; }
    }

    public class Set
    {
        public string FieldName { get; set; }
        public object Value { get; set; }
    }

    public class RecordItem
    {
        public Set[] Set { get; set; }
    }

}
