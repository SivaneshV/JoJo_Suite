using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rCreateTable
    {
        //Input local variables
        private Worksheet _xlWorksheet;
        private string _celladdress;
        private string _xlTableName;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
       


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
        public string xlTableName
        {
            get
            {
                return _xlTableName;
            }
            set
            {
                _xlTableName = value;
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
                Range range = (Range)_xlWorksheet.get_Range(_celladdress);
                _xlWorksheet.ListObjects.AddEx(XlListObjectSourceType.xlSrcRange, range, Type.Missing,
                   XlYesNoGuess.xlYes, Type.Missing).Name = _xlTableName;
               

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
