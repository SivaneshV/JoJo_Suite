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
    public class r2rAggregateField
    {
        //Input local variables
        private DataTable _DataTableInput;        
        private string _AggregateColumn;
        private AggregateColumnType _AggregateColumnType;
        private AggregateType _Aggregation;
        //Output Local Variables
        private dynamic _AggregationResult;
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

       
        public string AggregateColumn
        {
            get
            {
                return _AggregateColumn;
            }
            set
            {
                _AggregateColumn = value;
            }
        }

        public AggregateColumnType ColumnType
        {
            get
            {
                return _AggregateColumnType;
            }
            set
            {
                _AggregateColumnType = value;
            }
        }
        public AggregateType Aggregation
        {
            get
            {
                return _Aggregation;
            }
            set
            {
                _Aggregation = value;
            }
        }
        public enum AggregateType
        {
            Sum,
            Count
        }
        public enum AggregateColumnType
        {
            String,
            Int32,
            Boolean,
            Double,
            Decimal,

        }
        //Public output properties
        public dynamic AggregationResult
        {
            get
            {
                return _AggregationResult;
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

                if (_Aggregation == AggregateType.Sum)
                {
                    if (ColumnType == AggregateColumnType.Decimal)
                    {
                        Decimal DecimalSum = 0;
                        foreach (DataRow row in _DataTableInput.Rows)
                        {
                            DecimalSum += Convert.ToDecimal(row[_AggregateColumn]);
                        }
                        _AggregationResult = DecimalSum;
                    }
                    else if (ColumnType == AggregateColumnType.Double)
                    {
                        Double DoubleSum = 0.0;
                        foreach (DataRow row in _DataTableInput.Rows)
                        {
                            DoubleSum += Convert.ToDouble(row[_AggregateColumn]);
                        }
                        _AggregationResult = DoubleSum;
                    }
                    else if (ColumnType == AggregateColumnType.Int32)
                    {
                        Int64 IntSum = 0;
                        foreach (DataRow row in _DataTableInput.Rows)
                        {
                            IntSum += Convert.ToInt32(row[_AggregateColumn]);
                        }

                        _AggregationResult = IntSum;
                    }
                }
                else if (_Aggregation == AggregateType.Count)
                {
                    if (ColumnType == AggregateColumnType.Decimal)
                    {
                       
                        _AggregationResult = _DataTableInput.Rows.Count;
                    }
                    else if (ColumnType == AggregateColumnType.Double)
                    {
                        _AggregationResult = _DataTableInput.Rows.Count;
                    }
                    else if (ColumnType == AggregateColumnType.Int32)
                    {
                        _AggregationResult = _DataTableInput.Rows.Count;
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
