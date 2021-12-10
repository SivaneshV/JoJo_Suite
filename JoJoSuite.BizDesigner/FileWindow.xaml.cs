using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace r2rStudio.Business.Designer
{
    /// <summary>
    /// Interaction logic for FileWindow.xaml
    /// </summary>
    public partial class FileWindow : Window
    {
        public string RecentFile = "";
        public bool IsRecent = false;
        public bool IsOpen = false;
        public bool IsNew = false;
        public string RoboTemplate = "blank";

        public FileWindow()
        {
            InitializeComponent();

            recentFile1.Content = ConfigurationManager.AppSettings["recentFile1"];
            recentFile2.Content = ConfigurationManager.AppSettings["recentFile2"];
            recentFile3.Content = ConfigurationManager.AppSettings["recentFile3"];
            recentFile4.Content = ConfigurationManager.AppSettings["recentFile4"];
            recentFile5.Content = ConfigurationManager.AppSettings["recentFile5"];
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void recentFile1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RecentFile = recentFile1.Content.ToString();
            this.DialogResult = true;
            IsRecent = true;
            this.Close();
        }

        private void recentFile2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RecentFile = recentFile2.Content.ToString();
            this.DialogResult = true;
            IsRecent = true;
            this.Close();
        }

        private void recentFile3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RecentFile = recentFile3.Content.ToString();
            this.DialogResult = true;
            IsRecent = true;
            this.Close();
        }

        private void recentFile4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RecentFile = recentFile4.Content.ToString();
            this.DialogResult = true;
            IsRecent = true;
            this.Close();
        }

        private void recentFile5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RecentFile = recentFile5.Content.ToString();
            this.DialogResult = true;
            IsRecent = true;
            this.Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            RecentFile = recentFile5.Content.ToString();
            this.DialogResult = true;
            IsOpen = true;
            this.Close();
        }

        private void btnTemplateBlank_Click(object sender, RoutedEventArgs e)
        {
            RecentFile = recentFile5.Content.ToString();
            this.DialogResult = true;
            IsNew = true;
            this.Close();
        }
    }
}
