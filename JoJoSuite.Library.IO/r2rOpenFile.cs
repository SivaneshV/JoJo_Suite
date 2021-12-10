using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.IO
{
    public class r2rOpenFile
    {
        //Input local variables       
        private string _filename;
        private bool _splitlines;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _result;
        private string[] _resultarray;

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
        public bool SplitLines
        {
            get
            {
                return _splitlines;
            }
            set
            {
                _splitlines = value;
            }

        }

        //Public output properties
        public string Result
        {
            get
            {
                return _result;
            }
        }
        public string[] Resultarray
        {
            get
            {
                return _resultarray;
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
                string filecontent  = File.ReadAllText(_filename, Encoding.UTF8);
                _resultarray = null;
                _result = null;
                if (_splitlines==true)
                {
                    _resultarray = filecontent.Split('\n');                   

                }
                else if (_splitlines==false)
                {
                    _result = filecontent;
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
