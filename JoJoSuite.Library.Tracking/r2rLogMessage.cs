using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace JoJoSuite.Library.Tracking
{
    public class r2rLogMessage
    {
        //Input local variables       
        private int _BotId;
        private int _RunID;
        private string _LogMessage;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";



        //Public Input properties      
        public int BotId
        {
            get
            {
                return _BotId;
            }
            set
            {
                _BotId = value;
            }

        }
        public int RunID
        {
            get
            {
                return _RunID;
            }
            set
            {
                _RunID = value;
            }

        }
        public string LogMessage
        {
            get
            {
                return _LogMessage;
            }
            set
            {
                _LogMessage = value;
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
        // DoAction()

        public bool DoAction()
        {
            bool res = false;
            try
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["r2rDbConStr"]))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rUpdateBotLogMessage";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LogMessage", _LogMessage);
                        cmd.Parameters.AddWithValue("@RunId", _RunID);
                        cmd.Parameters.AddWithValue("@FKBot", _BotId);
                        cmd.ExecuteNonQuery();

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
