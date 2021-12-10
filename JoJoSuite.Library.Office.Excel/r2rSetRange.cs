using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
namespace JoJoSuite.Library.Office.Excel
{
    public class r2rSetRange
    {
        //Input local variables
        private Worksheet _worksheetobject;
        private string _startcelladdress;
        private string _endcelladdress;
        private dynamic _setdatalist;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";



        //Public Input properties
        public Worksheet Worksheet
        {
            get
            {
                return _worksheetobject;
            }
            set
            {
                _worksheetobject = value;
            }

        }
        public string StartCellAddress
        {
            get
            {
                return _startcelladdress;
            }
            set
            {
                _startcelladdress = value;
            }

        }
        public string EndCellAddress
        {
            get
            {
                return _endcelladdress;
            }
            set
            {
                _endcelladdress = value;
            }

        }
        public dynamic setDataList
        {
            get
            {
                return _setdatalist;
            }
            set
            {
                _setdatalist = value;
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
                ((Range)_worksheetobject.get_Range(_startcelladdress + ":" + _endcelladdress, Type.Missing)).Value2 = _setdatalist;

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
