using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using Microsoft.Exchange.WebServices.Data;


namespace JoJoSuite.Library.Email
{
    public class r2rLoginToServer
    {
        //Input local variables
        private string _server;
        private string _domain;
        private string _user;
        private string _pwd;
        private string _MailType;
        private Int32 _emailIMAPPort;
        private ImapClient _IMAPClient;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private ExchangeService _ewsConn;


        //Public Input properties
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
        public Int32 EmailIMAPPort
        {
            get
            {
                return _emailIMAPPort;
            }
            set
            {
                _emailIMAPPort = value;
            }
        }
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
        public string Domain
        {
            get
            {
                return _domain;
            }
            set
            {
                _domain = value;
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
        public ExchangeService ewsConnection
        {
            get
            {
                return _ewsConn;
            }

        }
        public ImapClient IMAPClient
        {
            get
            {
                return _IMAPClient;
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
                if (_MailType.ToLower() == "exchangeservice")
                {
                    _ewsConn = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                    _ewsConn.Credentials = new WebCredentials(_user, _pwd, _domain);

                    _ewsConn.Url = new Uri(_server);

                    _error = false;
                    _errorMsg = "";
                    res = true;
                }
                else
                {
                    _IMAPClient = new ImapClient();

                    _IMAPClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(MySecurityProtocolType.Ssl3 | MySecurityProtocolType.Tls12 | MySecurityProtocolType.Tls11 | MySecurityProtocolType.Tls);
                    _IMAPClient.Connect(_domain, _emailIMAPPort, MailKit.Security.SecureSocketOptions.SslOnConnect);

                    _IMAPClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    _IMAPClient.Authenticate(_user, _pwd);
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
