using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
namespace JoJoSuite.Library.Office.Excel
{
    public class r2rGetRange
    {
        //Input local variables
        private Worksheet _worksheetobject;
        private string _startcelladdress;
        private string _endcelladdress;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private dynamic _getdatalist;


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

        //Public output properties
        public dynamic GetDataList
        {
            get
            {
                return _getdatalist;
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
                _getdatalist = ((Range)_worksheetobject.get_Range(_startcelladdress + ":" + _endcelladdress, Type.Missing)).Value2;

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
