using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
namespace JoJoSuite.Library.Office.Excel
{
    public class r2rDeleteColumn
    {
        //Input local variables
        private Worksheet _xlWorksheet;
        private string _xlColumnNoOrName;
        private int _xlCount = 1;
        private string _xlTableName;

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
        public string xlColumnNoOrName
        {
            get
            {
                return _xlColumnNoOrName;
            }
            set
            {
                _xlColumnNoOrName = value;
            }

        }
        public int xlCount
        {
            get
            {
                return _xlCount;
            }
            set
            {
                _xlCount = value;
            }

        }
        public string xlTableName
        {
            get
            {
                return _xlTableName;
            }
            set
            {
                _xlTableName = value;
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
                if (_xlTableName != "")
                {
                    int ListCount = _xlWorksheet.ListObjects.Count;
                    for (int i = 1; i <= ListCount; i++)
                    {
                        if (_xlTableName.ToString().ToLower() == _xlWorksheet.ListObjects[i].Name.ToString().ToLower())
                        {
                            for (int j = 1; j <= _xlCount; j++)
                            {
                                _xlWorksheet.ListObjects[i].ListColumns.Item[xlColumnNoOrName].Delete();
                            }
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 1; j <= _xlCount; j++)
                    {
                        _xlWorksheet.Range[_xlColumnNoOrName].EntireColumn.Delete(XlDeleteShiftDirection.xlShiftToLeft);

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
