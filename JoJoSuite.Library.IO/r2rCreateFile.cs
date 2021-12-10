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
    public class r2rCreateFile
    {
        //Input local variables       
        private string _filename;
        private bool _overwrite;
        private string _content;

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
        public bool Overwrite
        {
            get
            {
                return _overwrite;
            }
            set
            {
                _overwrite = value;
            }

        }
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
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
                if (File.Exists(_filename) && _overwrite == true)
                {
                    File.Delete(_filename);
                    //System.IO.File.WriteAllText(_filename, _content);
                    using (StreamWriter outputFile = new StreamWriter(_filename, true))
                    {
                        outputFile.WriteLine(_content);
                    }
                }
                else if (!File.Exists(_filename))
                {
                    using (StreamWriter outputFile = new StreamWriter(_filename, true))
                    {
                        outputFile.WriteLine(_content);
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
