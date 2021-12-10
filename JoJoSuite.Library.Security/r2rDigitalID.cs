using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Security
{
    public class r2rDigitalID
    {
        //Input local variables       
        private string _accountNtid;
        private string _accountEmailid;
        private string _env;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _pass = "";

        //Public Input properties      
        public string AccountNtid
        {
            get
            {
                return _accountNtid;
            }
            set
            {
                _accountNtid = value;
            }

        }
        public string AccountEmailid
        {
            get
            {
                return _accountEmailid;
            }
            set
            {
                _accountEmailid = value;
            }

        }
        public string Env
        {
            get
            {
                return _env;
            }
            set
            {
                _env = value;
            }

        }

        //Public output properties      

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
        public string Pass
        {
            get
            {
                return _pass;
            }

        }

        // DoAction()

        public bool DoAction()
        {
            bool res = false;
            try
            {

                string fullPath = ConfigurationManager.AppSettings["digiturl"] + _accountNtid + "&env=" + _env;
                string uid = _accountEmailid;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullPath);
                request.Method = "Get";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "appication/json";
                string pwd = "";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    _pass = sr.ReadToEnd();
                }

                if (pwd.Contains("500"))
                {
                    Console.Write("Unable to retrieve password :" + pwd);
                    throw new System.InvalidOperationException("Unable to retrieve password :" + pwd);
                }

                _error = false;
                _errorMsg = "";
                res = true;
            }

            catch (Exception ex)
            {
                //throw new System.InvalidOperationException("Unable to retrieve password :" + ex.Message);
                res = false;
                _error = true;
                _errorMsg = "Unable to retrieve password :" + ":\n" + ex.Message;
            }
            return res;
        }



    }
}
