using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace JoJoSuite.Library.Email
{
    public class r2rSendMail
    {
        //Input local variables
        private string _username;
        private string _pass;
        private string _from;
        private string _to;
        private string _cc="";
        private string _subject;
        private string _SMTPHost;
        private string _body;
        private bool _isbodyhtml;
        private dynamic _attachment;
        private string _filepath;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";



        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
            }
        }

        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }


        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }


        public string Cc
        {
            get
            {
                return _cc;
            }
            set
            {
                _cc = value;
            }
        }


        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }


        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }


        public string Attachment
        {
            get
            {
                return _attachment;
            }
            set
            {
                _attachment = value;
            }
        }

        public string FilePath
        {
            get
            {
                return _filepath;
            }
            set
            {
                _filepath = value;
            }
        }

        public string SMTPHost
        {
            get
            {
                return _SMTPHost;
            }
            set
            {
                _SMTPHost = value;
            }
        }

        public bool IsBodyHtml
        {
            get
            {
                return _isbodyhtml;
            }
            set
            {
                _isbodyhtml = value;
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

                List<string> ObjStatus = new List<string>();
                try
                {
                    string[] toadd = _to.Split(',');
                    MailMessage objMessage = new MailMessage();

                    foreach (string strToAddress in toadd)
                    {
                        if (strToAddress.Length > 0)
                            objMessage.To.Add(new MailAddress(strToAddress));
                    }
                    string[] attPathName = new string[] { };
                    if (_filepath != null)
                    {
                        attPathName = _filepath.Split('|');
                    }
                    // string[] attPathName = _filepath.Split('|');

                    objMessage.From = new MailAddress(_from);
                    objMessage.Priority = MailPriority.High;

                    //if (bccmailid != "")
                    //{
                    //    objMessage.Bcc.Add(bccmailid);
                    //}
                    if (_cc != "" && _cc != null)
                    {
                        objMessage.CC.Add(_cc);
                    }

                    objMessage.Subject = _subject;
                    objMessage.Body = _body;
                   // _filepath = _filepath.Replace("..\\", "");

                    System.Net.Mail.Attachment ObjAttachment = null;
                    foreach (string strPathName in attPathName)
                    {

                        ObjAttachment = new System.Net.Mail.Attachment(strPathName);
                        objMessage.Attachments.Add(ObjAttachment);
                    }


                    objMessage.IsBodyHtml = _isbodyhtml;
                    SmtpClient client = new SmtpClient(_SMTPHost);
                    System.Net.NetworkCredential objNetworkCredential = new System.Net.NetworkCredential(_username, _pass);
                    client.Credentials = objNetworkCredential;
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(objMessage);
                    ObjStatus.Add("true");
                    if (ObjAttachment!=null)
                    {
                        ObjAttachment.Dispose();
                    }
                  
                    objMessage.Dispose();
                    res = true;

                }
                catch (Exception ex)
                {
                    res = false;
                    _error = true;
                    _errorMsg = this.GetType().ToString() + ":\n" + ex.Message;

                }
                finally
                {


                }            
               
            }

            catch (SmtpException ex)
            {
                res = false;
                _error = true;
                _errorMsg = this.GetType().ToString() + ":\n" + ex.Message;
            }
            return res;
        }
    }

}


