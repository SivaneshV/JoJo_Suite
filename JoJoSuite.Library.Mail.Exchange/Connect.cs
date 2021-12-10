using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Exchange.WebServices.Data;
using System.Data;

namespace JoJoSuite.Library.Mail.Exchange
{
    public class Connect
    {
        //Input local variables
        private string _server;
        private string _domain;
        private string _user;
        private string _pwd;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private ExchangeService _ewsConn;

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


        public string User
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


            DataTable dt1 = new DataTable();

            var a = dt1.Rows.Cast<object>();

            try
            {
                _ewsConn = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                _ewsConn.Credentials = new WebCredentials(_user, _pwd, _domain);

                _ewsConn.Url = new Uri(_server);

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
