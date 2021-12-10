using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.IO
{
    public class r2rFileExists
    {
        //Input local variables       
        private string _filename;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private bool _output;

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
        public bool Output
        {
            get
            {
                return _output;
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
                if (File.Exists(_filename))
                {
                    _output = true;
                    _error = false;
                    _errorMsg = "";
                    res = true;
                }
                else
                {
                    _output = false;
                    res = false;
                    _error = true;
                    _errorMsg = "File Not Exists";
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
