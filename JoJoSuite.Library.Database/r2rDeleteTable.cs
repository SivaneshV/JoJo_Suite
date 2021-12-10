using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Database
{
    public class r2rDeleteTable
    {
        private string _tableName;
        private string _condition;
        private string _setParameter;
        private SqlConnection _sqlconn;

        // Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private int _result;

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

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public SqlConnection SqlConn
        {
            get { return _sqlconn; }
            set { _sqlconn = value; }
        }

        public string Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        private string SetParameter
        {
            get { return _setParameter; }
            set { _setParameter = value; }
        }

        public bool doAction()
        {
            bool res = false;
            try
            {
                string query = "DELETE " + _tableName + "  WHERE  " + _condition + "";

                SqlCommand cmd = new SqlCommand(query, _sqlconn);

                _sqlconn.Open();

                _result = cmd.ExecuteNonQuery();

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
