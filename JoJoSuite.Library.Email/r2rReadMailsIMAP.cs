using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;

namespace JoJoSuite.Library.Email
{
    public class r2rReadMailsIMAP
    {


        //Input local variables
        //private string _mbox;

        private string _folder;
        private string _subjectFilter = "";
        private string _fromFilter = "";
        private bool _onlyNew = false;
        private bool _MarkAsRead = false;
        private ImapClient _IMAPClient = null;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private MailItems _mails = new MailItems();
        public IMailFolder _IMAPFolder = null;

        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
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
        public string SubjectFilter
        {
            get
            {
                return _subjectFilter;
            }
            set
            {
                _subjectFilter = value;
            }
        }
        public string FromAddressFilter
        {
            get
            {
                return _fromFilter;
            }
            set
            {
                _fromFilter = value;
            }
        }

        public bool OnlyNew
        {
            get
            {
                return _onlyNew;
            }
            set
            {
                _onlyNew = value;
            }
        }

        public bool MarkAsRead
        {
            get
            {
                return _MarkAsRead;
            }
            set
            {
                _MarkAsRead = value;
            }
        }

        public MailItems MAILS
        {
            get
            {
                return _mails;
            }
            set
            {
                _mails = value;
            }
        }
        //public IMailFolder IMAPFolder
        //{
        //    get
        //    {
        //        return _IMAPFolder;
        //    }

        //}
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

                //using (var _IMAPClient = new MailKit.Net.Imap.ImapClient())
                //{
                //    _IMAPClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(MySecurityProtocolType.Ssl3 | MySecurityProtocolType.Tls12 | MySecurityProtocolType.Tls11 | MySecurityProtocolType.Tls);
                //    _IMAPClient.Connect(_emailIMAPHost, _emailIMAPPort, MailKit.Security.SecureSocketOptions.SslOnConnect);

                //    _IMAPClient.AuthenticationMechanisms.Remove("XOAUTH2");

                //    _IMAPClient.Authenticate(_emailIMAPUsername, _emailIMAPPassword);

                _IMAPFolder = _IMAPClient.GetFolder(_folder);
                _IMAPFolder.Open(FolderAccess.ReadWrite);

                Console.WriteLine("Total messages: {0}", _IMAPFolder.Count);
                Console.WriteLine("Recent messages: {0}", _IMAPFolder.Recent);

                IList<UniqueId> uids = null;

                if (OnlyNew == true)
                {
                    if (!string.IsNullOrEmpty(_subjectFilter) && !string.IsNullOrEmpty(_fromFilter))
                    {
                        uids = _IMAPFolder.Search(SearchQuery.NotSeen.And(SearchQuery.NotDeleted).And(SearchQuery.SubjectContains(_subjectFilter)).And(SearchQuery.FromContains(_fromFilter)));
                    }
                    else if (!string.IsNullOrEmpty(_subjectFilter) && string.IsNullOrEmpty(_fromFilter))
                    {
                        uids = _IMAPFolder.Search(SearchQuery.NotSeen.And(SearchQuery.NotDeleted).And(SearchQuery.SubjectContains(_subjectFilter)));
                    }
                    else
                    {
                        uids = _IMAPFolder.Search(SearchQuery.NotSeen.And(SearchQuery.NotDeleted));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_subjectFilter) && !string.IsNullOrEmpty(_fromFilter))
                    {
                        uids = _IMAPFolder.Search(SearchQuery.NotDeleted.And(SearchQuery.SubjectContains(_subjectFilter)).And(SearchQuery.FromContains(_fromFilter)));
                    }
                    else if (!string.IsNullOrEmpty(_subjectFilter) && string.IsNullOrEmpty(_fromFilter))
                    {
                        uids = _IMAPFolder.Search(SearchQuery.NotDeleted.And(SearchQuery.SubjectContains(_subjectFilter)));
                    }
                    else
                    {
                        uids = _IMAPFolder.Search(SearchQuery.NotDeleted);
                    }
                }

                foreach (var uid in uids)
                {
                    var message = _IMAPFolder.GetMessage(uid);

                    if (message != null)
                    {
                        r2rMailItem M1 = new r2rMailItem();
                        M1.Id = message.MessageId;
                        M1.Subject = message.Subject;
                        M1.UniqueId = uid;
                        foreach (var to1 in message.To)
                        {
                            M1.To += to1.Name + ";";
                        }
                        foreach (var from1 in message.From)
                        {
                            M1.From += from1.Name + ";";
                        }

                        if (_mails.Mails == null)
                            _mails.Mails = new List<r2rMailItem>();

                        _mails.Mails.Add(M1);

                        if (_MarkAsRead)
                        {
                            _IMAPFolder.AddFlags(uid, MessageFlags.Seen, true);
                        }
                    }
                }

                res = true;
                //}
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
    [Flags]
    public enum MySecurityProtocolType
    {
        //
        // Summary:
        //     Specifies the Secure Socket Layer (SSL) 3.0 security protocol.
        Ssl3 = 48,
        //
        // Summary:
        //     Specifies the Transport Layer Security (TLS) 1.0 security protocol.
        Tls = 192,
        //
        // Summary:
        //     Specifies the Transport Layer Security (TLS) 1.1 security protocol.
        Tls11 = 768,
        //
        // Summary:
        //     Specifies the Transport Layer Security (TLS) 1.2 security protocol.
        Tls12 = 3072
    }
    //public class r2rMailItem
    //{
    //    public string Id { get; set; }
    //    public string Subject { get; set; }
    //    public string From { get; set; }
    //    public string To { get; set; }
    //    public string Body { get; set; }
    //    public bool HasAttachment { get; set; }


    //}
}

