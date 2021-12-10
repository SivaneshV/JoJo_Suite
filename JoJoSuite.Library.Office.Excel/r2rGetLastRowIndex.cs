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
    public class r2rGetLastRowIndex
    {
        //Input local variables
        private Worksheet _xlWorkSheet;
        private bool _isColumnValueSearch;
        private int _ColumnIndex;
        private string _ColumnValue;
      
        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private int _rowIndex;


        //Public Input properties
        public Worksheet xlWorkSheet
        {
            get
            {
                return _xlWorkSheet;
            }
            set
            {
                _xlWorkSheet = value;
            }

        }
        public bool IsColumnValueSearch
        {
            get
            {
                return _isColumnValueSearch;
            }
            set
            {
                _isColumnValueSearch = value;
            }

        }
        public int ColumnIndex
        {
            get
            {
                return _ColumnIndex;
            }
            set
            {
                _ColumnIndex = value;
            }

        }
        public string ColumnValue
        {
            get
            {
                return _ColumnValue;
            }
            set
            {
                _ColumnValue = value;
            }

        }
        //Public output properties
        public int RowIndex
        {
            get
            {
                return _rowIndex;
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
                var Rowindex = 0;
                Microsoft.Office.Interop.Excel.Range lastCell = _xlWorkSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing);
                Rowindex = lastCell.Row;
                if (_isColumnValueSearch)
                {
                 
                    while (!((_xlWorkSheet.Cells[Rowindex, _ColumnIndex]).Text.Contains(_ColumnValue)) && (Rowindex != 1))
                    {
                        Rowindex--;
                    }
                    _rowIndex = Rowindex;
                }
                else
                {
                    _rowIndex = Rowindex;
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
