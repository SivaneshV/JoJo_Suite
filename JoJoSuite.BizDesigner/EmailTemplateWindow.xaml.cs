using JoJoSuite.Business.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for EmailTemplateWindow.xaml
    /// </summary>
    public partial class EmailTemplateWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        public r2rBot crBot = new r2rBot();
        public r2rUser crUser = new r2rUser();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        public r2EmailActivity eAct = new r2EmailActivity();

        public List<string> VariableList = new List<string>();
        public EmailTemplateWindow()
        {
            InitializeComponent();

        }



        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            eAct.ServerUrl = txtUrl.Text;
            eAct.FromId = txtFromId.Text;
            eAct.Domain = txtDomain.Text;
            eAct.Username = txtUname.Text;
            eAct.Password = txtPass.Password;
            eAct.FromFolder = txtFrom.Text;
            eAct.ToFolder = txtTo.Text;
            eAct.DownloadPath = txtDownload.Text;
            eAct.DownloadAtt = (bool)ChkDownloadAttachment.IsChecked;
            eAct.MoveFolder = (bool)ChkEmailMove.IsChecked;
            eAct.SubjectFilter = txtSubFilter.Text;
            this.DialogResult = true;
            this.Close();

        }

        public void CreateSapAct(string line, string type)
        {


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

            lblStatus.Foreground = (Brush)System.Windows.Application.Current.Resources["ThemeColor2"];

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

        private void BtnDownloadPathCh_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtDownload.Text = fbd.SelectedPath;
                }
            }
        }

        public class r2EmailActivity
        {
            public string ServerUrl { get; set; }
            public string FromId { get; set; }
            public string Domain { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string FromFolder { get; set; }
            public string ToFolder { get; set; }
            public string DownloadPath { get; set; }
            public string SubjectFilter { get; set; }
            public bool DownloadAtt { get; set; }
            public bool MoveFolder { get; set; }
        }
    }


}
