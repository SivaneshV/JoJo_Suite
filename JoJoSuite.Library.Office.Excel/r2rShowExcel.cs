using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Office.Excel
{
    public class r2rShowExcel
    {
        //Input local variables       
        private string _filename;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";

        //Public Input properties
        public string FileName
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
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
                if (File.Exists(_filename))
                {
                    Process.Start(_filename);
                    _error = false;
                    _errorMsg = "";
                    res = true;
                }
                else
                {
                    _error = true;
                    _errorMsg = "File not found";
                }


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
