using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace JoJoSuite.Library.Web
{
    public class r2rListSelect
    { //Input local variables
        private IWebDriver _webDriver;
        private IWebElement _webElement;
        private string _xpath;
        private int _waitingTime;
        private bool _deSelect;
        private string _indexes;
        private string _values;
        private string _texts;
        private bool _waitToLoad;

        //Output local variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";

        //Public input properties
        public IWebDriver WebDriver
        {
            get
            {
                return _webDriver;
            }

            set
            {
                _webDriver = value;
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
                return _waitingTime;
            }

            set
            {
                _waitingTime = value;
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
        public bool DeSelect
        {
            get
            {
                return _deSelect;
            }

            set
            {
                _deSelect = value;
            }
        }
        public string Indexes
        {
            get
            {
                return _indexes;
            }

            set
            {
                _indexes = value;
            }
        }
        public string Values
        {
            get
            {
                return _values;
            }

            set
            {
                _values = value;
            }
        }
        public string Texts
        {
            get
            {
                return _texts;
            }

            set
            {
                _texts = value;
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
                if (_webElement != null && _xpath == null)
                {
                    _webElement.Click();
                }
                else
                {
                    dynamic CommonObj = _webDriver;
                    if (CommonObj == null)
                    {
                        CommonObj = _webElement;
                    }
                    if (WaitToload)
                    {
                        WebDriverWait wait = new WebDriverWait(CommonObj, TimeSpan.FromSeconds(_waitingTime));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(_xpath)));
                    }

                    if (Wait(CommonObj, _waitingTime, _xpath))
                    {
                        SelectElement oSelect = getSingle(CommonObj, _xpath);
                        if (_indexes != null)
                        {
                            foreach (string index in _indexes.Split(','))
                            {
                                if (_deSelect == true)
                                {
                                    oSelect.DeselectByIndex(Convert.ToInt32(index));
                                }
                                else
                                {
                                    oSelect.SelectByIndex(Convert.ToInt32(index));
                                }

                            }

                        }
                        else if (_values != null)
                        {
                            foreach (string value in _values.Split(','))
                            {
                                if (_deSelect == true)
                                {
                                    oSelect.DeselectByValue(Convert.ToString(value));
                                }
                                else
                                {
                                    oSelect.SelectByValue(Convert.ToString(value));
                                }

                            }
                        }
                        else if (_texts != null)
                        {
                            foreach (string text in _texts.Split(','))
                            {
                                if (_deSelect == true)
                                {
                                    oSelect.DeselectByText(Convert.ToString(text));
                                }
                                else
                                {
                                    oSelect.SelectByText(Convert.ToString(text));
                                }

                            }
                        }
                        else if (_indexes == null && _values == null && _texts == null && _deSelect == true)
                        {
                            oSelect.DeselectAll();
                        }
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

        #region WaitWebDriver
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

        #region WaitWebElement
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

        #region GetSingleWebDriver
        static SelectElement getSingle(IWebDriver parent, string sPath)
        {
            SelectElement res = null;
            try
            {
                res = new SelectElement(parent.FindElement(By.XPath(sPath)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return res;
        }
        #endregion

        #region GetSingleWebElement
        static SelectElement getSingle(IWebElement parent, string sPath)
        {
            SelectElement res = null;
            try
            {
                res = new SelectElement(parent.FindElement(By.XPath(sPath)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return res;
        }
        #endregion
    }
}
