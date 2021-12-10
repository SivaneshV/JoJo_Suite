using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rXlCopyPaste
    {
        //ExcelCopyPaste

        //Input local variables
        private Worksheet _xlWorksheetCopy;
        private Worksheet _xlWorksheetPaste;
        private string _cellAddressCopy;
        private string _cellAddressPaste;
        private bool _xlEntireColumn;
        private bool _xlEntireRow;
        private string _xlPasteType;




        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";



        //Public Input properties
        public Worksheet xlWorksheetCopy
        {
            get
            {
                return _xlWorksheetCopy;
            }
            set
            {
                _xlWorksheetCopy = value;
            }

        }
        public Worksheet xlWorksheetPaste
        {
            get
            {
                return _xlWorksheetPaste;
            }
            set
            {
                _xlWorksheetPaste = value;
            }

        }
        public string cellAddressCopy
        {
            get
            {
                return _cellAddressCopy;
            }
            set
            {
                _cellAddressCopy = value;
            }

        }
        public string cellAddressPaste
        {
            get
            {
                return _cellAddressPaste;
            }
            set
            {
                _cellAddressPaste = value;
            }

        }
        public bool xlEntireColumn
        {
            get
            {
                return _xlEntireColumn;
            }
            set
            {
                _xlEntireColumn = value;
            }

        }
        public bool xlEntireRow
        {
            get
            {
                return _xlEntireRow;
            }
            set
            {
                _xlEntireRow = value;
            }

        }
        public string xlPasteType
        {
            get
            {
                return _xlPasteType;
            }
            set
            {
                _xlPasteType = value;
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
                //Copy Actions
                if (_xlEntireRow == true)
                {
                    _xlWorksheetCopy.Range[_cellAddressCopy].EntireRow.Copy(Type.Missing);
                }
                else if (_xlEntireColumn == true)
                {
                    _xlWorksheetCopy.Range[_cellAddressCopy].EntireColumn.Copy(Type.Missing);
                }
                else if (_xlEntireRow == false && _xlEntireColumn == false)
                {
                    _xlWorksheetCopy.Range[_cellAddressCopy].Copy();
                }
                
                //Console.WriteLine(Clipboard.GetText());
                //string a = Clipboard.GetText();
                //Console.WriteLine(a.ToString());
                //Paste Actions
                if (_xlEntireRow == true)
                {
                    _xlWorksheetPaste.Range[_cellAddressPaste].EntireRow.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                }
                else if (_xlEntireColumn == true)
                {
                    _xlWorksheetPaste.Range[_cellAddressPaste].EntireColumn.PasteSpecial(XlPasteType.xlPasteValues);
                }
                else if (_xlEntireRow == false && _xlEntireColumn == false)
                {
                    if (_xlPasteType == "Values")
                    {
                        _xlWorksheetPaste.Range[_cellAddressPaste].PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                    }
                    else if (_xlPasteType == "Formulas")
                    {
                        _xlWorksheetPaste.Range[_cellAddressPaste].PasteSpecial(XlPasteType.xlPasteFormulas, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                    }

                }

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
