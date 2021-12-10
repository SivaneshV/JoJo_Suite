using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rSaveWorkbook
    {
        //Input local variables
        private string _file;
        private bool _saveAs;
        private Workbook _xlWorkBook;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";



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
        public bool SaveAs
        {
            get
            {
                return _saveAs;
            }
            set
            {
                _saveAs = value;
            }
        }

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
                dynamic TempxlApp = _xlWorkBook.Application;
                if (_saveAs == true)
                {
                    if (Path.GetExtension(_file).ToLower().Contains("csv"))
                    {
                        _xlWorkBook.SaveAs(_file, XlFileFormat.xlCSV, Type.Missing,
                                    Type.Missing, false, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }
                    else
                    {
                        _xlWorkBook.SaveAs(_file, XlFileFormat.xlOpenXMLWorkbook, Type.Missing,
                                    Type.Missing, false, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }
                }
                else
                {
                    _xlWorkBook.Save();
                }

                _xlWorkBook.Close(true, Type.Missing, Type.Missing);

                _xlWorkBook = null;
                TempxlApp.Quit();
                TempxlApp = null;

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
