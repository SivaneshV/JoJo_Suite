using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.IE;
using System.Threading;
using JoJoSuite.Library.Base;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace JoJoSuite.Library.Web
{
    public class r2rOpenSite
    {
        //Input local variables
        private string _url;
        private string _downloadpath;
        //private string _browser;
        //private r2rBase.r2rWebBrowserType _browser = r2rBase.r2rWebBrowserType.Chrome;
        private string _browser;

        //Output Local Variables
        private bool _error = true;
        private string _errorMsg = "DoAction() method not called";
        private IWebDriver _webdriver;


        //Public Input properties
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        public string DownloadPath
        {
            get
            {
                return _downloadpath;
            }
            set
            {
                _downloadpath = value;
            }
        }
        public string Browser
        {
            get
            {
                return _browser;
            }
            set
            {
                _browser = value;
            }
        }

        //Public output properties
        public IWebDriver WebDriver
        {
            get
            {
                return _webdriver;
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

        public void CloseDriver()
        {
            try
            {
                _webdriver.Close();
                _webdriver.Dispose();
                _webdriver = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // DoAction()

        public bool DoAction()
        {
            bool res = false;
            try
            {
                string drivepath = System.AppDomain.CurrentDomain.BaseDirectory + "Drivers";

                //To create Chrome browser driver and default browser driver
                if (_browser == "Chrome")
                {
                    var chromeOptions = new ChromeOptions();
                    if (_downloadpath != null)
                        chromeOptions.AddUserProfilePreference("download.default_directory", _downloadpath);
                    chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                    chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
                    chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
                    _webdriver = new ChromeDriver(drivepath, chromeOptions);
                    Thread.Sleep(5000);
                    Console.WriteLine(_url);
                    _webdriver.Navigate().GoToUrl(_url);
                    Thread.Sleep(5000);
                    _webdriver.Manage().Window.Maximize();
                }
                //To create firefox dirver
                else if (_browser == "Firefox")
                {
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(drivepath);
                    //service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe"; // May not be necessary
                    FirefoxOptions option = new FirefoxOptions();
                    option.SetPreference("browser.download.folderList", 2);
                    option.SetPreference("browser.download.manager.showWhenStarting", false);
                    option.SetPreference("browser.helperApps.alwaysAsk.force", false);
                    if (_downloadpath != null)
                    {
                        option.SetPreference("browser.download.dir", _downloadpath);
                    }

                    option.SetPreference("browser.helperApps.neverAsk.openFile", "application/vnd.ms-excel");
                    option.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.ms-excel");
                    TimeSpan time = TimeSpan.FromSeconds(10);
                    _webdriver = new FirefoxDriver(service, option, time);
                    _webdriver.Navigate().GoToUrl(_url);
                    Thread.Sleep(5000);
                }
                //To create IE driver
                else if (_browser == "IE")
                {
                    //InternetExplorerOptions options = new InternetExplorerOptions();
                    //options.EnableNativeEvents = true;
                    //options.EnablePersistentHover = true;
                    //_webdriver = new InternetExplorerDriver(drivepath, options);

                    //InternetExplorerOptions options = new InternetExplorerOptions();
                    //options.IgnoreZoomLevel = true;
                    //options.EnableNativeEvents = true;
                    //options.InitialBrowserUrl = "http://localhost";
                    //options.UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Accept;
                    //options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    //_webdriver = new InternetExplorerDriver(drivepath, options);



                    InternetExplorerOptions options = new InternetExplorerOptions();
                    options.EnableNativeEvents = false;
                    options.EnablePersistentHover = true;
                   // options.UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Accept;
                    //options.AcceptInsecureCertificates = true;
                    options.IgnoreZoomLevel = true;
                    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    //options.EnsureCleanSession = true;
                    _webdriver = new InternetExplorerDriver(drivepath, options);

                    Thread.Sleep(5000);
                    _webdriver.Navigate().GoToUrl(_url);
                    Thread.Sleep(5000);
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
