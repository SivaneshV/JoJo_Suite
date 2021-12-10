using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rSetSheet
    {
        //Input local variables
        private Workbook _xlWorkBook;
        private string _sheetname;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private Worksheet _xlWorksheet;


        //Public Input properties
        public Workbook xlWorkBook
        {
            get
            {
                return _xlWorkBook;
            }
            set
            {
                _xlWorkBook = value;
            }

        }
        public string SheetName
        {
            get
            {
                return _sheetname;
            }
            set
            {
                _sheetname = value;
            }

        }
        //Public output properties
        public Worksheet xlWorksheet
        {
            get
            {
                return _xlWorksheet;
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
                _xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)_xlWorkBook.Worksheets[_sheetname];

                _xlWorksheet.Activate();

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
