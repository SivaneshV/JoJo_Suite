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
    public class r2rUpdateStatus
    {
        //Input local variables       
        private int _BotId;
        private int _RunID;
        private int _Updatestatus;

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
        public int Updatestatus
        {
            get
            {
                return _Updatestatus;
            }
            set
            {
                _Updatestatus = value;
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
                        cmd.CommandText = "r2rUpdateRobotRunStatus";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Updatestatus", _Updatestatus);
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
