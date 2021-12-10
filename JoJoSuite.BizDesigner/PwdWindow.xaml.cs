using JoJoSuite.Business.Lib;
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

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for PwdWindow.xaml
    /// </summary>
    public partial class PwdWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        List<r2rBotPassword> lstPwd = new List<r2rBotPassword>();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        r2rBot crBot = new r2rBot();

        public PwdWindow(r2rBot Bot)
        {
            InitializeComponent();
            crBot = Bot;
            LoadPasswords();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            r2rBotPassword pwd = new r2rBotPassword();
            pwd.Bot = crBot;

            EditPwdWindow ep1 = new EditPwdWindow(pwd, 0);
            ep1.Owner = this;
            ep1.ShowDialog();

            if (ep1.btnRes == 0)
            {
                LoadPasswords();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbPwd.SelectedItem != null)
            {
                StackPanel sp1 = (StackPanel)lbPwd.SelectedItem;

                r2rBotPassword pwd = (r2rBotPassword)sp1.Tag;
                pwd.Bot = crBot;

                EditPwdWindow ep1 = new EditPwdWindow(pwd, 1);
                ep1.Owner = this;
                ep1.ShowDialog();
            }
        }

        private void LoadPasswords()
        {
            lstPwd.Clear();

            lbPwd.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            lstPwd = r2rLib.GetBotPasswords(crBot.Id);

            int counter = 1;

            foreach (r2rBotPassword obj in lstPwd)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;
                sp1.Tag = obj;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\key01.png", UriKind.Relative));
                i1.Height = 16;
                i1.Width = 16;

                Label l4 = new Label();
                l4.Content = counter.ToString();
                l4.Width = 50;
                l4.HorizontalContentAlignment = HorizontalAlignment.Center;

                Label l2 = new Label();
                l2.Content = obj.Name;

                counter++;

                sp1.Children.Add(i1);
                sp1.Children.Add(l4);
                sp1.Children.Add(l2);

                lbPwd.Items.Add(sp1);
            }

            Mouse.OverrideCursor = null;

        }
    }
}
