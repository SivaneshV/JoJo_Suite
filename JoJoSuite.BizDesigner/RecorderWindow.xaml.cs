using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using JoJoSuite.Business.Lib;
using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for importSapScript.xaml
    /// </summary>
    public partial class RecorderWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        public r2rBot crBot = new r2rBot();
        public r2rUser crUser = new r2rUser();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);
        public r2rRecorderActivity lstRecoderActs = new r2rRecorderActivity();

        //public List<Recorder> lstRecoderActs = new List<Recorder>();
        public string URL;
        public string WebConnector;
        public List<string> VariableList = new List<string>();
        public RecorderWindow()
        {
            InitializeComponent();
            ChangeVariables();
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            System.Uri url;
            string jsonpath = "";
            if (UrlChecker1(txtUrl.Text))
            {
                try
                {
                    if (Convert.ToString(drpRecorderType.SelectedItem) == "Chrome Extenstion")
                    {

                        string recoderpath = System.AppDomain.CurrentDomain.BaseDirectory + "CWebRecorder";
                        jsonpath = recoderpath;
                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = recoderpath + "\\UIR.exe",
                                Arguments = @txtUrl.Text

                            }

                        };
                        process.Start();
                        process.WaitForExit();



                        //string drivepath = System.AppDomain.CurrentDomain.BaseDirectory + "Drivers";
                        //string recorderPath = System.AppDomain.CurrentDomain.BaseDirectory + "CWebRecorder";
                        //jsonpath = recorderPath + @"\Download";
                        ////To create Chrome browser driver and default browser driver
                        //IWebDriver WebDriver;
                        //var chromeOptions = new ChromeOptions();

                        //var AdblockAuth = recorderPath + "\\" +ConfigurationManager.AppSettings["ChromeExtenstion"]; // @"\RUIRecorder_31012020.crx";
                        //chromeOptions.AddArguments("--start-maximized", "--disable-web-security");

                        //chromeOptions.AddExtension(AdblockAuth);

                        //if (recorderPath != null)
                        //    chromeOptions.AddUserProfilePreference("download.default_directory", recorderPath + @"\Download");
                        //chromeOptions.AddUserProfilePreference("download.prompt_for_download", "true");
                        //chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
                        //chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
                        ////WebDriver = new ChromeDriver(drivepath, chromeOptions);
                        //Thread.Sleep(5000);
                        //WebDriver = new ChromeDriver(drivepath, chromeOptions);

                        //if (txtUrl.Text != "") { WebDriver.Navigate().GoToUrl(txtUrl.Text); }
                        //Thread.Sleep(5000);

                    }
                    else
                    {


                        string recoderpath = System.AppDomain.CurrentDomain.BaseDirectory + "WebRecoder";
                        jsonpath = recoderpath;
                        //using (Process process1 = new Process())
                        //{
                        //    process1.StartInfo = new ProcessStartInfo
                        //    {
                        //        FileName = recoderpath + "\\UIRCP.exe",
                        //        Arguments = @txtUrl.Text + " sessionid123 botid123"

                        //    }
                        //}
                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = recoderpath + "\\UIRCP.exe",
                                Arguments = @txtUrl.Text + " sessionid123 botid123"

                            }

                        };
                        process.Start();
                        process.WaitForExit();

                    }
                    lstRecoderActs = Newtonsoft.Json.JsonConvert.DeserializeObject<r2rRecorderActivity>(File.ReadAllText(jsonpath + "\\ExportToR2r.json"));
                    URL = txtUrl.Text;
                    lstRecoderActs.AddBrowser = (bool)ChkAddBrowser.IsChecked;
                    WebConnector = Convert.ToString(drpVariables.SelectedItem);
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    r2rMsgBox.errorfile("btnStart_Click", ex);
                    MessageBox.Show("Something went wrong in recorder. Please Re-record again");
                }

            }
            else
            {
                MessageBox.Show("Please provide correct URL");
            }


        }
        public bool UrlChecker1(string url)
        {
            Uri uriResult;
            bool tryCreateResult = Uri.TryCreate(url, UriKind.Absolute, out uriResult);
            if (tryCreateResult == true && uriResult != null)
                return true;
            else
                return false;
        }
        public bool ValidHttpURL(string s, out Uri resultURI)
        {
            if (!Regex.IsMatch(s, @"^https?:\/\/", RegexOptions.IgnoreCase))
                s = "http://" + s;

            if (Uri.TryCreate(s, UriKind.Absolute, out resultURI))
                return (resultURI.Scheme == Uri.UriSchemeHttp ||
                        resultURI.Scheme == Uri.UriSchemeHttps);

            return false;
        }
        private void ChangeVariables()
        {

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                drpVariables.ItemsSource = VariableList;
                drpRecorderType.Items.Add("Window Recorder");
                //drpRecorderType.Items.Add("Chrome Extenstion");
            }));
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void SetStatus(string msg, StatusState state)
        {
            lblStatus.Content = msg;

            var bc = new BrushConverter();

            lblStatus.Foreground = (Brush)Application.Current.Resources["ThemeColor2"];

            if (state == StatusState.Info)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#03a9f4");
                lblStatus.Foreground = (Brush)bc.ConvertFrom("#fff");
            }
            else if (state == StatusState.Warning)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#FFF59D");

            }
            else if (state == StatusState.Success)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#A5D6A7");
            }
            else if (state == StatusState.Danger)
            {
                pnlStatus.Background = (Brush)bc.ConvertFrom("#E57373");
            }

            var switchOffAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.Zero,
            };

            var switchOnAnimation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.Zero,
                BeginTime = TimeSpan.FromSeconds(0.3),
            };

            var blinkStoryboard = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(0.5),
                RepeatBehavior = new RepeatBehavior(3)
            };

            Storyboard.SetTarget(switchOffAnimation, pnlStatus);
            Storyboard.SetTargetProperty(switchOffAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOffAnimation);

            Storyboard.SetTarget(switchOnAnimation, pnlStatus);
            Storyboard.SetTargetProperty(switchOnAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOnAnimation);

            pnlStatus.BeginStoryboard(blinkStoryboard);
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
    public class r2rRecorderActivity
    {
        public string SessionId { get; set; }
        public string BotId { get; set; }
        public string Url { get; set; }
        public bool AddBrowser { get; set; }
        public List<Recorder> recorder { get; set; }

    }
    public class Recorder
    {
        public string element { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public string scrLoc { get; set; }
        public string absXath { get; set; }
        public string[] relXpath { get; set; }
        public FramePath[] FramePath { get; set; }
    }

    public partial class FramePath
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }



}
