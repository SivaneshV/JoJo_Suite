using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.IO
{
    public class r2rDeleteFolder
    {
        //Input local variables       
        private string _folderpath;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private bool _output;
        //Public Input properties
        public string FolderPath
        {
            get
            {
                return _folderpath;
            }
            set
            {
                _folderpath = value;
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
                if (Directory.Exists(_folderpath))
                {
                    Directory.Delete(_folderpath);                  
                }
                _output = true;
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
