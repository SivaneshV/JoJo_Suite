using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
namespace JoJoSuite.Library.Office.Excel
{
   public class r2rXlPaste
    {
        //Input local variables
        private Worksheet _xlWorksheet;
        private string _celladdress;
        private string _xlPasteType;
        private bool _xlEntireColumn;
        private bool _xlEntireRow;


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
                if (_xlEntireRow == true)
                {
                    _xlWorksheet.Range[_celladdress].EntireRow.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                }
                else if (_xlEntireColumn == true)
                {
                    _xlWorksheet.Range[_celladdress].EntireColumn.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                }
                else if (_xlEntireRow == false && _xlEntireColumn == false)
                {
                    if (_xlPasteType == "Values")
                    {
                        _xlWorksheet.Range[_celladdress].PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
                    }
                    else if (_xlPasteType== "Formulas")
                    {
                        _xlWorksheet.Range[_celladdress].PasteSpecial(XlPasteType.xlPasteFormulas, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
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
