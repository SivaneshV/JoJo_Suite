using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Web
{
    public class r2rSwitchTo
    {
        //Input local variables
        private IWebDriver _webdriver;
        private string _iframeid;
        private bool _parent;
        private bool _window;
        private bool _iframe;

        private int _waitingtime;

        //Output local variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private string _outputStr;

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

        public string Iframeid
        {
            get
            {
                return _iframeid;
            }

            set
            {
                _iframeid = value;
            }
        }
        public bool Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }
        public bool Window
        {
            get
            {
                return _window;
            }

            set
            {
                _window = value;
            }
        }
        public bool Iframe
        {
            get
            {
                return _iframe;
            }

            set
            {
                _iframe = value;
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
                if (Window == true)
                {
                    if (_parent == true)
                    {
                        _webdriver.Close();
                        _webdriver.SwitchTo().Window(_webdriver.WindowHandles.First());
                    }
                    else if (_parent == false)
                    {
                        _webdriver.SwitchTo().Window(_webdriver.WindowHandles.Last());
                    }
                }
                else if (Iframe == true)
                {
                    if (_parent == true)
                    {
                        _webdriver.SwitchTo().DefaultContent();
                    }
                    else if (_parent == false)
                    {
                        _webdriver.SwitchTo().Frame(_iframeid);
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

        #region Wait
        static bool Wait(IWebDriver parent, int seconds, string sPath)
        {
            bool res = false;

            IWebElement e1 = null;

            for (int i = 0; i < (seconds); i++)
            {
                try
                {
                    e1 = parent.FindElement(By.XPath(sPath));
                    res = true;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);

                    //throw;
                }

                Thread.Sleep(1000);
            }

            return res;
        }
        #endregion
    }
}
