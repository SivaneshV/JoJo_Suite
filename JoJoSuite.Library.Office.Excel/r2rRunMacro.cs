using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelIn = Microsoft.Office.Interop.Excel;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rRunMacro
    {
        //Input local variables
        private string _xlFileName;
        private string _xlMacroName;
        private bool _xlvisible;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";

        //Public Input Properties
        public string xlFileName
        {
            get
            {
                return _xlFileName;
            }
            set
            {
                _xlFileName = value;
            }

        }

        public string xlMacroName
        {
            get
            {
                return _xlMacroName;
            }
            set
            {
                _xlMacroName = value;
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

        //Public Output Properties    
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
                ExcelIn.Application xlApp = new ExcelIn.Application();
                xlApp.DisplayAlerts = false;
                xlApp.AskToUpdateLinks = false;
                ExcelIn.Workbook xlWorkBook;
                
                xlWorkBook = xlApp.Workbooks.Open(_xlFileName);
                if (_xlvisible == true)
                {
                    xlApp.Visible = true;
                }
              
                xlApp.DisplayAlerts = false;
                xlApp.Run(_xlMacroName);

                xlWorkBook.Close(false);

                xlApp.Quit();

                releaseObject(xlApp);
                releaseObject(xlWorkBook);
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

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                _errorMsg = ex.Message.ToString();
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
