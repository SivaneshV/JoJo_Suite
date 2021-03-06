using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Web
{
    public class r2rCloseBrowser
    {
        //Input local variables
        private IWebDriver _webdriver;


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
                _webdriver.Close();
                _webdriver.Quit();

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
