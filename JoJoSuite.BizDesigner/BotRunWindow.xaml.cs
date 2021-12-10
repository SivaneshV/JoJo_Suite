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
    /// Interaction logic for BotRunWindow.xaml
    /// </summary>
    public partial class BotRunWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        List<r2rBotRun> lstHistory = new List<r2rBotRun>();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        r2rBot crBot = new r2rBot();

        public BotRunWindow(r2rBot Bot)
        {
            InitializeComponent();

            crBot = Bot;

            LoadHistory(0);
        }

        private void LoadHistory(int testRun)
        {
            lstHistory.Clear();

            lbTeam.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            lstHistory = r2rLib.GetBotRuns(crBot.Id, testRun);

            int counter = 1;

            foreach (r2rBotRun obj in lstHistory)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;
                sp1.Tag = obj;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\bothistory.png", UriKind.Relative));
                i1.Height = 16;
                i1.Width = 16;

                Label l1 = new Label();
                l1.Width = 200;

                Label l4 = new Label();
                l4.Content = counter.ToString();
                l4.Width = 100;

                Label l2 = new Label();
                l2.Content = "Starts @" + obj.TimeStart.ToString("hh:mm:ss");
                l2.Content += " End @" + obj.TimeEnd.ToString("hh:mm:ss");

                Label l3 = new Label();
                l3.Content = counter.ToString();
                l3.Width = 50;

                Label l6 = new Label();
                l6.Content = obj.User.Name;

                if (obj.TestRun)
                {
                    l1.Content = obj.DateRun.ToString("dd MMMM yyyy");
                    l1.Foreground = l2.Foreground = l4.Foreground = l6.Foreground = Brushes.Gray;
                    l4.Content = "(Test)";
                }
                else
                {
                    l1.Content = obj.DateRun.ToString("dd MMMM yyyy");
                    l1.Foreground = l2.Foreground = l4.Foreground = l6.Foreground = Brushes.Black;
                    l4.Content = "(Production)";
                }

                Label l5 = new Label();
                l5.Content = "[TCount-" + obj.TransactionCount + "]";
                l5.Width = 50;

             

                l1.SetValue(Label.FontWeightProperty, FontWeights.Bold);

                counter++;

                sp1.Children.Add(i1);
                sp1.Children.Add(l3);
                sp1.Children.Add(l1);
                sp1.Children.Add(l2);
                sp1.Children.Add(l4);
                sp1.Children.Add(l5);
                sp1.Children.Add(l6);

                if (obj.Log.Trim().Length > 0)
                {
                    Button b1 = new Button();
                    b1.Style = Resources["OrangeButton"] as Style;
                    b1.Content = "Log";
                    b1.Click += B1_Click;
                    b1.Tag = obj.Log;
                    sp1.Children.Add(b1);
                }
                

                lbTeam.Items.Add(sp1);
            }

            Mouse.OverrideCursor = null;

        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((Button)sender).Tag.ToString());
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem sel = (ComboBoxItem)cboFilterType.SelectedItem;

            int rType = Convert.ToInt32(sel.Tag);

            LoadHistory(rType);

        }
    }
}
