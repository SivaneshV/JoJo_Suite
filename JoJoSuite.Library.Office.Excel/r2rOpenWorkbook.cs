using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rOpenWorkbook
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
        // DoAction()

        public bool DoAction()
        {
            bool res = false;
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.DefaultSaveFormat = XlFileFormat.xlOpenXMLWorkbook;
                if (_xlvisible == true)
                {
                    xlApp.Visible = true;
                }
                xlApp.DisplayAlerts = false;
                xlApp.AskToUpdateLinks = false;
                _xlWorkBook = xlApp.Workbooks.Open(_file, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                
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
