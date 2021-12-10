using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for RecorderIntro.xaml
    /// </summary>
    public partial class RecorderIntro : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        SpeechSynthesizer speaker = null;
        public bool isFirst = false;
        public int currentImageCount = 1;
        public RecorderIntro()
        {
            InitializeComponent();

            speaker = new SpeechSynthesizer();

            this.isFirst = true;

            //this.checkBox.IsChecked = isDontShowAgain();
            chkDisplay.IsChecked = CheckSplashScreen();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setVoice();

            loadImageLinks();

            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            LoadImages();

            AppSettings.isAppStartupCnt++;

            if (!CheckSplashScreen() && AppSettings.isAppStartupCnt == 1)
            {
                try
                {
                    if (Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["SayWelcomeMessage"]))
                    {
                        var welcome = new System.Windows.Media.MediaPlayer();
                        welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/Welcome.mp3", UriKind.Relative));
                        welcome.Play();
                    }
                }
                catch { }
            }
        }

        private void LoadImages()
        {
            try
            {
                var path = System.IO.Path.Combine(Environment.CurrentDirectory, "IntroImages");
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                //foreach (var item in directoryInfo.GetFiles())
                //{
                //var webImage = new BitmapImage();
                //webImage.BeginInit();
                //webImage.UriSource = new Uri(@"/IntroImages/" + currentImageCount.ToString() + directoryInfo.GetFiles()[0].Extension, UriKind.Relative);
                //webImage.EndInit();
                imageViewer.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/IntroImages/" +
                    currentImageCount.ToString() + directoryInfo.GetFiles()[0].Extension));
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        int timer = 0; System.Drawing.Image original = null;
        string currentImagePath = string.Empty;
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer = timer + 5;

            //if (currentImagePath != null)
            //    image.Source = new BitmapImage(new Uri(currentImagePath)); //SetAlpha((Bitmap)original, timer);

            if (timer >= 255)
            {
                timer = 0;
                currentImagePath = string.Empty;
                dispatcherTimer.Stop();
            }
        }

        static Bitmap SetAlpha(Bitmap bmpIn, int alpha)
        {
            Bitmap bmpOut = new Bitmap(bmpIn.Width, bmpIn.Height);
            float a = alpha / 255f;
            System.Drawing.Rectangle r = new System.Drawing.Rectangle(0, 0, bmpIn.Width, bmpIn.Height);

            float[][] matrixItems = {
        new float[] {1, 0, 0, 0, 0},
        new float[] {0, 1, 0, 0, 0},
        new float[] {0, 0, 1, 0, 0},
        new float[] {0, 0, 0, a, 0},
        new float[] {0, 0, 0, 0, 1}};

            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            using (Graphics g = Graphics.FromImage(bmpOut))
                g.DrawImage(bmpIn, r, r.X, r.Y, r.Width, r.Height, GraphicsUnit.Pixel, imageAtt);

            return bmpOut;
        }

        private bool isDontShowAgain()
        {
            try
            {
                string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string path = System.IO.Path.GetDirectoryName(exe);
                string filePath = System.IO.Path.Combine(path, "showAgain.cfg");

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line = string.Empty;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.ToLower().Contains("false"))
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //WebDriver.MyLog.Error(ex.Message + " - " + ex.StackTrace);
                return false;
            }

            return true;
        }

        private void setVoice()
        {
            var botVoice = string.Empty;
            foreach (var voice in speaker.GetInstalledVoices())
            {
                if (voice.VoiceInfo.Gender == VoiceGender.Female)
                {
                    botVoice = voice.VoiceInfo.Name;
                    if (voice.VoiceInfo.Name.Contains("Hazel"))
                    {
                        botVoice = voice.VoiceInfo.Name;
                        break;
                    }
                }
            }

            speaker.SelectVoice(botVoice);
            speaker.Volume = 100;
            speaker.Rate = 0;
        }

        private void loadImageLinks()
        {
            try
            {
                int imgCnt = 0;
                int leftPos = 200;
                string[] filePaths = Directory.GetFiles(getIntroImagePath()).Where(file =>
                file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".jpeg") || file.ToLower().EndsWith(".gif")).ToArray();

                if (filePaths.Length > 0)
                    currentImagePath = filePaths[0];
                //pictureBox1.Image = Image.FromFile(filePaths[0]);

                //tableLayoutPanel1.Controls.Clear();
                //tableLayoutPanel1.ColumnCount = 0;

                foreach (var fileName in filePaths)
                {
                    Label linkLabel = new Label();

                    //linkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                    linkLabel.FontSize = 14;
                    //new Font(new FontFamily("Calibri"), 14, FontStyle.Bold);
                    linkLabel.Content = "\u25C9";
                    linkLabel.MouseUp += LinkLabel_MouseUp;
                    linkLabel.Tag = fileName;
                    linkLabel.Width = 25;

                    //tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
                    //tableLayoutPanel1.ColumnCount = tableLayoutPanel1.ColumnCount + 1;
                    //tableLayoutPanel1.Controls.Add(linkLabel, imgCnt, 0);

                    //gridPanel.

                    //imgCnt++;
                    leftPos = leftPos + 2;
                }

                //tableLayoutPanel1.ColumnCount = tableLayoutPanel1.ColumnCount - 1;

                //if (tableLayoutPanel1.Controls.Count > 0)
                //{
                //    currentLink = tableLayoutPanel1.Controls[0].Tag.ToString();
                //    panel1.Controls[0].Font = new Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
                //    tableLayoutPanel1.Controls[0].ForeColor = Color.Blue;
                //    tableLayoutPanel1.Controls[0].Text = "\t\u2022";
                //}

                dispatcherTimer.Start();
            }
            catch (Exception ex) { }
        }

        private void LinkLabel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var link = (Label)sender;
                currentImagePath = getIntroImagePath(); //System.Drawing.Image.FromFile(System.IO.Path.Combine(getIntroImagePath(), link.Tag.ToString()));

                //pictureBox1.Image = SetAlpha((Bitmap)original, 0);
                //original = null;
                //pictureBox1.Image = Image.FromFile(Path.Combine(getIntroImagePath(), link.Tag.ToString()));



                //foreach (LinkLabel ctrl in tableLayoutPanel1.Controls)
                //{
                //    ctrl.Font = new Font(new FontFamily("Calibri"), 14, FontStyle.Bold);
                //    ctrl.Text = "\u25C9";
                //}

                //link.Font = new Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
                //link.ForeColor = Color.Blue;
                //link.Text = "\t\u2022";

                //currentLink = link.Tag.ToString();

                //if (tableLayoutPanel1.Controls.Count > 0)
                //{
                //    if (link.Tag.ToString() == tableLayoutPanel1.Controls[tableLayoutPanel1.Controls.Count - 1].Tag.ToString())
                //    {
                //        linkNext.Text = "FINISH";
                //    }
                //    else
                //    {
                //        linkNext.Text = "NEXT";
                //    }
                //}

                dispatcherTimer.Start();
            }
            catch (Exception ex)
            {

            }
        }

        private string getIntroImagePath()
        {
            string exe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string path = System.IO.Path.GetDirectoryName(exe);

            return System.IO.Path.Combine(path, System.IO.Path.Combine("Resources", "IntroImages"));
        }

        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    var welcome = new System.Windows.Media.MediaPlayer();
                    welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                    welcome.Play();
                }
                catch { }

                var path = System.IO.Path.Combine(Environment.CurrentDirectory, "IntroImages");
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                currentImageCount = currentImageCount - 1;

                var filePath = directoryInfo.GetFiles().Where(f => f.Name.Contains(currentImageCount.ToString())).FirstOrDefault();

                if (filePath != null)
                {
                    imageViewer.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/IntroImages/" + filePath.Name));
                }
                else
                {
                    currentImageCount = currentImageCount + 1;
                }
            }
            catch { }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    var welcome = new System.Windows.Media.MediaPlayer();
                    welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                    welcome.Play();
                }
                catch { }

                var path = System.IO.Path.Combine(Environment.CurrentDirectory, "IntroImages");
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                currentImageCount = currentImageCount + 1;

                var filePath = directoryInfo.GetFiles().Where(f => f.Name.Contains(currentImageCount.ToString())).FirstOrDefault();

                if (filePath != null)
                {
                    imageViewer.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/IntroImages/" + filePath.Name));
                }
                else
                {
                    currentImageCount = currentImageCount - 1;
                }
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var welcome = new System.Windows.Media.MediaPlayer();
                welcome.Open(new Uri(Environment.CurrentDirectory + "/Sounds/ButtonClick.mp3", UriKind.Relative));
                welcome.Play();
            }
            catch { }

            this.Hide();
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ChangeSplashScreenDisplay("true");
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeSplashScreenDisplay("false");
        }

        private bool ChangeSplashScreenDisplay(string isDisplay)
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

                File.WriteAllText(rootPath, isDisplay);
            }
            catch (Exception ex)
            {

            }
            return displaySplash;
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
    }
}
