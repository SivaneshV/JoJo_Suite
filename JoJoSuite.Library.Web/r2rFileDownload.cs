using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Library.Web
{
    public class r2rFileDownload
    {
        //Input local variables
        private string _URL;
        private string _Username;
        private string _Password;
        private string _DownloadPath;

        private string _fileName;
        private bool _useAutoName;

        private string _fileType;

        //Output local variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _outputStr;

        //Public input properties
        public string URL
        {
            get
            {
                return _URL;
            }

            set
            {
                _URL = value;
            }
        }
        public string Username
        {
            get
            {
                return _Username;
            }

            set
            {
                _Username = value;
            }
        }
        public string Password
        {
            get
            {
                return _Password;
            }

            set
            {
                _Password = value;
            }
        }
        public string DownloadPath
        {
            get
            {
                return _DownloadPath;
            }

            set
            {
                _DownloadPath = value;
            }

        }




        //Public input properties
        public string OutputStr
        {
            get
            {
                return _outputStr;
            }

        }
        public bool Error
        {
            get
            {
                return _error;
            }

        }
        public string ErrorMessage
        {
            get
            {
                return _errorMsg;
            }

        }

        public bool DoAction()
        {
            bool res = false;
            try
            {

                var client = new WebClient { Credentials = new NetworkCredential(_Username, _Password) };
                // var client = new WebClient();
                var response = client.DownloadData(_URL);
                string headerContentDispostion = client.ResponseHeaders["Content-Disposition"];

                if (headerContentDispostion.Trim().Length > 0)
                {
                    Console.WriteLine("File Name found in Headers");
                    string fileName = headerContentDispostion.Substring(headerContentDispostion.IndexOf("filename=") + 9).Replace("\"", "");
                    File.WriteAllBytes(_DownloadPath + fileName, response);
                    Console.WriteLine("Download completed:" + fileName);
                    _error = false;
                    _errorMsg = "";
                    res = true;
                }
                else
                {
                    Console.WriteLine("File Name not found in Headers, automatically creating filename");
                    string fileName = "_" + DateTime.Now.ToString("ddMMyyyhhmmss");
                    File.WriteAllBytes(_DownloadPath + fileName, response);
                    string trId = System.AppDomain.CurrentDomain.BaseDirectory + @"Tools\exiftool.exe";
                    string file = _DownloadPath + fileName;
                    var proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = trId,
                            Arguments = file,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    proc.Start();

                    string ResStr = proc.StandardOutput.ReadToEnd();
                    List<string> splitstr = ResStr.Split(new[] { "\r\n" }, StringSplitOptions.None).Where(a => a.Contains("File Type Extension")).ToList();

                    if (splitstr.Count > 0)
                    {
                        string fileextension = splitstr[0].Replace("File Type Extension", "").Replace(" ", "").Replace(":", "");
                        if (fileextension == "xml")
                        {
                            if (ResStr.IndexOf("Workbook") > -1)
                            {
                                //MessageBox.Show("xls");
                                File.Move(file, file + ".xls");
                            }

                        }
                        else
                        {
                            //MessageBox.Show(fileextension);
                            File.Move(file, file + "." + fileextension);
                        }

                    }
                    Console.WriteLine("Download completed:" + fileName);
                    _error = false;
                    _errorMsg = "";
                    res = true;
                }




            }

            catch (Exception ex)
            {
                res = false;
                _error = true;
                _errorMsg = this.GetType().ToString() + ":\n" + ex.Message;
            }
            return res;
        }
    }
}
