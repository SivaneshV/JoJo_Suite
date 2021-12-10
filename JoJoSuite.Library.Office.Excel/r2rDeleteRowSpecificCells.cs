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
    public class r2rDeleteRowSpecificCells
    {
        //Input local variables
        private Worksheet _xlWorkSheet;
        private string _ColumnNamesList;
        private int _RowIndex;
        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private bool _isSuccess;


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
        public string ColumnNamesList
        {
            get
            {
                return _ColumnNamesList;
            }
            set
            {
                _ColumnNamesList = value;
            }

        }
        public int RowIndex
        {
            get
            {
                return _RowIndex;
            }
            set
            {
                _RowIndex = value;
            }

        }
        //Public output properties
        public bool IsSucces
        {
            get
            {
                return _isSuccess;
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
                foreach (string column in _ColumnNamesList.Split(','))
                {
                    _xlWorkSheet.Range[column + (RowIndex)].Value = "";
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
