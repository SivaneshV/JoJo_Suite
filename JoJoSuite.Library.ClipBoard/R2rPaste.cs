using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Library.ClipBoard
{
    public class R2rPaste
    {
        //Input local variables
        private string _filePath;
        private bool _txtFile = false;

        private bool _excelFile = false;
        private string _excelSheet;
        private string _cellAddress;

        private bool _web = false;
        private string _xpath;
        private Int32 _waitTime;
        private IWebDriver _webDriver;
        private IWebElement _webElement;

        private string _ControlID;
        private object _sapObj;

        //Output local variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
     

        //Public input properties
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }
        public bool TxtFile
        {
            get
            {
                return _txtFile;
            }
            set
            {
                _txtFile = value;
            }
        }
        public bool ExcelFile
        {
            get
            {
                return _excelFile;
            }
            set
            {
                _excelFile = value;
            }
        }
        public string ExcelSheet
        {
            get
            {
                return _excelSheet;
            }
            set
            {
                _excelSheet = value;
            }
        }
        public string CellAddress
        {
            get
            {
                return _cellAddress;
            }
            set
            {
                _cellAddress = value;
            }
        }
        public bool Web
        {
            get
            {
                return _web;
            }
            set
            {
                _web = value;
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
        public Int32 WaitTime
        {
            get
            {
                return _waitTime;
            }
            set
            {
                _waitTime = value;
            }
        }
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

        public bool DoAction()
        {
            bool res = false;
            try
            {
              
                if (_txtFile == true)
                {
                    Thread thread = new Thread(() => File.WriteAllText(_filePath, Clipboard.GetText()));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();

                }
                else if (_excelFile == true)
                {
                    Microsoft.Office.Interop.Excel.Application appExl = new Microsoft.Office.Interop.Excel.Application();
                    Workbook workbook = appExl.Workbooks.Open(_filePath,
                        Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    Worksheet sheet = workbook.Sheets[_excelSheet];
                    //sheet.Range[_cellAddress].Value= Clipboard.GetText();
                    sheet.Range[_cellAddress].PasteSpecial();
                    workbook.Save();
                    workbook.Close(true, Type.Missing, Type.Missing);
                    workbook = null;
                    appExl.Quit();
                    appExl = null;
                }
                else if (_web == true)
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
                        if (Wait(CommonObj, _waitTime, _xpath))
                        {
                           
                            var txtObj = getSingle(CommonObj, _xpath);
                            txtObj.Clear();
                            //Console.WriteLine(Clipboard.GetText().ToString());
                            System.Threading.Thread.Sleep(2000);
                            txtObj.SendKeys(OpenQA.Selenium.Keys.Control + "V");
                            
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
