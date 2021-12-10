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
    /// Interaction logic for ExcelTemplateWindow.xaml
    /// </summary>
    public partial class ExcelTemplateWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        public r2rBot crBot = new r2rBot();
        public r2rUser crUser = new r2rUser();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        public r2EmailActivity eAct = new r2EmailActivity();

        public List<string> VariableList = new List<string>();
        public ExcelTemplateWindow()
        {
            InitializeComponent();

        }



        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            eAct.FilePath = txtFilePath.Text;
            eAct.SheetName = txtSheetName.Text;
            eAct.ExistingExcel = (bool)ChkExistExcel.IsChecked;
            eAct.CreateExcel = (bool)ChkNewExcel.IsChecked;
            eAct.WriteValue = (bool)ChkWriteValue.IsChecked;
            eAct.ReadValue = (bool)ChkReadValue.IsChecked;
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

        //private void BtnDownloadPathCh_Click(object sender, RoutedEventArgs e)
        //{
        //    using (var fbd = new FolderBrowserDialog())
        //    {
        //        DialogResult result = fbd.ShowDialog();

        //        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //        {
        //            txtDownload.Text = fbd.SelectedPath;
        //        }
        //    }
        //}

        public class r2EmailActivity
        {
            public string FilePath { get; set; }
            public string SheetName { get; set; }          
            public bool ExistingExcel { get; set; }
            public bool CreateExcel { get; set; }
            public bool WriteValue { get; set; }
            public bool ReadValue { get; set; }
        }

        private void BtnFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Excel files|*.xls*|CSV files|*.csv|All files (*.*)|*.*";

            //if (ofd.ShowDialog() == true)
            //{
                
            //}
        }
    }


}
