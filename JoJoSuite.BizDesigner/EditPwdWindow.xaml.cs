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
    /// <summary>
    /// Interaction logic for EditPwdWindow.xaml
    /// </summary>
    public partial class EditPwdWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        public int btnRes = 0;
        //mode 0-add, 1-edit
        public int mode = 0;
        public int Id = 0;

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);
        r2rBotPassword crPwd = new r2rBotPassword();

        public EditPwdWindow(r2rBotPassword Pwd, int Mode)
        {
            InitializeComponent();
            crPwd = Pwd;
            mode = Mode;

            if (mode == 1)
            {
                txtName.IsEnabled = false;
                txtName.Text = crPwd.Name;
                pwdPwd.Password = crPwd.Decrypt();
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            if (txtName.Text.Trim().Length == 0)
            {
                SetStatus("Key cannot be blank.", StatusState.Danger);
                return;
            }

            if (pwdPwd.Password.Trim().Length == 0)
            {
                SetStatus("Password cannot be blank.", StatusState.Danger);
                return;
            }

            crPwd.Name = txtName.Text;
            crPwd.Password = pwdPwd.Password;
            //crPwd.Encrypt();

            crPwd.Password = crPwd.Encrypt();

            if (mode == 0)
            {
                if (r2rLib.AddPassword(crPwd) > 0)
                {
                    //SetStatus("Password added successfully.", StatusState.Success);

                    btnRes = 0;
                    this.Close();
                }
                else
                {
                    SetStatus("Unable to add password.", StatusState.Danger);
                }
            }
            else
            {
                if (r2rLib.UpdatePassword(crPwd) == true)
                {

                    //SetStatus("Password updated successfully.", StatusState.Success);
                    btnRes = 0;
                    this.Close();
                }
                else
                {
                    SetStatus("Unable to update password.", StatusState.Danger);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnRes = 1;
            this.Close();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            btnRes = 1;
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
    }
}
