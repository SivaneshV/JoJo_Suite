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
    public class r2rCopyPasteRange
    {
        //Input local variables
        private Worksheet _xlWorkSheetSource;
        private Worksheet _xlWorkSheetDestination;
        private string _SourceColumnFrom;
        private string _SourceColumnTo;
        private int _SourceRowFrom;
        private int _SourceRowTo;
        private string _DestColumnFrom;
        private string _DestColumnTo;
        private int _DestRowFrom;
        private int _DestRowTo;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private bool _isSuccess;


        //Public Input properties
        public Worksheet xlWorkSheetSource
        {
            get
            {
                return _xlWorkSheetSource;
            }
            set
            {
                _xlWorkSheetSource = value;
            }

        }
        public Worksheet xlWorkSheetDestination
        {
            get
            {
                return _xlWorkSheetDestination;
            }
            set
            {
                _xlWorkSheetDestination = value;
            }

        }
        public string SourceColumnFrom
        {
            get
            {
                return _SourceColumnFrom;
            }
            set
            {
                _SourceColumnFrom = value;
            }
        }
        public string SourceColumnTo
        {
            get
            {
                return _SourceColumnTo;
            }
            set
            {
                _SourceColumnTo = value;
            }
        }
        public int SourceRowFrom
        {
            get
            {
                return _SourceRowFrom;
            }
            set
            {
                _SourceRowFrom = value;
            }
        }
        public int SourceRowTo
        {
            get
            {
                return _SourceRowTo;
            }
            set
            {
                _SourceRowTo = value;
            }
        }

        public string DestColumnFrom
        {
            get
            {
                return _DestColumnFrom;
            }
            set
            {
                _DestColumnFrom = value;
            }
        }
        public string DestColumnTo
        {
            get
            {
                return _DestColumnTo;
            }
            set
            {
                _DestColumnTo = value;
            }
        }
        public int DestRowFrom
        {
            get
            {
                return _DestRowFrom;
            }
            set
            {
                _DestRowFrom = value;
            }
        }
        public int DestRowTo
        {
            get
            {
                return _DestRowTo;
            }
            set
            {
                _DestRowTo = value;
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
                var Rowindex = 0;
                Range source = _xlWorkSheetSource.Range[_SourceColumnFrom + _SourceRowFrom.ToString() + ":" + _SourceColumnTo + _SourceRowTo.ToString() + ""];
                Range dest = _xlWorkSheetDestination.Range[_DestColumnFrom + _DestRowFrom.ToString() + ":" + _DestColumnTo + _DestRowTo.ToString() + ""];

                source.Copy(Type.Missing);
                dest.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteAll, Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

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
