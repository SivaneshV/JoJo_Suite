using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Database
{
    public class r2rInsertToTable
    {
        // Input Local Variables
        private string _tableName;

        private List<string> _columnNameLst = new List<string>();
        private List<object> _valuesLst = new List<object>();

        private string strColumnNames;

        private string strValueNames;

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


        public List<string> ColumnName
        {
            get { return _columnNameLst; }
            set { _columnNameLst = value; }
        }

        public List<object> Values
        {
            get { return _valuesLst; }
            set { _valuesLst = value; }
        }

        public SqlConnection SqlConn
        {
            get { return _sqlconn; }
            set { _sqlconn = value; }
        }

        public int Result
        {
            get
            {
                return _result;
            }

        }

        public bool doAction()
        {
            bool res = false;

            try
            {
                //string colNames;
                //string valueNames;

                strColumnNames = string.Join(",", _columnNameLst.ToArray());

                strValueNames = "@p" + string.Join(",@p", _columnNameLst.ToArray());



                string query = "INSERT INTO " + _tableName + " (" + strColumnNames + " ) VALUES ( " + strValueNames + " ) ";

                SqlCommand cmd = new SqlCommand(query, _sqlconn);

                for (int i = 0; i < _columnNameLst.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@p" + _columnNameLst[i], _valuesLst[i]);
                }

                if (_sqlconn.State != System.Data.ConnectionState.Open)
                {
                    _sqlconn.Open();
                }

                _result = cmd.ExecuteNonQuery();

                res = true;

                _sqlconn.Close();


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
