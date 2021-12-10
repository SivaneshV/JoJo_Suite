using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using MailKit.Net.Imap;

namespace JoJoSuite.Library.Email
{
    public class r2rLoginToServerIMap
    {
        //Input local variables
        private string _server;
        private string _user;
        private string _pwd;
        private string _mailbox;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private ImapClient _ImapClient = new MailKit.Net.Imap.ImapClient();


        //Public Input properties
        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }
        public string Mailbox
        {
            get
            {
                return _mailbox;
            }
            set
            {
                _mailbox = value;
            }
        }


        public string user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        public string Password
        {
            get
            {
                return _pwd;
            }
            set
            {
                _pwd = value;
            }
        }


        //Public output properties
        public ImapClient IMapClient
        {
            get
            {
                return _ImapClient;
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
        // DoAction()

        public bool DoAction()
        {
            bool res = false;
            try
            {

                _ImapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                _ImapClient.Connect(_server, 993, true);

                _ImapClient.AuthenticationMechanisms.Remove("XOAUTH2");

                _ImapClient.Authenticate(_user + "\\" + _mailbox, _pwd);

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
