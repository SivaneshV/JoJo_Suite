using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rCreateWorkbook
    {
        //Input local variables
        private string _file;
        private bool _xlvisible;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private Workbook _xlWorkBook;


        //Public Input properties
        public string File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
            }
        }
        public bool xlVisible
        {
            get
            {
                return _xlvisible;
            }
            set
            {
                _xlvisible = value;
            }
        }

        //Public output properties
        public Workbook xlWorkBook
        {
            get
            {
                return _xlWorkBook;
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

        /// <summary>
        /// DoAction()
        /// </summary>
        /// <returns></returns>
        public bool DoAction()
        {
            bool res = false;
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (_xlvisible == true)
                {
                    xlApp.Visible = true;
                }
                xlApp.DisplayAlerts = false;
                _xlWorkBook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                _error = false;
                _errorMsg = "";

                _error = AutoSave(_xlWorkBook,out _errorMsg);
               
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

        private bool AutoSave(Workbook xlWorkBookAutoSave,out string Error)
        {
            Error = "";
            try
            {
                xlWorkBookAutoSave.SaveAs(_file, XlFileFormat.xlOpenXMLWorkbook, Type.Missing,
                        Type.Missing, false, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
                return false;               
            }
        }
    }
}
