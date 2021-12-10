using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Configuration;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Threading;
using System.Collections.Generic;

using JoJoSuite.Business.Lib;
using System.Web;
using System.DirectoryServices;
using JoJoSuite.UI;

namespace JoJoSuite.Business.Designer
{
    public partial class FirstWindow : Window
    {
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        public static extern bool LogonUser(string userName, string domainName, string password, int LogonType, int LogonProvider, ref IntPtr phToken);

        enum StatusState { Info, Warning, Success, Danger };

        List<r2rBot> lstMyBots = new List<r2rBot>();
        List<r2rBot> lstSharedBots = new List<r2rBot>();

        string sEmail = "";
        string sPwd = "";

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);
        r2rUser crUser = new r2rUser();

        //int SelIdMyBot = 0;
        //int SelIdSharedBot = 0;

        //string SelMyBotName = "";
        //string SelSharedBotName = "";

        r2rBot selBot = new r2rBot();
        r2rBot selSharedBot = new r2rBot();

        MainWindow mainWindow;

        r2rMsgBox msgBox;

        public FirstWindow()
        {
            InitializeComponent();
            lblTitle.Content = ConfigurationManager.AppSettings["AppName"] + " - [ V " + ConfigurationManager.AppSettings["Version"] + " ]";
            msgBox = new r2rMsgBox(this);

            ChangeToLogin();
        }

        private void btnWinMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnWinRestore_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                System.Windows.Forms.Screen crScreen = System.Windows.Forms.Screen.FromPoint(pt);

                if (crScreen.Primary)
                {
                    MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                    MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
                }
                else
                {
                    MaxHeight = double.PositiveInfinity;
                    MaxWidth = double.PositiveInfinity;
                }

                this.WindowState = WindowState.Maximized;
                btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_restore01.png", UriKind.Relative));
                dpMain.Margin = new Thickness(6);
            }
            else
            {
                MaxHeight = double.PositiveInfinity;
                MaxWidth = double.PositiveInfinity;

                this.WindowState = WindowState.Normal;
                btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_max01.png", UriKind.Relative));
                dpMain.Margin = new Thickness(0);
            }
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            if (msgBox.Show("Do you want to exit R2r Suite", "Exit R2r", r2rMsgBoxButtons.OkCancel) == r2rMsgBoxResult.Ok)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

            if (e.ClickCount == 2)
            {

                if (this.WindowState == WindowState.Normal)
                {
                    System.Drawing.Point pt = System.Windows.Forms.Cursor.Position;
                    System.Windows.Forms.Screen crScreen = System.Windows.Forms.Screen.FromPoint(pt);

                    if (crScreen.Primary)
                    {
                        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                        MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
                    }
                    else
                    {
                        MaxHeight = double.PositiveInfinity;
                        MaxWidth = double.PositiveInfinity;
                    }

                    this.WindowState = WindowState.Maximized;
                    btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_restore01.png", UriKind.Relative));
                    dpMain.Margin = new Thickness(6);
                }
                else
                {
                    MaxHeight = double.PositiveInfinity;
                    MaxWidth = double.PositiveInfinity;

                    this.WindowState = WindowState.Normal;
                    btnWinRestoreImage.Source = new BitmapImage(new Uri(@"/JoJoSuite.UI;component/Images/win_max01.png", UriKind.Relative));
                    dpMain.Margin = new Thickness(0);
                }
            }
        }

        private void SetStatus(string msg, StatusState state)
        {
            lblStatus.Content = msg;

            var bc = new BrushConverter();

            lblStatus.Foreground = (Brush)bc.ConvertFrom("#fff");

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

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            sEmail = txtEmail.Text.Trim();
            sPwd = pbPwd.Password;

            if (sEmail.Length == 0)
            {
                SetStatus("Email address cannot be blank.", StatusState.Danger);
                return;
            }
            else
            {
                if (IsValidEmail(sEmail) == false)
                {
                    SetStatus("Not a proper email address.", StatusState.Danger);
                    return;
                }
            }

            if (sPwd.Length == 0)
            {
                SetStatus("Password cannot be blank.", StatusState.Danger);
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;

            if (r2rLib.CheckDBConnection() == false)
            {
                SetStatus("R2r Database Server is not available, please contact R2r administrator or try again later.", StatusState.Danger);
                Mouse.OverrideCursor = null;
                return;
            }

            //r2rTheme t1 = r2rLib.ActiveTheme();
            //var bc = new BrushConverter();

            //Application.Current.Resources["Color1"] = (Brush)bc.ConvertFrom(t1.Color1);
            //Application.Current.Resources["Color2"] = (Brush)bc.ConvertFrom(t1.Color2);
            //Application.Current.Resources["Color3"] = (Brush)bc.ConvertFrom(t1.Color3);
            //Application.Current.Resources["Color4"] = (Brush)bc.ConvertFrom(t1.Color4);

            if (CheckEmail(sEmail, sPwd))
            {
                if (crUser.Active == false)
                {
                    SetStatus("User is not active.", StatusState.Warning);
                    Mouse.OverrideCursor = null;
                    return;
                }
                else
                {
                    //sPwd = pbPwd.Password.Trim();

                    //if (sPwd.Length == 0)
                    //{
                    //    SetStatus("Password cannot be blank.", StatusState.Danger);
                    //    Mouse.OverrideCursor = null;
                    //    return;
                    //}
                    //else
                    //{
                    //    //do LDAP login
                    //    if (DoLogin(sEmail, sPwd))
                    //    {
                    //login successfull

                    SetStatus("Welcome to JoJo Suite.", StatusState.Success);
                    Mouse.OverrideCursor = null;

                    tiBots.Visibility = Visibility.Visible;
                    tiSignIn.Visibility = Visibility.Collapsed;

                    lblWinUser.Content = "Not " + crUser.Name + "? Sign out.";

                    tiBots.Focus();

                    LoadMyBots();

                    LoadSharedBots();

                    //btnTemplateBlank.Visibility = btnTemplateGMB.Visibility = btnTemplateTimer.Visibility = Visibility.Visible;
                    btnTemplateBlank.Visibility = Visibility.Visible;

                    AddOrUpdateAppSettings("r1", crUser.Role.Id.ToString());
                    // UI.Properties.Settings.Default["A4"] = crUser.Role.Id;
                    if (chkRemember.IsChecked == true)
                    {
                        UI.Properties.Settings.Default["A1"] = txtEmail.Text;
                        UI.Properties.Settings.Default["A2"] = pbPwd.Password;
                        UI.Properties.Settings.Default["A3"] = true;
                        UI.Properties.Settings.Default.Save();
                    }
                    else
                    {
                        UI.Properties.Settings.Default["A1"] = "";
                        UI.Properties.Settings.Default["A2"] = "";
                        UI.Properties.Settings.Default["A3"] = false;
                        UI.Properties.Settings.Default.Save();
                    }
                    //}
                    //else
                    //{
                    //    SetStatus("Authentication failed.", StatusState.Danger);
                    //    Mouse.OverrideCursor = null;
                    //    return;
                    //}
                    //}
                }
            }
            else
            {
                SetStatus("Email not found.", StatusState.Warning);

                if (msgBox.Show("Email not found, do you want to Sign Up with JoJo?", "JoJo Sign Up", r2rMsgBoxButtons.OkCancel) == r2rMsgBoxResult.Ok)
                {
                    if (r2rLib.RegisterUser(sEmail))
                    {
                        SetStatus("Email registered with JoJo, you will receive status email shortly.", StatusState.Success);
                    }
                    else
                    {
                        SetStatus("Unable to Sign Up with JoJo, please try again later or contact JoJo administrator.", StatusState.Danger);
                    }
                }

                Mouse.OverrideCursor = null;
            }

            Mouse.OverrideCursor = null;

        }
        public void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
        private bool CheckEmail(string email, string pwd)
        {
            bool res = false;
            res = r2rLib.UserExist(email, pwd, out crUser);
            return res;
        }

        private bool DoLogin(string email, string pwd)
        {
            bool res = false;

            if (email.ToLowerInvariant().Contains(email.Trim().ToLowerInvariant()) && email.ToLowerInvariant().Contains(email.Trim().ToLowerInvariant()))
            {
                res = IsValidateCredentials(email.Trim(), pwd.Trim(), "AUTH");
            }
            return res;
        }

        private string GetloggedinUserName()
        {
            System.Security.Principal.WindowsIdentity currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            return currentUser.Name;
        }

        private bool IsValidateCredentials(string userName, string password, string domain)
        {
            IntPtr tokenHandler = IntPtr.Zero;
            bool isValid = LogonUser(userName, domain, password, 3, 0, ref tokenHandler);
            return isValid;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            //txtEmail.Text = "";
            //pbPwd.Password = "";
            chkRemember.IsChecked = false;
        }

        private void LoadMyBots()
        {
            lbMyBots.Items.Clear();
            lstMyBots = r2rLib.GetBots(crUser.Id);

            lbMyBots.HorizontalContentAlignment = HorizontalAlignment.Stretch;

            foreach (r2rBot bot in lstMyBots)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;

                StackPanel sp1a = new StackPanel();
                sp1a.Orientation = Orientation.Vertical;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\r9.png", UriKind.Relative));
                i1.Tag = bot.Id;
                i1.Height = 32;
                i1.Width = 32;

                Label l1 = new Label();
                l1.Content = bot.Title;
                l1.SetValue(Label.FontWeightProperty, FontWeights.Bold);
                var bc = new BrushConverter();
                l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");

                Label l2 = new Label();
                l2.Content = bot.Functionality;

                sp1a.Children.Add(l1);
                sp1a.Children.Add(l2);

                sp1.Children.Add(i1);
                sp1.Children.Add(sp1a);

                sp1.MouseDown += Sp1_MouseDown;
                sp1.HorizontalAlignment = HorizontalAlignment.Stretch;
                sp1.Background = Brushes.Transparent;
                sp1.Tag = bot;

                lbMyBots.Items.Add(sp1);
            }
        }

        private void Sp1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {

                btnLoadMyBot_Click(btnLoadMyBot, new RoutedEventArgs());

            }
        }

        private void LoadSharedBots()
        {
            lstSharedBots.Clear();
            lbSharedBots.Items.Clear();

            lbSharedBots.HorizontalContentAlignment = HorizontalAlignment.Stretch;

            lstSharedBots = r2rLib.GetSharedBots(crUser.Id);

            foreach (r2rBot bot in lstSharedBots)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;

                StackPanel sp1a = new StackPanel();
                sp1a.Orientation = Orientation.Vertical;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\r9.png", UriKind.Relative));
                i1.Tag = bot.Id;
                i1.Height = 32;
                i1.Width = 32;

                Label l1 = new Label();
                l1.Content = bot.Title;
                l1.SetValue(Label.FontWeightProperty, FontWeights.Bold);
                var bc = new BrushConverter();
                l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");

                StackPanel sp1b = new StackPanel();
                sp1b.Orientation = Orientation.Horizontal;

                Label l1a = new Label();
                l1a.Content = bot.Functionality;

                Image i2 = new Image();
                i2.Source = new BitmapImage(new Uri(@"\images\view01.png", UriKind.Relative));
                i2.ToolTip = "View Only";

                i2.Tag = bot.Id;
                i2.Height = 12;
                i2.Width = 12;

                if (bot.BotAccess == 1)
                {
                    i2.Source = new BitmapImage(new Uri(@"\images\run01.png", UriKind.Relative));
                    i2.ToolTip = "Run Access";
                }
                else if (bot.BotAccess == 2)
                {
                    i2.Source = new BitmapImage(new Uri(@"\images\full01.png", UriKind.Relative));
                    i2.ToolTip = "Full Control";
                }

                sp1b.Children.Add(i2);
                sp1b.Children.Add(l1a);

                sp1a.Children.Add(l1);
                sp1a.Children.Add(sp1b);

                sp1.Children.Add(i1);
                sp1.Children.Add(sp1a);

                sp1.MouseDown += Sp1_MouseDown1;
                sp1.HorizontalAlignment = HorizontalAlignment.Stretch;
                sp1.Background = Brushes.Transparent;
                sp1.Tag = bot;

                lbSharedBots.Items.Add(sp1);
            }
        }

        private void Sp1_MouseDown1(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                btnLoadSharedBot_Click(btnLoadSharedBot, new RoutedEventArgs());
            }
        }

        private void btnTemplateBlank_Click(object sender, RoutedEventArgs e)
        {
            BotBlankWindow newBot = new BotBlankWindow();

            newBot.Owner = this;
            newBot.crUser = crUser;

            if (newBot.ShowDialog() == true)
            {
                mainWindow = new MainWindow(newBot.crBot, crUser);
                mainWindow.Owner = this;

                mainWindow.WindowState = this.WindowState;

                if (this.WindowState == WindowState.Normal)
                {
                    mainWindow.Width = this.Width;
                    mainWindow.Height = this.Height;
                    mainWindow.Left = this.Left;
                    mainWindow.Top = this.Top;
                }

                this.Hide();
                mainWindow.ShowDialog();
                if (mainWindow.isClose)
                {
                    System.Windows.Application.Current.Shutdown();
                    return;
                }
                if (mainWindow.isSignOut)
                {
                    ChangeToLogin();
                }
                else
                {
                    //LoadMyBots();
                    btnBackMain.Visibility = Visibility.Visible;
                }
                this.Show();

                this.WindowState = mainWindow.WindowState;
                this.Width = mainWindow.Width;
                this.Height = mainWindow.Height;
                this.Left = mainWindow.Left;
                this.Top = mainWindow.Top;
            }
        }

        static bool IsValidEmail(string mail)
        {
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void lbMyBots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMyBots.SelectedItem != null)
            {
                StackPanel sp1 = (StackPanel)lbMyBots.SelectedItem;
                selBot = (r2rBot)sp1.Tag;
                Image i1 = (Image)sp1.Children[0];
                Label l1 = (Label)((StackPanel)sp1.Children[1]).Children[0];
            }
        }

        private void lbSharedBots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSharedBots.SelectedItem != null)
            {
                StackPanel sp1 = (StackPanel)lbSharedBots.SelectedItem;
                selSharedBot = (r2rBot)sp1.Tag;
                Image i1 = (Image)sp1.Children[0];
                Label l1 = (Label)((StackPanel)sp1.Children[1]).Children[0];
            }
        }

        private void btnDelMyBot_Click(object sender, RoutedEventArgs e)
        {
            if (selBot.Id > 0)
            {
                if (msgBox.Show("Delete this bot?\n" + selBot.Title, "DELETE", r2rMsgBoxButtons.OkNoCancel) == r2rMsgBoxResult.Ok)
                {
                    Mouse.OverrideCursor = Cursors.Wait;

                    if (r2rLib.DeleteBot(selBot.Id))
                    {
                        LoadMyBots();
                    }

                    Mouse.OverrideCursor = null;
                }
            }
        }

        private void btnLoadMyBot_Click(object sender, RoutedEventArgs e)
        {
            if (selBot.Id > 0)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                //PleaseWait pleaseWait = new PleaseWait();

                mainWindow = new MainWindow(selBot, crUser);
                mainWindow.Owner = this;

                mainWindow.WindowState = this.WindowState;

                if (this.WindowState == WindowState.Normal)
                {
                    mainWindow.Width = this.Width;
                    mainWindow.Height = this.Height;
                    mainWindow.Left = this.Left;
                    mainWindow.Top = this.Top;

                    mainWindow.dpMain.Margin = new Thickness(0);
                }
                else
                {
                    mainWindow.MaxHeight = this.MaxHeight;
                    mainWindow.MaxWidth = this.MaxWidth;

                    mainWindow.dpMain.Margin = new Thickness(6);
                }

                this.Hide();

                //pleaseWait.Show();
                //Thread.Sleep(1000);
                //pleaseWait.Hide();

                mainWindow.ShowDialog();

                if (mainWindow.isClose)
                {
                    System.Windows.Application.Current.Shutdown();
                    return;
                }
                if (mainWindow.isSignOut)
                {
                    ChangeToLogin();
                }
                else
                {
                    btnBackMain.Visibility = Visibility.Visible;
                }

                this.Show();

                this.WindowState = mainWindow.WindowState;

                this.Width = mainWindow.Width;
                this.Height = mainWindow.Height;
                this.Left = mainWindow.Left;
                this.Top = mainWindow.Top;

                if (this.WindowState == WindowState.Normal)
                {
                    dpMain.Margin = new Thickness(0);
                }
                else
                {
                    dpMain.Margin = new Thickness(6);
                }
            }
        }

        private void btnLoadSharedBot_Click(object sender, RoutedEventArgs e)
        {
            if (selSharedBot.Id > 0)
            {
                Mouse.OverrideCursor = Cursors.Wait;

                mainWindow = new MainWindow(selSharedBot, crUser);
                mainWindow.Owner = this;

                mainWindow.WindowState = this.WindowState;

                if (this.WindowState == WindowState.Normal)
                {
                    mainWindow.Width = this.Width;
                    mainWindow.Height = this.Height;
                    mainWindow.Left = this.Left;
                    mainWindow.Top = this.Top;
                }

                this.Hide();
                mainWindow.ShowDialog();
                if (mainWindow.isClose)
                {
                    System.Windows.Application.Current.Shutdown();
                    return;
                }
                if (mainWindow.isSignOut)
                {
                    ChangeToLogin();
                }
                else
                {
                    btnBackMain.Visibility = Visibility.Visible;
                }
                this.Show();

                this.WindowState = mainWindow.WindowState;
                this.Width = mainWindow.Width;
                this.Height = mainWindow.Height;
                this.Left = mainWindow.Left;
                this.Top = mainWindow.Top;
            }
        }

        private void btnBackMain_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainWindow.ShowDialog();

            if (mainWindow.isClose)
            {
                System.Windows.Application.Current.Shutdown();
                return;
            }
            if (mainWindow.isSignOut)
            {
                ChangeToLogin();
            }
            this.Show();

            this.WindowState = mainWindow.WindowState;

            if (this.WindowState == WindowState.Normal)
            {
                dpMain.Margin = new Thickness(0);
            }
            else
            {
                dpMain.Margin = new Thickness(6);
            }

            this.Width = mainWindow.Width;
            this.Height = mainWindow.Height;
            this.Left = mainWindow.Left;
            this.Top = mainWindow.Top;

        }

        private void btnWinUser_Click(object sender, RoutedEventArgs e)
        {
            ChangeToLogin();
            //btnWinUser.ContextMenu.IsOpen = true;
        }

        private void ChangeToLogin()
        {
            tiBots.Visibility = Visibility.Collapsed;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            btnBackMain.Visibility = Visibility.Collapsed;
            // btnTemplateBlank.Visibility = btnTemplateGMB.Visibility = btnTemplateTimer.Visibility = Visibility.Collapsed;
            btnTemplateBlank.Visibility = Visibility.Collapsed;
            lblWinUser.Content = "";
            tiSignIn.Visibility = Visibility.Visible;
            txtEmail.Focus();
            tiSignIn.IsSelected = true;


            bool remPwd = false;

            Boolean.TryParse(UI.Properties.Settings.Default["A3"].ToString(), out remPwd);

            if (remPwd)
            {
                pbPwd.Password = UI.Properties.Settings.Default["A2"].ToString();
            }

        }

        private void btnReloadSharedBot_Click(object sender, RoutedEventArgs e)
        {
            LoadSharedBots();
        }

        private void btnReloadMyBot_Click(object sender, RoutedEventArgs e)
        {
            LoadMyBots();
        }

        private void miSignOut_Click(object sender, RoutedEventArgs e)
        {
            ChangeToLogin();
        }
    }
}
