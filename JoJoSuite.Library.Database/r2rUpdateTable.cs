using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Database
{
    public class r2rUpdateTable
    {
        private string _tableName;
        private string _condition;
        private string _setParameter;
        private string _setValue;
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

        public SqlConnection SqlConn
        {
            get { return _sqlconn; }
            set { _sqlconn = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public string SetParameter
        {
            get { return _setParameter; }
            set { _setParameter = value; }
        }

        public string SetValue
        {
            get { return _setValue; }
            set { _setValue = value; }
        }

        public bool doAction()
        {
            bool res = false;

            try
            {
                r2rInsertToTable obj = new r2rInsertToTable();


                string query = "UPDATE " + _tableName + " SET " + _setParameter + " =  '" + _setValue + "'  where  " + _condition + "";

                SqlCommand cmd = new SqlCommand(query, _sqlconn);

                if (_sqlconn.State != System.Data.ConnectionState.Open)
                {
                    _sqlconn.Open();
                }

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
