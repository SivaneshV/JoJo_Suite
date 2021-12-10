using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace JoJoSuite.Library.Database
{

    public class r2rConnectToDatabase
    {
        //Input local variables
        private string _server;
        private string _database;
        private string _user;
        private string _pwd;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private SqlConnection _sqlconn;


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
        public string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
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
        public SqlConnection sqlConnection
        {
            get
            {
                return _sqlconn;
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
                string connetionstr = string.Empty;

                if (!string.IsNullOrEmpty(_user) && !string.IsNullOrEmpty(_pwd))
                {
                    //  _server = "c2w30220.itcs.hpicorp.ne"; _database = "Rebalancing"; _user = "sa"; _pwd = "omni5678$$";
                    connetionstr = "Data Source ={0}; Initial Catalog = {1}; Persist Security Info =true; User ID = {2}; Password = {3}";
                    connetionstr = connetionstr.Replace("{0}", _server);
                    connetionstr = connetionstr.Replace("{1}", _database);
                    connetionstr = connetionstr.Replace("{2}", _user);
                    connetionstr = connetionstr.Replace("{3}", _pwd);
                }
                else
                {
                    connetionstr = "Data Source ={0}; Initial Catalog = {1}; Integrated Security =true;";
                    connetionstr = connetionstr.Replace("{0}", _server);
                    connetionstr = connetionstr.Replace("{1}", _database);
                }

                // SqlConnection con = new SqlConnection(connetionstr);
                _sqlconn = new SqlConnection(connetionstr);
                _sqlconn.Open();
                _sqlconn.Close();
                _error = false;
                _errorMsg = "";
                res = true;
                // if (_sqlconn.State != _sqlconn.Open) { _sqlconn.Open(); }

          
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
