using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Database
{
    public class r2rDataQuery
    {
        private string[] _parameters;
        private string[] _valueslist;
        private string _query;
        private SqlConnection _sqlconn;
        private string _type;
        // Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private DataTable _resultdatatable=new DataTable();


        //Public Input properties
        public string[] Parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }
        public string[] ValuesList
        {
            get
            {
                return _valueslist;
            }
            set
            {
                _valueslist = value;
            }
        }
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
            }
        }
        public SqlConnection SqlConn
        {
            get { return _sqlconn; }
            set { _sqlconn = value; }
        }
        public string QueryType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        //Public output properties
        public DataTable Resultdatatable
        {
            get
            {
                return _resultdatatable;
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
            try
            {
                SqlCommand cmd = new SqlCommand(_query, _sqlconn);

                if (_parameters != null && _valueslist != null)
                {
                    if (_type == "StoreProcedure")
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        cmd.CommandType = CommandType.Text;
                    }

                    for (int i = 0; i < _parameters.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue(_parameters[i], _valueslist[i]);
                    }
                }
              
                _sqlconn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(_resultdatatable);

                _sqlconn.Close();

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
