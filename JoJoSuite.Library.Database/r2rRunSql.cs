using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace r2rStudio.Library.Database
{
    public class r2rRunSql
    {
        //Input local variables
        //private string _server;
        //private string _database;
        //private string _user;
        private string _sqlScript;
        private SqlConnection _sqlconn;

        private bool _singlevalue = true;



        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        //Public Input properties
        public string SqlScript
        {
            get
            {
                return _sqlScript;
            }
            set
            {
                _sqlScript = value;
            }
        }
        public SqlConnection sqlConnection
        {
            get
            {
                return _sqlconn;
            }

        }

        public bool SingleValue
        {
            get
            {
                return _singlevalue;
            }
            set
            {
                _singlevalue = value;
            }
        }
        public bool DoAction()
        {
            bool res = false;
            try
            {

            
                DataSet datatset = new DataSet();
                if (sqlConnection.State != ConnectionState.Open) { sqlConnection.Open(); }
                SqlCommand cmd = new SqlCommand("DoAction", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SingleValue", SingleValue);
                cmd.Parameters.AddWithValue("@SqlScript", SqlScript);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(datatset);
                sqlConnection.Close();

                cmd.Dispose();
                _error = false;
                _errorMsg = "";
                res = true;



            }

            catch (Exception ex)
            {
                res = false;
                _error = true;
                _errorMsg = ex.Message;
            }
            return res;
        }
    }




}

