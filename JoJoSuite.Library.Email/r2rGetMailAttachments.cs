using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using Microsoft.Exchange.WebServices.Data;

namespace JoJoSuite.Library.Email
{
    public class r2rGetMailAttachments
    {
        //Input local variables        
        private object _emailId;
        private string _folderPath;
        private ExchangeService _ewsConn;
        private ImapClient _IMAPClient;
        private string _MailType;
        private string _folderName;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string[] _fileList;



        //Public Input properties
        public string FolderName
        {
            get
            {
                return _folderName;
            }
            set
            {
                _folderName = value;
            }
        }
        public object EmailId
        {
            get
            {
                return _emailId;
            }
            set
            {
                _emailId = value;
            }
        }
        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                _folderPath = value;
            }
        }
        public string MailType
        {
            get
            {
                return _MailType;
            }
            set
            {
                _MailType = value;
            }
        }
        public ExchangeService EwsConn
        {
            get
            {
                return _ewsConn;
            }
            set
            {
                _ewsConn = value;
            }
        }
        public ImapClient IMAPClient
        {
            get
            {
                return _IMAPClient;
            }
            set
            {
                _IMAPClient = value;
            }
        }

        //output Properties
        public string[] FileList
        {
            get
            {
                return _fileList;
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
            EmailMessage message;
            try
            {
                if (_emailId != null)
                {
                    if (MailType.ToLower().Trim() == "exchangeservice")
                    {
                        message = EmailMessage.Bind(_ewsConn, _emailId.ToString());
                        _fileList = new string[message.Attachments.Count];
                        int count = 0;
                        foreach (Attachment attachment in message.Attachments)
                        {
                            if (attachment is FileAttachment)
                            {
                                FileAttachment fileattachment = attachment as FileAttachment;
                                var fullPath = System.IO.Path.Combine(_folderPath, fileattachment.Name);
                                fileattachment.Load(fullPath);
                                _fileList[count] = fullPath;
                            }
                            count = count + 1;

                        }
                    }
                    else
                    {
                        var folder = _IMAPClient.GetFolder(_folderName);

                        var msg = folder.GetMessage((MailKit.UniqueId)_emailId);

                        _fileList = new string[msg.Attachments.Count()];
                        int count = 0;
                        foreach (MimeKit.MimeEntity attachment in msg.Attachments)
                        {
                            var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                            var fullPath = System.IO.Path.Combine(_folderPath, fileName);

                            using (var stream = File.Create(fullPath))
                            {
                                if (attachment is MimeKit.MessagePart)
                                {
                                    var rfc822 = (MimeKit.MessagePart)attachment;

                                    rfc822.Message.WriteTo(stream);
                                    _fileList[count] = fullPath;
                                }
                                else
                                {
                                    var part = (MimeKit.MimePart)attachment;

                                    part.Content.DecodeTo(stream);
                                    _fileList[count] = fullPath;
                                }
                                count = count + 1;
                            }
                        }
                    }
                }

                _error = false;
                _errorMsg = "";
                res = true;
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
