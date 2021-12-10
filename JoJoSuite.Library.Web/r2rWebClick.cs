using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoJoSuite.Library.Web
{
    public class r2rWebClick
    {
        //Input local variables
        private IWebDriver _webdriver;
        private IWebElement _webElement;
        private bool _waitToLoad;
        private string _xpath;
        private int _waitingtime;
        private bool _error = true;
        //Output local variables
        
        private string _errorMsg = "DoAction() method not called";
        //private string _outputStr;

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
        public IWebElement WebElement
        {
            get
            {
                return _webElement;
            }

            set
            {
                _webElement = value;
            }
        }
        public string Xpath
        {
            get
            {
                return _xpath;
            }

            set
            {
                _xpath = value;
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
        public bool WaitToload
        {
            get
            {
                return _waitToLoad;
            }

            set
            {
                _waitToLoad = value;
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
                if (_webElement!=null &&  _xpath==null)
                {
                    _webElement.Click();
                }
                else
                {
                    dynamic CommonObj = _webdriver;
                    if (CommonObj == null)
                    {
                        CommonObj = _webElement;
                    }
                    if (WaitToload)
                    {
                        WebDriverWait wait = new WebDriverWait(CommonObj, TimeSpan.FromSeconds(_waitingtime));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xpath)));
                    }
                    if (Wait(CommonObj, _waitingtime, _xpath))
                    {
                        var clickObj = getSingle(CommonObj, _xpath);
                        clickObj.Click();
                        _error = false;
                        _errorMsg = "";
                        res = true;
                    }
                    else
                    {
                        _error = true;
                        _errorMsg = "Element not found";
                        res = false;
                    }
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

        #region WaitIWebDriver
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

        #region WaitIWebElement
        static bool Wait(IWebElement parent, int seconds, string sPath)
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

        #region getSingleIWebDriver
        static IWebElement getSingle(IWebDriver parent, string sPath)
        {
            IWebElement res = null;
            try
            {
                res = parent.FindElement(By.XPath(sPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);

                //throw;
            }
            return res;
        }
        #endregion
        #region getSingleIWebElement
        static IWebElement getSingle(IWebElement parent, string sPath)
        {
            IWebElement res = null;
            try
            {
                res = parent.FindElement(By.XPath(sPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);

                //throw;
            }
            return res;
        }
        #endregion
    }
}
