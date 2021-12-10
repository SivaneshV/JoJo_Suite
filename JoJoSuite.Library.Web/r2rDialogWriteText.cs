using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Web
{
   public class r2rDialogWriteText
    {
        //Input local variables
        private IWebDriver _webdriver;
        private string _writetext;        
        private int _waitingtime;

        //Output local variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";

        //Public input properties
        public IWebDriver WebDriver
        {
            get
            {
                return _webdriver;
            }

            set
            {
                _webdriver = value;
            }
        }

        public string WriteText
        {
            get
            {
                return _writetext;
            }

            set
            {
                _writetext = value;
            }
        }
      
        public int WaitingTime
        {
            get
            {
                return _waitingtime;
            }

            set
            {
                _waitingtime = value;
            }
        }


        //Public input properties   
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
                IAlert alert = _webdriver.SwitchTo().Alert();
                if (alert != null)
                {
                    alert.SendKeys(_writetext);
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
