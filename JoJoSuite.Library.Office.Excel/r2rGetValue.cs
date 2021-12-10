using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rGetValue
    {
        //Input local variables
        private Worksheet _xlWorksheet;
        private string _celladdress;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _getvalue;


        //Public Input properties
        public Worksheet xlWorksheet
        {
            get
            {
                return _xlWorksheet;
            }
            set
            {
                _xlWorksheet = value;
            }

        }
        public string CellAddress
        {
            get
            {
                return _celladdress;
            }
            set
            {
                _celladdress = value;
            }

        }


        //Public output properties
        public string GetValue
        {
            get
            {
                if (_getvalue == null)
                {
                    _getvalue = "";
                }

                return _getvalue;
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
                
              _getvalue = Convert.ToString(_xlWorksheet.Range[_celladdress].Value2);

                // _getvalue = Convert.ToString(_worksheetobject.Cells[_celladdress].Value);
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
