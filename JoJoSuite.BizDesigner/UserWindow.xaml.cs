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
    public partial class UserWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        List<r2rUser> lstUser = new List<r2rUser>();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        r2rUser crUser = new r2rUser();

        public UserWindow()
        {
            InitializeComponent();

            LoadUsers();
            FillTeams();
            FillRoles();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            if (txtName.Text.Trim().Length == 0)
            {
                SetStatus("Please type Name.", StatusState.Warning);
                return;
            }

            if (cboRole.SelectedIndex < 0)
            {
                SetStatus("Please select Role.", StatusState.Warning);
                return;
            }

            if (cboTeam.SelectedIndex < 0)
            {
                SetStatus("Please select Team.", StatusState.Warning);
                return;
            }

            crUser.Active = (chkActive.IsChecked == true);
            crUser.Name = txtName.Text.Trim();
            crUser.Role.Id = Convert.ToInt32(cboRole.SelectedValue);
            crUser.Team.Id = Convert.ToInt32(cboTeam.SelectedValue);

            if (r2rLib.UpdateUser(crUser))
            {
                if (lbUsers.SelectedIndex >= 0)
                {
                    StackPanel sp1 = (StackPanel)lbUsers.SelectedItem;

                    StackPanel sp1a = (StackPanel)sp1.Children[1];

                    Label l1 = (Label)sp1a.Children[0];

                    l1.Content = crUser.Name;

                    var bc = new BrushConverter();

                    if (crUser.Active)
                    {
                        l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");
                    }
                    else
                    {
                        l1.Foreground = Brushes.Red;
                    }
                }

                SetStatus("User updated successfully.", StatusState.Success);
                return;
            }
            else
            {
                SetStatus("Not able to update User.", StatusState.Danger);
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            chkActive.IsChecked = crUser.Active;

            cboRole.SelectedItem = null;

            if (crUser.Role != null)
            {
                cboRole.SelectedValue = crUser.Role.Id;

            }

            cboTeam.SelectedItem = null;

            if (crUser.Team != null)
            {
                cboTeam.SelectedValue = crUser.Team.Id;
            }
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

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void LoadUsers()
        {
            lstUser.Clear();

            lbUsers.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            if (cboFilterRole.SelectedIndex <= 0 && cboFilterTeam.SelectedIndex <= 0)
            {
                lstUser = r2rLib.GetUsers();
            }
            else
            {
                int role = 0;
                int team = 0;
                Label l1 = (Label)cboFilterRole.SelectedValue;
                Label l2 = (Label)cboFilterTeam.SelectedValue;

                role = Convert.ToInt32(l1.Tag);
                team = Convert.ToInt32(l2.Tag);

                if (role > 0 && team == 0)
                {
                    lstUser = r2rLib.GetUsersByRole(role);
                }
                else if (role == 0 && team > 0)
                {
                    lstUser = r2rLib.GetUsersByTeam(team);
                }
                else if (role > 0 && team > 0)
                {
                    lstUser = r2rLib.GetUsersByRoleAndTeam(role, team);
                }
            }

            foreach (r2rUser obj in lstUser)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;
                sp1.Tag = obj;

                StackPanel sp1a = new StackPanel();
                sp1a.Orientation = Orientation.Vertical;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\user01.png", UriKind.Relative));
                i1.Height = 32;
                i1.Width = 32;

                Label l1 = new Label();
                l1.Content = obj.Name;
                l1.SetValue(Label.FontWeightProperty, FontWeights.Bold);
                var bc = new BrushConverter();

                if (obj.Active)
                {
                    l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");
                }
                else
                {
                    l1.Foreground = Brushes.Red;
                }

                Label l2 = new Label();
                l2.Content = obj.Email;

                sp1a.Children.Add(l1);
                sp1a.Children.Add(l2);

                sp1.Children.Add(i1);
                sp1.Children.Add(sp1a);

                lbUsers.Items.Add(sp1);
            }

            Mouse.OverrideCursor = null;

        }

        private void FillTeams()
        {
            List<r2rTeam> lstTeam = r2rLib.GetTeams();

            cboTeam.ItemsSource = lstTeam;

            cboTeam.DisplayMemberPath = "Title";
            cboTeam.SelectedValuePath = "Id";

            Label l1 = new Label();
            l1.Content = "ALL TEAMS";
            l1.Tag = 0;

            cboFilterTeam.Items.Add(l1);

            foreach (r2rTeam team in lstTeam)
            {
                Label l1a = new Label();
                l1a.Content = team.Title;
                l1a.Tag = team.Id;

                cboFilterTeam.Items.Add(l1a);
            }

            cboFilterTeam.SelectedIndex = 0;
        }

        private void FillRoles()
        {
            List<r2rRole> lstRole = r2rLib.GetRoles();

            cboRole.ItemsSource = lstRole;

            cboRole.DisplayMemberPath = "Title";
            cboRole.SelectedValuePath = "Id";

            cboFilterRole.Items.Clear();

            Label l1 = new Label();
            l1.Content = "ALL ROLES";
            l1.Tag = 0;

            cboFilterRole.Items.Add(l1);

            foreach(r2rRole role in lstRole)
            {
                Label l1a = new Label();
                l1a.Content = role.Title;
                l1a.Tag = role.Id;

                cboFilterRole.Items.Add(l1a);
            }
            cboFilterRole.SelectedIndex = 0;

        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbUsers.SelectedItem == null)
            {
                return;
            }

            StackPanel sp1 = (StackPanel)lbUsers.SelectedItem;

            crUser = (r2rUser)sp1.Tag;

            if (crUser.Id > 0)
            {
                txtName.Text = crUser.Name;
                txtEmail.Text = crUser.Email;
                chkActive.IsChecked = crUser.Active;

                cboRole.SelectedItem = null;

                if (crUser.Role.Id > 0)
                {
                    cboRole.SelectedValue = crUser.Role.Id;
                }

                cboTeam.SelectedItem = null;

                if (crUser.Team.Id > 0)
                {
                    cboTeam.SelectedValue = crUser.Team.Id;
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
