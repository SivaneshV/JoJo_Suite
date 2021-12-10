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
    public class r2rAddSheet
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
                 _xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)_xlWorkBook.Worksheets.Add
                    (System.Reflection.Missing.Value,
                    _xlWorkBook.Worksheets[_xlWorkBook.Worksheets.Count],
                    System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value);
                _xlWorksheet.Name = _sheetname;


                //_worksheetobject = _expackage.Workbook.Worksheets.FirstOrDefault(x => x.Name == _existingsheetname);
                //if (_worksheetobject == null)
                //{
                //    _worksheetobject = _expackage.Workbook.Worksheets.Add(_sheetname);
                //}
                //else
                //{
                //    _worksheetobject.Name = _sheetname;
                //}


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
