using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace JoJoSuite.Library.Mail.Exchange
{
    class SendEmail
    {
        //Input local variables
        private string _username;
        private string _pass;
        private string _from;
        private string _to;
        private string _cc;
        private string _subject;
        private string _body;
        private bool _isbodyhtml;
        private dynamic _attachment;
        private string _filepath;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";



        public string UserName
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


        public dynamic Attachments
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

                    string[] attPathName = _filepath.Split('|');

                    objMessage.From = new MailAddress(_from);
                    objMessage.Priority = MailPriority.High;

                    //if (bccmailid != "")
                    //{
                    //    objMessage.Bcc.Add(bccmailid);
                    //}
                    if (_cc != "")
                    {
                        objMessage.CC.Add(_cc);
                    }

                    objMessage.Subject = _subject;
                    objMessage.Body = _body;
                    _filepath = _filepath.Replace("..\\", "");

                    System.Net.Mail.Attachment ObjAttachment = null;
                    foreach (string strPathName in attPathName)
                    {
                        ObjAttachment = new System.Net.Mail.Attachment(strPathName);
                        objMessage.Attachments.Add(ObjAttachment);
                    }
                    objMessage.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp1.hp.com");
                    System.Net.NetworkCredential objNetworkCredential = new System.Net.NetworkCredential(_username, _pass);
                    client.Credentials = objNetworkCredential;
                    client.EnableSsl = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(objMessage);
                    ObjStatus.Add("true");
                    ObjAttachment.Dispose();
                    objMessage.Dispose();
                }
                catch (Exception ex)
                {
                    ObjStatus.Add("false");
                }
                finally
                {
                }

                Console.WriteLine("\nMAIL SENT ============>>>>>>");
                return true;
            }

            catch (SmtpException ex)
            {
                Console.WriteLine("Mail failed to send*************", ex.Message);
                return false;
            }
        }
    }
}
