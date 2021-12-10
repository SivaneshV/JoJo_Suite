using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace JoJoSuite.Library.IO
{
    public  class r2rMoveFile
    {
        //Input local variables       
        private string _sourcepath;
        private string _destinationpath;
        private bool _keepcopy;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";

        //Public Input properties
        public string SourcePath
        {
            get
            {
                return _sourcepath;
            }
            set
            {
                _sourcepath = value;
            }
        }

        public string DestinationPath
        {
            get
            {
                return _destinationpath;
            }
            set
            {
                _destinationpath = value;
            }
        }

        public bool KeepCopy
        {
            get
            {
                return _keepcopy;
            }
            set
            {
                _keepcopy = value;
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
                if (_keepcopy)
                {
                    File.Copy(_sourcepath, _destinationpath);
                }
                else
                {
                    if (Path.GetFileName(_destinationpath)=="")
                    {
                        _destinationpath = _destinationpath + Path.GetFileName(_sourcepath);
                    }
                    File.Move(_sourcepath, _destinationpath);
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
