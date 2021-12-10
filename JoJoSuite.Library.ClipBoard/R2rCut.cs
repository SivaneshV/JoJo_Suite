using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.ClipBoard
{
    public class R2rCut
    {
        //Output local variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _outputStr;

        //Public input properties
       

        //Public input properties
        public string OutputStr
        {
            get
            {
                return _outputStr;
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

        public bool DoAction()
        {
            bool res = false;
            try
            {        

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
