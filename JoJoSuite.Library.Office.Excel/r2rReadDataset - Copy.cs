using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using ExcelDataReader;


namespace r2rStudio.Library.Office.Excel
{
    public class r2rDataSetToExcel
    {
        //Input local variables
        private string _filepath;
        private bool _userheaderrow;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private DataSet _ds;

        //Public Input properties
        public string FilePath
        {
            get
            {
                return _filepath;
            }
            set
            {
                _filepath = value;
            }
        }
        public bool UserHeaderRow
        {
            get
            {
                return _userheaderrow;
            }
            set
            {
                _userheaderrow = value;
            }
        }
        public bool UseHeaderRow
        {
            get
            {
                return _userheaderrow;
            }
            set
            {
                _userheaderrow = value;
            }
        }
        //Public output properties
        public DataSet Ds
        {
            get
            {
                return _ds;
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
                using (var stream = File.Open(_filepath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {

                        if (_userheaderrow)
                        {
                            _ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                // Gets or sets a callback to obtain configuration options for a DataTable. 
                                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true,
                                }
                            });
                        }
                        else
                        {
                            _ds = reader.AsDataSet();
                        }

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
