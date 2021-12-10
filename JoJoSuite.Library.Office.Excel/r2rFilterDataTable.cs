using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using ExcelDataReader;


namespace JoJoSuite.Library.Office.Excel
{
    public class r2rFilterDataTable
    {
        //Input local variables
        private DataTable _DataTableInput;
        private bool _useheaderrow;
        private string _FilterColumn;
        private FilterColumnType _FilterColumnType;
        private string _FilterValue;
        private FilterCondtion _Condition;
        //Output Local Variables
        private DataTable _DataTableOutput;
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private DataSet _ds;

        //Public Input properties
        public DataTable DataTableInput
        {
            get
            {
                return _DataTableInput;
            }
            set
            {
                _DataTableInput = value;
            }
        }

        public bool UseHeaderRow
        {
            get
            {
                return _useheaderrow;
            }
            set
            {
                _useheaderrow = value;
            }
        }
        public string FilterColumn
        {
            get
            {
                return _FilterColumn;
            }
            set
            {
                _FilterColumn = value;
            }
        }
        public string FilterValue
        {
            get
            {
                return _FilterValue;
            }
            set
            {
                _FilterValue = value;
            }
        }
        public FilterCondtion Condition
        {
            get
            {
                return _Condition;
            }
            set
            {
                _Condition = value;
            }
        }
        public FilterColumnType ColumnType
        {
            get
            {
                return _FilterColumnType;
            }
            set
            {
                _FilterColumnType = value;
            }
        }
        public enum FilterCondtion
        {
            And,
            Or
        }
        public enum FilterColumnType
        {
            String,
            Int32,
            Boolean,
            Double,
            Decimal,

        }
        //Public output properties
        public DataTable ResultRecordSet
        {
            get
            {
                return _DataTableOutput;
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



        public async Task<bool> DoAction()
        {
            bool res = false;
            try
            {

                if (_Condition == FilterCondtion.And)
                {
                    if (ColumnType == FilterColumnType.String)
                    {
                        foreach (var value in FilterValue.Split(';'))
                        {
                            if (_DataTableInput.AsEnumerable().Where(x => x[_FilterColumn].ToString().Trim() == value.Trim()).Any())
                            {
                                _DataTableInput = _DataTableInput.AsEnumerable().Where(x => x[_FilterColumn].ToString().Trim() == value.Trim()).CopyToDataTable();
                            }
                            else
                            {
                                _DataTableInput = _DataTableInput.Clone();
                            }
                        }
                    }
                    else if (ColumnType == FilterColumnType.Boolean)
                    {
                        bool value = Convert.ToBoolean(_FilterValue);
                        _DataTableInput = _DataTableInput.AsEnumerable().Where(x => x.Field<bool>(_FilterColumn) == value).CopyToDataTable();
                    }
                    else if (ColumnType == FilterColumnType.Decimal)
                    {
                        foreach (var value in FilterValue.Split(';'))
                        {
                            var decimalvalue = Convert.ToDecimal(value);
                            _DataTableInput = _DataTableInput.AsEnumerable().Where(x => x.Field<decimal>(_FilterColumn) == decimalvalue).CopyToDataTable();
                        }
                    }
                    else if (ColumnType == FilterColumnType.Double)
                    {
                        foreach (var value in FilterValue.Split(';'))
                        {
                            var doublevalue = Convert.ToDouble(value);
                            _DataTableInput = _DataTableInput.AsEnumerable().Where(x => x.Field<double>(_FilterColumn) == doublevalue).CopyToDataTable();
                        }
                    }
                    else if (ColumnType == FilterColumnType.Int32)
                    {
                        foreach (var value in FilterValue.Split(';'))
                        {
                            var Intvalue = Convert.ToInt32(value);
                            _DataTableInput = _DataTableInput.AsEnumerable().Where(x => x.Field<double>(_FilterColumn) == Intvalue).CopyToDataTable();
                        }
                    }
                }
                else if (_Condition == FilterCondtion.Or)
                {
                    if (ColumnType == FilterColumnType.String)
                    {
                        var stringList = _FilterValue.Split(';');
                        _DataTableInput = _DataTableInput.AsEnumerable().Where(x => stringList.Any(y => y == x.Field<string>(_FilterColumn))).CopyToDataTable();
                    }
                    else if (ColumnType == FilterColumnType.Boolean)
                    {
                        bool value = Convert.ToBoolean(_FilterValue);
                        _DataTableInput = _DataTableInput.AsEnumerable().Where(x => x.Field<bool>(_FilterColumn) == value).CopyToDataTable();
                    }
                    else if (ColumnType == FilterColumnType.Decimal)
                    {
                        List<decimal> decimalvalue = new List<decimal>();
                        foreach (var value in FilterValue.Split(';'))
                        {
                            decimalvalue.Add(Convert.ToDecimal(value));
                        }
                        _DataTableInput = _DataTableInput.AsEnumerable().Where(x => decimalvalue.Any(y => y == x.Field<decimal>(_FilterColumn))).CopyToDataTable();
                    }
                    else if (ColumnType == FilterColumnType.Double)
                    {
                        List<double> doublevalue = new List<double>();
                        foreach (var value in FilterValue.Split(';'))
                        {
                            doublevalue.Add(Convert.ToDouble(value));
                        }
                        _DataTableInput = _DataTableInput.AsEnumerable().Where(x => doublevalue.Any(y => y == x.Field<double>(_FilterColumn))).CopyToDataTable();
                    }
                    else if (ColumnType == FilterColumnType.Int32)
                    {
                        List<Int32> intvalue = new List<Int32>();
                        foreach (var value in FilterValue.Split(';'))
                        {
                            intvalue.Add(Convert.ToInt32(value));
                        }
                        _DataTableInput = _DataTableInput.AsEnumerable().Where(x => intvalue.Any(y => y == x.Field<int>(_FilterColumn))).CopyToDataTable();
                    }
                }
                _DataTableOutput = _DataTableInput;
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
