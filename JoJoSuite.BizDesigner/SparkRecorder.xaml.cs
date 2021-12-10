using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for SparkRecorder.xaml
    /// </summary>
    public partial class SparkRecorder : Window
    {
        #region [Recorder Variables]
        public IWebDriver baseDriver;
        public ChromeDriver Driver = null;
        //public IWebDriver Driver = null;
        bool isRecoding = false;
        public RecordingStatus isRecordingPausedPrevState = RecordingStatus.Recording;
        int recordPauseHitCount = 1;
        public RecordedElements recordedElements;
        public ImageAnimationController recordingImageController = null;
        public string Url = string.Empty;
        public string BotId = string.Empty;
        public string SessionId = string.Empty;
        //public EventCapturingWebDriver
        r2rMsgBox msgBox;
        public r2rRecorderActivity lstRecoderActs = new r2rRecorderActivity();
        public string WebConnector;
        public List<string> VariableList = new List<string>();
        public string URL;
        public System.Web.Http.SelfHost.HttpSelfHostServer server = null;
        #endregion

        private string _TextBoxRequestMsg;
        public string TextBoxRequestMsg
        {
            get { return txtUrl.Text; }
            set
            {
                _TextBoxRequestMsg = value;
                Dispatcher.Invoke(() => txtUrl.Text = _TextBoxRequestMsg);
            }
        }

        private Point lastPoint;
        public SparkRecorder()
        {
            InitializeComponent();

            msgBox = new r2rMsgBox(this);

        }

        public SparkRecorder(string Url, string BotId, string SessionId, bool _IsRoboticUser = false)
        {
            InitializeComponent();

            msgBox = new r2rMsgBox(this);
            this.Url = Url;
            this.BotId = BotId;
            this.SessionId = SessionId;
            AppSettings.IsRoboticUser = _IsRoboticUser;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            drpVariables.ItemsSource = VariableList;

            if(VariableList.Count == 1)
            {
                drpVariables.SelectedIndex = 0;
            }

            Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            ClearAllRecordedElements();
            CleanScreenshotDirectory();

            //recordingImageController = ImageBehavior.GetAnimationController(recordingImage);

            Presenters.MainWindowPresenter = this;
            recordedElements = new RecordedElements();
            StartApi();

            txtUrl.Text = Url;


            Task.Run(() =>
            {
                Thread.Sleep(500);
                if (!string.IsNullOrEmpty(Url))
                {
                    Dispatcher.Invoke(() =>
                    {
                        Thread.Sleep(1000);
                        Image_MouseUp(null, null);
                    });
                }
            });

            if (!CheckSplashScreen())
            {
                RecorderIntro splashScreen = new RecorderIntro();
                splashScreen.ShowDialog();
            }
        }

        private bool CheckSplashScreen()
        {
            bool displaySplash = false;
            try
            {
                string rootPath = Environment.CurrentDirectory + "//app.cfg";

                if (!(File.Exists(rootPath)))
                {
                    var file = File.Create(rootPath);
                    try
                    {
                        file.Close();
                        file.Dispose();
                    }
                    catch { }
                }

                var splashData = File.ReadAllText(rootPath);

                if (splashData.ToLower().Trim().Contains("true"))
                {
                    displaySplash = true;
                }
            }
            catch (Exception ex)
            {

            }
            return displaySplash;
        }

        private void btnWinMin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            this.WindowState = WindowState.Minimized;
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            r2rMsgBoxResult mRes = msgBox.Show("Are you sure want to exit?", "Exit", r2rMsgBoxButtons.OkCancel);

            if (mRes == r2rMsgBoxResult.No)
            {
                //this.isClose = true;
                this.Hide();
            }
            else if (mRes == r2rMsgBoxResult.Ok)
            {
                //SaveChanges();
                //this.isClose = true;

                lblStatus.Content = "Please wait...";

                Dispatcher.Invoke(() =>
                {
                    AppSettings.recordingStatus = RecordingStatus.Paused;

                    var fileName = System.Configuration.ConfigurationSettings.AppSettings["RsrJsonFile"].ToString();
                    SavePageObjectToFileAsJson(fileName, SessionId, BotId);

                    if (Driver != null)
                    {
                        try
                        {
                            try
                            {
                                while (true)
                                {
                                    Driver.Close();
                                }
                            }
                            catch { }

                            Driver.Dispose();
                        }
                        catch { }
                    }

                    server.CloseAsync().Wait();
                    server.Dispose();

                    //Environment.Exit(0);

                    this.Hide();
                });

                //imgShutdown_MouseLeftButtonUp(null, null);

                //this.Hide();
            }
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

            //if (e.ClickCount == 2)
            //{

            //    if (this.WindowState == WindowState.Normal)
            //    {
            //        System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
            //        System.Windows.Forms.Screen crScreen = System.Windows.Forms.Screen.FromPoint(pt);

            //        if (crScreen.Primary)
            //        {
            //            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            //            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //        }
            //        else
            //        {
            //            MaxHeight = double.PositiveInfinity;
            //            MaxWidth = double.PositiveInfinity;
            //        }

            //        this.WindowState = WindowState.Maximized;
            //        //btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_restore01.png", UriKind.Relative));
            //        dpMain.Margin = new Thickness(6);
            //    }
            //    else
            //    {
            //        MaxHeight = double.PositiveInfinity;
            //        MaxWidth = double.PositiveInfinity;

            //        this.WindowState = WindowState.Normal;
            //        //btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_max01.png", UriKind.Relative));
            //        dpMain.Margin = new Thickness(0);
            //    }
            //}
        }
        private void ClearAllRecordedElements()
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch { }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                lastPoint = new Point(e.GetPosition(this).X, e.GetPosition(this).Y);
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Left += e.GetPosition(this).X - lastPoint.X;
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isRecoding && AppSettings.recordingStatus != RecordingStatus.Paused)
            {
                startRecord();
            }
            else
            {
                AppSettings.recordingStatus = RecordingStatus.Recording;

                refreshUI();
            }
        }

        private void refreshRecordedElementObj()
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                if (recordedElements.Elements == null)
                    recordedElements.Elements = new List<RecordedElement>();

                if (File.Exists(fileName))
                {
                    var jsonData = System.IO.File.ReadAllText(fileName);
                    // De-serialize to object or create new list
                    recordedElements = JsonConvert.DeserializeObject<RecordedElements>(jsonData)
                                          ?? new RecordedElements();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SaveRecordedElement(RecordedElement element)
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                var fileName = System.IO.Path.Combine(path, "RecordedElements.json");

                if (!File.Exists(fileName))
                {
                    var file = File.Create(fileName);
                    file.Close();
                    File.WriteAllText(fileName, "{\"Elements\":[]}");
                }

                if (File.Exists(fileName))
                {
                    var jsonData = System.IO.File.ReadAllText(fileName);
                    // De-serialize to object or create new list
                    recordedElements = JsonConvert.DeserializeObject<RecordedElements>(jsonData)
                                          ?? new RecordedElements();

                    if (recordedElements.Elements == null)
                        recordedElements.Elements = new List<RecordedElement>();

                    if (recordedElements.Elements.Count > 0)
                    {
                        var prevEl = recordedElements.Elements[recordedElements.Elements.Count - 1];
                        if (element.ElementAction == prevEl.ElementAction && element.ElementCodeName == prevEl.ElementCodeName
                            && element.ElementCssSelector == prevEl.ElementCssSelector && element.ElementXPath == prevEl.ElementXPath
                            && element.ElementValue == prevEl.ElementValue && element.ElementAction != "click")
                        {
                            try
                            {
                                IJavaScriptExecutor js = Presenters.MainWindowPresenter.Driver as IJavaScriptExecutor;
                                js.ExecuteScript("if (window.parent.parent.parent.parent.parent.document.getElementById('recordLoading')) window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = 'none';");
                            }
                            catch { }

                            return;
                        }
                        else if (element.ElementAction == prevEl.ElementAction && element.ElementCodeName == prevEl.ElementCodeName
                            && element.ElementCssSelector == prevEl.ElementCssSelector && element.ElementXPath == prevEl.ElementXPath
                            && element.ElementValue != prevEl.ElementValue)
                        {
                            recordedElements.Elements.Where(el => el.CaptureCount == prevEl.CaptureCount).FirstOrDefault().ElementValue = element.ElementValue;

                            // Update json data string
                            jsonData = JsonConvert.SerializeObject(recordedElements);
                            System.IO.File.WriteAllText(fileName, jsonData);

                            try
                            {
                                IJavaScriptExecutor js = Presenters.MainWindowPresenter.Driver as IJavaScriptExecutor;
                                js.ExecuteScript("if (window.parent.parent.parent.parent.parent.document.getElementById('recordLoading')) window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = 'none';");
                            }
                            catch { }


                            return;
                        }
                        else if (element.ElementAction.ToLower().Contains("type") && prevEl.ElementAction.ToLower().Contains("type")
                            && element.ElementXPath == prevEl.ElementXPath && element.ElementCssSelector == prevEl.ElementCssSelector
                            && element.ElementCodeName == prevEl.ElementCodeName && element.ElementValue == prevEl.ElementValue)
                        {
                            try
                            {
                                IJavaScriptExecutor js = Presenters.MainWindowPresenter.Driver as IJavaScriptExecutor;
                                js.ExecuteScript("if (window.parent.parent.parent.parent.parent.document.getElementById('recordLoading')) window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = 'none';");
                            }
                            catch { }


                            return;
                        }
                    }

                    //IFRAME
                    if (!string.IsNullOrEmpty(element.FrameId))
                    {
                        element.FrameIdOrName = element.FrameId;
                        element.FrameIdOrNameType = "id";
                    }

                    // Add any new element
                    if (recordedElements.Elements == null)
                        recordedElements.Elements = new List<RecordedElement>();

                    if (recordedElements.Elements.Count > 0)
                        element.CaptureCount = recordedElements.Elements.Max(el => el.CaptureCount) + 1;
                    else
                        element.CaptureCount = 1;

                    if (Driver != null)
                    {
                        var driverTitleF = Driver.Title.ToString().ToLower().Trim().Replace(" ", "");
                        var elTitleF = (element.WindowTitle != null ? element.WindowTitle : "").ToString().ToLower().Trim().Replace(" ", "");
                        if (driverTitleF == elTitleF)
                        {
                            element.WindowId = Driver.CurrentWindowHandle;
                        }
                        else
                        {
                            foreach (var item in Driver.WindowHandles)
                            {
                                Driver.SwitchTo().Window(item);

                                var driverTitle = Driver.Title.ToString().ToLower().Trim().Replace(" ", "");
                                var elTitle = (element.WindowTitle != null ? element.WindowTitle : "").ToString().ToLower().Trim().Replace(" ", "");
                                if (driverTitle == elTitle)
                                {
                                    Driver.SwitchTo().DefaultContent();
                                    try
                                    {
                                        Driver.SwitchTo().Frame(0);
                                    }
                                    catch { }
                                    break;
                                }
                            }
                        }
                        //Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                        element.ElementSelectedPath = element.ElementXPath;
                        element.ElementPaths = new List<string>() { element.ElementXPath, element.ElementCssSelector };
                        element.WindowId = Driver.CurrentWindowHandle;
                        element.WindowTitle = Driver.Title;

                    }

                    recordedElements.Elements.Add(element);

                    // Update json data string
                    jsonData = JsonConvert.SerializeObject(recordedElements);
                    System.IO.File.WriteAllText(fileName, jsonData);

                    try
                    {
                        var welcome = new System.Windows.Media.MediaPlayer();
                        welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/Recorded.mp3", UriKind.Relative));
                        welcome.Play();
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }

            try
            {
                IJavaScriptExecutor js = Presenters.MainWindowPresenter.Driver as IJavaScriptExecutor;
                js.ExecuteScript("if (window.parent.parent.parent.parent.parent.document.getElementById('recordLoading')) window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = 'none';");
            }
            catch { }
            //Thread.Sleep(100);
            try
            {
                //IWebElement ele = Driver.FindElement(By.XPath(element.ElementXPath));

                //CaptureElementScreenShot(ele, element.CaptureCount.ToString());
                //var img = GetElementScreenShot(Driver, ele);
                //var screenshotFileName = System.IO.Path.Combine(System.IO.Path.Combine(Environment.CurrentDirectory, "Screenshots"), "El_" + element.CaptureCount.ToString() + ".png");
                //img.Save(screenshotFileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {

            }
        }

        public System.Drawing.Image CaptureElementScreenShot(IWebElement element, string CaptureCount)
        {
            var filename = Environment.CurrentDirectory + "\\Screenshots\\" + "El_" + CaptureCount + ".jpeg";
            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var imgst = System.Drawing.Image.FromStream(new MemoryStream(screenshot.AsByteArray)) as System.Drawing.Bitmap;
            var bitmap_img = imgst.Clone(new System.Drawing.Rectangle(element.Location, element.Size), imgst.PixelFormat);
            bitmap_img.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);

            System.Drawing.Image img = System.Drawing.Bitmap.FromFile(filename);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle();

            if (element != null)
            {
                // Get the Width and Height of the WebElement using
                int width = element.Size.Width;
                int height = element.Size.Height;

                // Get the Location of WebElement in a Point.
                // This will provide X & Y co-ordinates of the WebElement
                System.Drawing.Point p = element.Location;

                // Create a rectangle using Width, Height and element location
                rect = new System.Drawing.Rectangle(p.X, p.Y, width, height);
            }

            // croping the image based on rect.
            System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(img);
            var cropedImag = bmpImage.Clone(rect, bmpImage.PixelFormat);

            return cropedImag;
        }

        public System.Drawing.Bitmap GetElementScreenShot(IWebDriver driver, IWebElement element)
        {
            Screenshot sc = ((ITakesScreenshot)driver).GetScreenshot();
            var img = System.Drawing.Image.FromStream(new MemoryStream(sc.AsByteArray)) as System.Drawing.Bitmap;
            return img.Clone(new System.Drawing.Rectangle(element.Location, element.Size), img.PixelFormat);
        }

        #region [Private Recorder Methods]
        private void startRecord()
        {
            try
            {
                if (AppSettings.recordingStatus != RecordingStatus.Paused)
                {
                    if (!string.IsNullOrEmpty(txtUrl.Text.Trim()))
                    {
                        if (drpVariables.SelectedIndex >= 0)
                        {
                            bool result = Uri.IsWellFormedUriString(txtUrl.Text.Trim(), UriKind.Absolute);

                            if (result)
                            {
                                URL = txtUrl.Text.Trim();

                                Dispatcher.Invoke(() =>
                                {
                                    txtUrl.IsReadOnly = true;
                                    lblStatus.Content = "Please wait...";
                                    var image = new BitmapImage();
                                    image.BeginInit();
                                    image.UriSource = new Uri(@"/Images/Recording.gif", UriKind.Relative);
                                    image.EndInit();
                                    ImageBehavior.SetAnimatedSource(recordingImage, image);
                                    recordingImageController = ImageBehavior.GetAnimationController(recordingImage);
                                    btnPause.IsEnabled = true;
                                    btnRecord.IsEnabled = false;
                                });

                                Task.Run(() =>
                                {

                                    isRecoding = true;
                                    AppSettings.recordingStatus = RecordingStatus.Recording;

                                    StartEmbededWebDriver("chrome");

                                    Thread.Sleep(1000);

                                    Dispatcher.Invoke(() => Driver.Navigate().GoToUrl(txtUrl.Text));

                                    Thread.Sleep(1000);

                                    Dispatcher.Invoke(() =>
                                    {
                                        SaveRecordedElement(new RecordedElement { ElementValue = txtUrl.Text, ElementAction = "openurl" });
                                    });
                                //StartApi();

                                IJavaScriptExecutor js = Driver as IJavaScriptExecutor;

                                    js.ExecuteScript("window.parent.parent.parent.parent.parent.document.isRecordingPaused = false;");
                                    js.ExecuteScript("window.postMessage({'type': 'isRecordingPaused', 'jsonData': false}, '*');");

                                    Dispatcher.Invoke(() =>
                                    {
                                        lblStatus.Content = "Recording...";
                                    });

                                    refreshUI();
                                });
                            }
                            else
                            {
                                MessageBox.Show("Please enter valid URL to start recording. URL should be starts with http or https.", "Spark Recorder");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select connector.", "Spark Recorder");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid URL to start recording", "Spark Recorder");
                    }
                }
                else
                {
                    //Have to start visual search
                    AppSettings.recordingStatus = RecordingStatus.Recording;
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri("Images\\Recording.gif");
                    image.EndInit();
                    ImageBehavior.SetAnimatedSource(recordingImage, image);
                }
            }
            catch (Exception ex)
            {
                recordingImage.Source = null;
                lblStatus.Content = "Failed. Try Again.";
            }
        }

        private void StartEmbededWebDriver(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                //caps = DesiredCapabilities.Firefox();
                //caps = ConfigureRemoteWebdriverCapabilities(caps, browserOptions.BrowserProfile, isRemoteDriver);
                //return new FirefoxDriver(caps);
                //return null;
                case "chrome": //browser_Chrome

                    string drivepath = System.AppDomain.CurrentDomain.BaseDirectory + "Drivers";

                    var driverService = ChromeDriverService.CreateDefaultService(drivepath);
                    driverService.HideCommandPromptWindow = true;
                    ChromeOptions options = new ChromeOptions();
                    //options.BinaryLocation = ;

                    var ExtensionsDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Extensions\\";
                    var FullPath = System.IO.Path.Combine(ExtensionsDirectory, "Chrome.crx");
                    options.AddExtension(FullPath);
                    options.AddArguments("--disable-web-security");
                    options.AddArguments("--enable-local-storage");
                    //options.BinaryLocation = @"C:\Program Files (x86)\Chrome\79\chrome.exe";
                    var chromePath = System.IO.Path.Combine(Environment.CurrentDirectory, "79");
                    chromePath = System.IO.Path.Combine(chromePath, "chrome.exe");
                    //options.BinaryLocation = chromePath;

                    Driver = new ChromeDriver(driverService, options);
                    Driver.Manage().Window.Maximize();

                    //baseDriver = new ChromeDriver(driverService, options);
                    //Driver = baseDriver;
                    //using (Driver = new ChromeDriver(driverService, options))
                    //{
                    //    //Driver.ElementClickCaptured += Driver_ElementClickCaptured;
                    //    //Driver.ElementRightClickCaptured += Driver_ElementRightClickCaptured;
                    //    //Driver.ElementDoubleClickCaptured += Driver_ElementDoubleClickCaptured;
                    //    //Driver.ElementMouseOverCaptured += Driver_ElementMouseOverCaptured;
                    //    //Driver.ElementMouseLeaveCaptured += Driver_ElementMouseLeaveCaptured;
                    //    //Driver.ElementKeyPressCaptured += Driver_ElementKeyPressCaptured;

                    //    Driver.Manage().Window.Maximize();
                    //}

                    break;
                    //return Driver;

                    ChromeOptions chromeOptions = new ChromeOptions();

                    #region Extension
                    //var ExtensionsDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    //var FullPath = ExtensionsDirectory + "Chrome.crx";
                    //chromeOptions.AddExtension(FullPath);
                    #endregion

                    chromeOptions.AddArguments("--disable-web-security");
                    chromeOptions.AddArguments("--enable-local-storage");
                //chromeOptions.BinaryLocation = @"C:\Program Files (x86)\Chrome\79\chrome.exe";
                //chromeOptions.AddArguments("--kiosk");
                //chromeOptions.AddArguments("--headless");
                //return new ChromeDriver(driverService, chromeOptions);

                case "internetexplorer":
                //return new InternetExplorerDriver(new InternetExplorerOptions() { IntroduceInstabilityByIgnoringProtectedModeSettings = true });
                //return null;
                //case WebDriverOptions.browser_PhantomJS:
                //    return new PhantomJSDriver();
                //case WebDriverOptions.browser_Safari:
                //    return new SafariDriver();
                default:
                    throw new ArgumentException(String.Format(@"<{0}> was not recognized as supported browser. This parameter is case sensitive", browserName),
                                                "WebDriverOptions.BrowserProfile.ActivationBrowserName");
            }
        }

        #endregion

        #region [Internal Methods]
        internal void RefreshRecordingUIDiv()
        {
            try
            {
                if (Driver != null)
                {
                    if (isRecordingPausedPrevState != AppSettings.recordingStatus)
                    {
                        if (AppSettings.recordingStatus != RecordingStatus.Recording)
                        {
                            IJavaScriptExecutor js = Driver as IJavaScriptExecutor;

                            js.ExecuteScript("window.parent.parent.parent.parent.parent.document.getElementById('content') != null ? window.parent.parent.parent.parent.parent.document.getElementById('content').innerText = 'Paused  ' : '';");
                            js.ExecuteScript("window.parent.parent.parent.parent.parent.document.getElementById('circle') != null ? window.parent.parent.parent.parent.parent.document.getElementById('circle').style = 'visibility: hidden;' : '';");
                            js.ExecuteScript("window.parent.parent.parent.parent.parent.document.isRecordingPaused = true");
                            js.ExecuteScript("window.postMessage({'type': 'isRecordingPaused', 'jsonData': true}, '*');");
                        }
                        else
                        {
                            IJavaScriptExecutor js = Driver as IJavaScriptExecutor;

                            js.ExecuteScript("window.parent.parent.parent.parent.parent.document.getElementById('content') != null ? window.parent.parent.parent.parent.parent.document.getElementById('content').innerText = 'Recording...' : '';");
                            js.ExecuteScript("window.parent.parent.parent.parent.parent.document.getElementById('circle') != null ? window.parent.parent.parent.parent.parent.document.getElementById('circle').style = 'visibility: visible;' : '';");
                            js.ExecuteScript("window.parent.parent.parent.parent.parent.document.isRecordingPaused = false");
                            js.ExecuteScript("window.postMessage({'type': 'isRecordingPaused', 'jsonData': false}, '*');");
                        }

                        isRecordingPausedPrevState = AppSettings.recordingStatus;

                        recordPauseHitCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                //MyLog.Exception(ex);
                //throw;
            }
        }

        internal void StartApi()
        {
            try
            {
                var config = new System.Web.Http.SelfHost.HttpSelfHostConfiguration("http://localhost:47581");// + FreeTcpPort());

                config.MessageHandlers.Add(new CustomHeaderHandler());

                config.Routes.MapHttpRoute(
                                              name: "default",
                                              routeTemplate: "api/{controller}/{id}",
                                              defaults: new { controller = "Command", id = RouteParameter.Optional }
                                            );

                server = new System.Web.Http.SelfHost.HttpSelfHostServer(config);
                server.OpenAsync().Wait();
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("already exists"))
                    //MyLog.Exception(ex);
                    throw;
            }
        }

        #endregion

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var isManualPause = true;
                if (AppSettings.recordingStatus == RecordingStatus.Recording)
                {
                    ImgPause_MouseLeftButtonUp(null, null);
                    isManualPause = false;
                }

                RecorderSettings settings = new RecorderSettings(Driver);
                settings.ShowDialog();

                if (!isManualPause)
                {
                    Image_MouseUp(null, null);
                }
            }
            catch { }
        }

        private void imgShutdown_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (MessageBox.Show("Are you sure want to exit?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //{
            AppSettings.recordingStatus = RecordingStatus.Paused;

            var fileName = System.Configuration.ConfigurationSettings.AppSettings["RsrJsonFile"].ToString();
            SavePageObjectToFileAsJson(fileName, SessionId, BotId);

            if (Driver != null)
            {
                try
                {
                    try
                    {
                        while (true)
                        {
                            Driver.Close();
                        }
                    }
                    catch { }

                    Driver.Dispose();
                }
                catch { }
            }
            //Environment.Exit(0);
            this.Hide();
            //}
        }

        public void SavePageObjectToFileAsJson(string fileName, string SessionId, string BotId)
        {
            try
            {
                List<Entities.Recorder> recorders = new List<Entities.Recorder>();

                string currentWindow = string.Empty;
                string previousFrameIdOrNameType = string.Empty;
                string previousFrameIdOrName = string.Empty;
                var url = string.Empty;
                var isPreviousWindowSwitched = false;

                refreshRecordedElementObj();

                url = recordedElements.Elements.Where(e => e.ElementAction.ToLower().Contains("openurl")).Select(e => e.ElementValue).FirstOrDefault();

                if (recordedElements.Elements.Where(e => !e.ElementAction.ToLower().Contains("openurl")).Any())
                {
                    foreach (var itm in recordedElements.Elements.Where(e => !(e.ElementAction != null ? e.ElementAction.ToLower() : "").Contains("openurl")).ToList())
                    {
                        List<string> rel = new List<string>();
                        List<Entities.Framepath> framePaths = new List<Entities.Framepath>();

                        if (!string.IsNullOrEmpty(itm.ElementXPath) && !rel.Contains(itm.ElementXPath))
                            rel.Add(itm.ElementXPath);

                        if (!string.IsNullOrEmpty(itm.ElementId) && !rel.Contains(itm.ElementId))
                            rel.Add(itm.ElementId);

                        if (!string.IsNullOrEmpty(itm.ElementCssSelector) && !rel.Contains(itm.ElementCssSelector))
                            rel.Add(itm.ElementCssSelector);

                        if (itm.FrameIdOrName != "DefaultContent")
                            if (!string.IsNullOrEmpty(itm.FrameIdOrNameType) && !string.IsNullOrEmpty(itm.FrameIdOrName))
                            {
                                if (string.IsNullOrEmpty(previousFrameIdOrNameType))
                                {
                                    framePaths.Add(new Entities.Framepath() { Type = itm.FrameIdOrNameType, Value = itm.FrameIdOrName });
                                }
                                else
                                {
                                    if (previousFrameIdOrName != itm.FrameIdOrName && previousFrameIdOrNameType != itm.FrameIdOrNameType)
                                    {
                                        framePaths.Add(new Entities.Framepath() { Type = itm.FrameIdOrNameType, Value = itm.FrameIdOrName });
                                    }
                                }
                                previousFrameIdOrNameType = itm.FrameIdOrNameType;
                                previousFrameIdOrName = itm.FrameIdOrName;
                            }

                        if (framePaths.Count <= 0)
                        {
                            if (isPreviousWindowSwitched)
                            {
                                framePaths.Add(new Entities.Framepath() { Type = itm.FrameIdOrNameType, Value = itm.FrameIdOrName });
                            }
                        }

                        var action = string.Empty;

                        action = itm.ElementAction == "type" ? "sendkeys" : (itm.ElementAction == "type and enter" ? "sendkeysandenter" : itm.ElementAction);

                        recorders.Add(new Entities.Recorder() { action = action, element = itm.ElementTag, value = itm.ElementValue, absXath = itm.ElementXPath, relXpath = rel.ToArray(), scrLoc = "", framePath = framePaths.ToArray() });

                        if (currentWindow != itm.WindowTitle && !string.IsNullOrEmpty(currentWindow))
                        {
                            recorders.Add(new Entities.Recorder() { action = "switchwindow", element = itm.WindowTitle, value = itm.WindowTitle, absXath = "", relXpath = (new List<string>().ToArray()), scrLoc = "", framePath = framePaths.ToArray() });
                            isPreviousWindowSwitched = true;
                        }
                        else
                        {
                            isPreviousWindowSwitched = false;
                        }

                        currentWindow = itm.WindowTitle;


                    }
                }

                var jsonObject = new Entities.Rootobject()
                {
                    BotId = BotId,
                    SessionId = SessionId,
                    Url = url,
                    Recorder = recorders.ToArray()
                };

                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                fileName = System.IO.Path.Combine(path, fileName);

                if (File.Exists(fileName))
                    File.Delete(fileName);

                var serializer = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);

                lstRecoderActs = Newtonsoft.Json.JsonConvert.DeserializeObject<r2rRecorderActivity>(serializer);
                lstRecoderActs.AddBrowser = true;
                //WebConnector = "Window Recorder";
                WebConnector = Convert.ToString(drpVariables.SelectedItem);

                using (var writeStream = new StreamWriter(fileName, true))
                {
                    writeStream.WriteLine(serializer);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void ImgPause_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AppSettings.recordingStatus = RecordingStatus.Paused;
            refreshUI();
        }

        private void refreshUI()
        {
            try
            {
                if (AppSettings.recordingStatus == RecordingStatus.Paused)
                {
                    lblStatus.Content = "Paused.";
                    //if (recordingImageController != null)
                    //    recordingImageController.Pause();
                    recordingImage.Visibility = Visibility.Hidden;
                    btnRecord.Content = "Resume";
                    PauseControl();
                    RefreshRecordingUIDiv();
                }
                else if (AppSettings.recordingStatus == RecordingStatus.Recording)
                {
                    lblStatus.Content = "Recording...";
                    if (recordingImageController != null)
                        recordingImageController.Play();
                    recordingImage.Visibility = Visibility.Visible;
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(@"/Images/Recording.gif", UriKind.Relative);
                    image.EndInit();
                    ImageBehavior.SetAnimatedSource(recordingImage, image);
                    recordingImageController = ImageBehavior.GetAnimationController(recordingImage);
                    btnRecord.Content = "Recording";
                    RecordControl();
                    RefreshRecordingUIDiv();
                }
                else
                {
                    lblStatus.Content = "Stopped.";
                    //recordingImageController.Pause();
                    recordingImage.Visibility = Visibility.Hidden;
                    btnRecord.Content = "Record";
                    StopControl();
                    RefreshRecordingUIDiv();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ImgStop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AppSettings.recordingStatus = RecordingStatus.Paused;
            if (Driver != null)
            {
                try
                {
                    Driver.Close();
                    Driver.Dispose();
                }
                catch
                { }
            }
            refreshUI();
        }

        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecoding && AppSettings.recordingStatus != RecordingStatus.Paused)
            {
                startRecord();
            }
            else
            {
                AppSettings.recordingStatus = RecordingStatus.Recording;

                refreshUI();
            }

            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.recordingStatus = RecordingStatus.Paused;
            refreshUI();

            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }
        }

        private void PauseControl()
        {
            btnPause.IsEnabled = false;
            btnRecord.IsEnabled = true;
            btnStop.IsEnabled = true;
        }
        private void RecordControl()
        {
            btnPause.IsEnabled = true;
            btnRecord.IsEnabled = false;
            btnStop.IsEnabled = true;
        }
        private void StopControl()
        {
            btnPause.IsEnabled = false;
            btnRecord.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            var isEdited = true;
            if (isEdited)
            {
                r2rMsgBoxResult mRes = msgBox.Show("Are you sure want to exit?", "Exit", r2rMsgBoxButtons.OkCancel);

                if (mRes == r2rMsgBoxResult.Ok)
                {
                    //SaveChanges();
                    //this.isClose = true;
                    this.Hide();

                    imgShutdown_MouseLeftButtonUp(null, null);
                }
            }
            else
            {
                //this.isClose = true;
                this.Hide();
            }
            //isRecoding = false;
            //AppSettings.recordingStatus = RecordingStatus.Stopped;
            //if (Driver != null)
            //{
            //    try
            //    {
            //        Driver.Close();
            //        Driver.Dispose();
            //    }
            //    catch
            //    { }
            //}
            //refreshUI();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isManualPause = true;
                if (AppSettings.recordingStatus == RecordingStatus.Recording)
                {
                    if (Driver != null)
                        ImgPause_MouseLeftButtonUp(null, null);
                    isManualPause = false;
                }

                try
                {
                    var welcome = new System.Windows.Media.MediaPlayer();
                    welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                    welcome.Play();
                }
                catch { }

                RecorderSettings settings = new RecorderSettings(Driver);
                settings.ShowDialog();

                if (!isManualPause)
                {
                    if (Driver != null)
                    {
                        Image_MouseUp(null, null);
                    }
                }

            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var isEdited = true;
            if (isEdited)
            {
                r2rMsgBoxResult mRes = msgBox.Show("Are you sure want to exit?", "Exit", r2rMsgBoxButtons.OkCancel);

                if (mRes == r2rMsgBoxResult.No)
                {
                    //this.isClose = true;
                    this.Hide();
                    server.CloseAsync().Wait();
                    server.Dispose();
                }
                else if (mRes == r2rMsgBoxResult.Ok)
                {
                    //SaveChanges();
                    //this.isClose = true;
                    this.Hide();
                    server.CloseAsync().Wait();
                    server.Dispose();

                    imgShutdown_MouseLeftButtonUp(null, null);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                //this.isClose = true;
                this.Hide();
                server.CloseAsync().Wait();
                server.Dispose();
            }
        }

        private void CleanScreenshotDirectory()
        {
            try
            {
                var dirName = "Screenshots";

                if (!Directory.Exists(Environment.CurrentDirectory + "\\" + dirName))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + dirName);
                }
                else
                {
                    DirectoryInfo downloadDirectory = new DirectoryInfo(Environment.CurrentDirectory + "\\" + dirName);

                    foreach (FileInfo file in downloadDirectory.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in downloadDirectory.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            RecorderIntro splashScreen = new RecorderIntro();
            splashScreen.ShowDialog();
        }
    }

    public static class Presenters
    {
        private static SparkRecorder _MainWindowPresenter = null;
        public static SparkRecorder MainWindowPresenter
        {
            get
            {
                return (_MainWindowPresenter = _MainWindowPresenter ?? new SparkRecorder());
            }
            set
            {
                _MainWindowPresenter = value;
            }
        }
    }

    public class CustomHeaderHandler : System.Net.Http.DelegatingHandler
    {
        protected override Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith((task) =>
                {
                    System.Net.Http.HttpResponseMessage response = task.Result;
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    return response;
                });
        }
    }

    public class CommandController : ApiController
    {
        SparkRecorder mainW = Presenters.MainWindowPresenter;

        [HttpPost]
        public string SaveCommand(RecordedElement element)
        {
            if (AppSettings.recordingStatus == RecordingStatus.Recording)
            {
                //Task.Run(() => Presenters.WebDriverMainPresenter.ProcessApiCommands(element));
                //Presenters.WebDriverMainPresenter.ProcessApiCommands(element);
                //mainW.TextBoxRequestMsg = element.ElementAction;
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    //mainW.txtUrl.Text = element.ElementAction;
                    //mainW.recordedElements.Add(element);
                    mainW.SaveRecordedElement(element);

                });
            }
            else
            {
                try
                {
                    IJavaScriptExecutor js = Presenters.MainWindowPresenter.Driver as IJavaScriptExecutor;
                    js.ExecuteScript("if (window.parent.parent.parent.parent.parent.document.getElementById('recordLoading')) window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = 'none';");
                }
                catch { }
            }

            return "Updated!";
        }

        [HttpGet]
        public string IsRecording()
        {
            return AppSettings.recordingStatus.ToString();
        }

        [HttpPost]
        public string UpdateWindow(string windowTitle)
        {
            //Task.Run(() => Presenters.WebDriverMainPresenter.ProcessApiCommands(element));


            return "Updated!";
        }
    }

    public class RecordedElements
    {
        public List<RecordedElement> Elements { get; set; }
    }

    public class RecordedElement
    {
        public int Sno { get; set; }
        public int CaptureCount { get; set; }
        public string ElementId { get; set; }
        public string ElementTag { get; set; }
        public string ElementCodeName { get; set; }
        public string ElementXPath { get; set; }
        public string ElementSelectedPath { get; set; }
        public List<string> ElementPaths { get; set; }
        public string ElementCssSelector { get; set; }
        public string ElementAction { get; set; }
        public string ElementValue { get; set; }
        public string WindowTitle { get; set; }
        public string WindowId { get; set; }
        public string FrameId { get; set; }
        public string FrameIdOrName { get; set; }
        public string FrameIdOrNameType { get; set; }
        public string Type { get; set; }
        public List<string> Commands { get; set; }
        public bool IsEdited { get; set; }
    }

    public class AllCommands
    {
        public string Command { get; set; }
    }


    public class ComboBoxPairs
    {
        public string _Key { get; set; }
        public string _Value { get; set; }

        public ComboBoxPairs(string _key, string _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
}

namespace Entities
{
    public class Rootobject
    {
        public string SessionId { get; set; }
        public string BotId { get; set; }
        public string Url { get; set; }
        public Recorder[] Recorder { get; set; }
    }

    public class Recorder
    {
        public string element { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public object scrLoc { get; set; }
        public string absXath { get; set; }
        public string[] relXpath { get; set; }
        public Framepath[] framePath { get; set; }
    }

    public class Framepath
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
