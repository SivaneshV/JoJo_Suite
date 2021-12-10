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
    public class r2rDeleteRowsRange
    {
        //Input local variables
        private Worksheet _xlWorkSheet;
        private string _RangeColumnFrom;
        private string _RangeColumnTo;
        private int _RangeRowFrom;
        private int _RangeRowTo;

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

        public string RangeColumnFrom
        {
            get
            {
                return _RangeColumnFrom;
            }
            set
            {
                _RangeColumnFrom = value;
            }
        }
        public string RangeColumnTo
        {
            get
            {
                return _RangeColumnTo;
            }
            set
            {
                _RangeColumnTo = value;
            }
        }
        public int RangeRowFrom
        {
            get
            {
                return _RangeRowFrom;
            }
            set
            {
                _RangeRowFrom = value;
            }
        }
        public int RangeRowTo
        {
            get
            {
                return _RangeRowTo;
            }
            set
            {
                _RangeRowTo = value;
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
                Range TempRange = _xlWorkSheet.Range[_RangeColumnFrom + _RangeRowFrom.ToString() + ":" + _RangeColumnTo + _RangeRowTo.ToString()];

                TempRange.EntireRow.Delete(Type.Missing);
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
