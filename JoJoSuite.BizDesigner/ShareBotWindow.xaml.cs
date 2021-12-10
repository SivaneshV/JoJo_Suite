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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JoJoSuite.UI
{
    public partial class ShareBotWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        List<r2rUser> lstUser = new List<r2rUser>();

        List<r2rUser> lstSharedUser = new List<r2rUser>();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        r2rUser crUser = new r2rUser();

        r2rBot crBot = new r2rBot();

        public ShareBotWindow(r2rUser User, r2rBot Bot)
        {
            InitializeComponent();

            crUser = User;
            crBot = Bot;

            LoadUsers();
        }

        private void LoadUsers()
        {
            lstUser.Clear();

            lbUsers.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            lstUser = r2rLib.GetUsers();

            foreach (r2rUser obj in lstUser)
            {
                if (crUser.Id != obj.Id)
                {
                    StackPanel sp1 = new StackPanel();
                    sp1.Orientation = Orientation.Horizontal;

                    CheckBox cb1 = new CheckBox();
                    cb1.Content = obj.Name;
                    cb1.Tag = obj.Id;
                    cb1.Height = 30;
                    cb1.Width = 230;

                    RadioButton rb1 = new RadioButton();
                    rb1.Content = "View";
                    rb1.IsChecked = false;
                    rb1.Height = 30;
                    rb1.Width = 75;

                    RadioButton rb2 = new RadioButton();
                    rb2.Content = "Run";
                    rb2.IsChecked = true;
                    rb2.Height = 30;
                    rb2.Width = 75;

                    RadioButton rb3 = new RadioButton();
                    rb3.Content = "Change";
                    rb3.IsChecked = false;
                    rb3.Height = 30;
                    rb3.Width = 75;

                    sp1.Children.Add(cb1);
                    sp1.Children.Add(rb1);
                    sp1.Children.Add(rb2);
                    sp1.Children.Add(rb3);

                    cb1.Height = rb1.Height = rb2.Height = rb3.Height = 30;
                    cb1.VerticalContentAlignment = rb1.VerticalContentAlignment = rb2.VerticalContentAlignment = rb3.VerticalContentAlignment = VerticalAlignment.Center;

                    lbUsers.Items.Add(sp1);
                }
            }

            lstSharedUser = r2rLib.GetUsersByBot(crBot.Id);

            foreach(object listItem in lbUsers.Items)
            {
                StackPanel sp1 = (StackPanel)listItem;

                CheckBox chkItem = (CheckBox)sp1.Children[0];

                chkItem.Checked += ChkItem_Checked;
                chkItem.Unchecked += ChkItem_Unchecked;

                RadioButton rdoView = (RadioButton)sp1.Children[1];
                RadioButton rdoRun = (RadioButton)sp1.Children[2];
                RadioButton rdoChange = (RadioButton)sp1.Children[3];

                rdoView.IsChecked = true;

                r2rUser fUser = lstSharedUser.Find(x => x.Id == Convert.ToInt32(chkItem.Tag));

                if (fUser == null)
                {
                    //not shared
                    chkItem.IsChecked = false;

                    rdoView.Visibility = rdoRun.Visibility = rdoChange.Visibility = Visibility.Collapsed;
                }
                else
                {
                    //already shared
                    chkItem.IsChecked = true;

                    rdoView.Visibility = rdoRun.Visibility = rdoChange.Visibility = Visibility.Visible;

                    if (fUser.BotAccess == 0)
                    {
                        rdoView.IsChecked = true;
                    }
                    else if (fUser.BotAccess == 1)
                    {
                        rdoRun.IsChecked = true;
                    }
                    else
                    {
                        rdoChange.IsChecked = true;
                    }
                }
            }

            Mouse.OverrideCursor = null;

        }

        private void ChkItem_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chkItem = (CheckBox)e.Source;

            StackPanel sp1 = (StackPanel)chkItem.Parent;

            RadioButton rdoView = (RadioButton)sp1.Children[1];
            RadioButton rdoRun = (RadioButton)sp1.Children[2];
            RadioButton rdoChange = (RadioButton)sp1.Children[3];

            rdoView.Visibility = rdoRun.Visibility = rdoChange.Visibility = Visibility.Collapsed;
        
        }

        private void ChkItem_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkItem = (CheckBox)e.Source;

            StackPanel sp1 = (StackPanel)chkItem.Parent;

            RadioButton rdoView = (RadioButton)sp1.Children[1];
            RadioButton rdoRun = (RadioButton)sp1.Children[2];
            RadioButton rdoChange = (RadioButton)sp1.Children[3];

            rdoView.Visibility = rdoRun.Visibility = rdoChange.Visibility = Visibility.Visible;
        
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
            Storyboard.SetTargetProperty(switchOffAnimation, new PropertyPath(StackPanel.OpacityProperty));
            blinkStoryboard.Children.Add(switchOffAnimation);

            Storyboard.SetTarget(switchOnAnimation, pnlStatus);
            Storyboard.SetTargetProperty(switchOnAnimation, new PropertyPath(StackPanel.OpacityProperty));
            blinkStoryboard.Children.Add(switchOnAnimation);

            pnlStatus.BeginStoryboard(blinkStoryboard);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (r2rLib.RemoveUsersFromBot(crBot.Id, crUser.Id))
            {
                foreach (object listItem in lbUsers.Items)
                {
                    StackPanel sp1 = (StackPanel)listItem;

                    CheckBox chkItem = (CheckBox)sp1.Children[0];

                    RadioButton rdoView = (RadioButton)sp1.Children[1];
                    RadioButton rdoRun = (RadioButton)sp1.Children[2];
                    RadioButton rdoChange = (RadioButton)sp1.Children[3];

                    if (chkItem.IsChecked == true)
                    {
                        int userId = Convert.ToInt32(chkItem.Tag);

                        int access = 0;

                        if (rdoRun.IsChecked == true)
                        {
                            access = 1;
                        }
                        else if (rdoChange.IsChecked == true)
                        {
                            access = 2;
                        }

                        if (r2rLib.AddUserToBot(crBot.Id, userId, access) <= 0)
                        {
                            SetStatus("Sorry, not able to update shared users.", StatusState.Danger);
                            return;
                        }
                    }
                }
                SetStatus("Bot shared successfully with the selected users.", StatusState.Success);

            }
            else
            {
                SetStatus("Sorry, not able to update shared users.", StatusState.Danger);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
