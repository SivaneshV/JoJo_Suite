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
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        List<r2rTeam> lstTeam = new List<r2rTeam>();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        r2rTeam crTeam = new r2rTeam();

        bool isNew = false;

        public TeamWindow()
        {
            InitializeComponent();

            LoadTeams();
            FillRegions();
            FillManagers();
            FillL2s();
            FillL3s();
            FillL4s();

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text.Trim().Length == 0)
            {
                SetStatus("Title cannot be blank.", StatusState.Danger);
                return;
            }

            if (cboManager.SelectedIndex < 0)
            {
                SetStatus("Please select Manager.", StatusState.Danger);
                return;
            }

            if (cboL4.SelectedIndex < 0)
            {
                SetStatus("Please select L4.", StatusState.Danger);
                return;
            }

            if (cboL3.SelectedIndex < 0)
            {
                SetStatus("Please select L3.", StatusState.Danger);
                return;
            }

            if (cboL2.SelectedIndex < 0)
            {
                SetStatus("Please select L2.", StatusState.Danger);
                return;
            }

            if (cboRegion.SelectedIndex < 0)
            {
                SetStatus("Please select Region.", StatusState.Danger);
                return;
            }

            crTeam.Title = txtTitle.Text.Trim();
            crTeam.Manager = new r2rUser() { Id = Convert.ToInt32(cboManager.SelectedValue) };
            crTeam.L2 = new r2rUser() { Id = Convert.ToInt32(cboL2.SelectedValue) };
            crTeam.L3 = new r2rUser() { Id = Convert.ToInt32(cboL3.SelectedValue) };
            crTeam.L4 = new r2rUser() { Id = Convert.ToInt32(cboL4.SelectedValue) };
            crTeam.Region = (r2rRegion)cboRegion.SelectedItem;
            crTeam.Active = (chkActive.IsChecked == true);

            if (isNew)
            {
                crTeam.Id = r2rLib.AddTeam(crTeam);

                if (crTeam.Id > 0)
                {
                    StackPanel sp1 = new StackPanel();
                    sp1.Orientation = Orientation.Horizontal;
                    sp1.Tag = crTeam;

                    StackPanel sp1a = new StackPanel();
                    sp1a.Orientation = Orientation.Vertical;

                    Image i1 = new Image();
                    i1.Source = new BitmapImage(new Uri(@"\images\team01.png", UriKind.Relative));
                    i1.Height = 32;
                    i1.Width = 32;

                    Label l1 = new Label();
                    l1.Content = crTeam.Title;
                    l1.SetValue(Label.FontWeightProperty, FontWeights.Bold);
                    var bc = new BrushConverter();

                    if (crTeam.Active)
                    {
                        l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");
                    }
                    else
                    {
                        l1.Foreground = Brushes.Red;
                    }

                    Label l2 = new Label();
                    l2.Content = crTeam.Region.Title;

                    sp1a.Children.Add(l1);
                    sp1a.Children.Add(l2);

                    sp1.Children.Add(i1);
                    sp1.Children.Add(sp1a);

                    lbTeam.Items.Add(sp1);

                    SetStatus("Team added successfully.", StatusState.Success);
                    return;
                }
                else
                {
                    SetStatus("Not able to add new Team.", StatusState.Danger);
                    return;
                }
            }
            else
            {
                if (r2rLib.UpdateTeam(crTeam))
                {
                    if (lbTeam.SelectedIndex > 0)
                    {
                        StackPanel sp1 = (StackPanel)lbTeam.SelectedItem;

                        StackPanel sp1a = (StackPanel)sp1.Children[1];

                        Label l1 = (Label)sp1a.Children[0];

                        l1.Content = crTeam.Title;

                        var bc = new BrushConverter();

                        if (crTeam.Active)
                        {
                            l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");
                        }
                        else
                        {
                            l1.Foreground = Brushes.Red;
                        }
                    }

                    SetStatus("Team updated successfully.", StatusState.Success);
                    return;
                }
                else
                {
                    SetStatus("Not able to update Team.", StatusState.Danger);
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = crTeam.Title;
            chkActive.IsChecked = crTeam.Active;

            cboRegion.SelectedItem = null;

            if (crTeam.Region.Id > 0)
            {
                cboRegion.SelectedValue = crTeam.Region.Id;
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

        private void LoadTeams()
        {
            lstTeam.Clear();

            lbTeam.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            if (cboFilterRegion.SelectedIndex <= 0)
            {
                lstTeam = r2rLib.GetTeams();
            }
            else
            {
                int region = 0;
                Label l1 = (Label)cboFilterRegion.SelectedValue;

                region = Convert.ToInt32(l1.Tag);

                if (region > 0)
                {
                    lstTeam = r2rLib.GetTeams(region);
                }
            }

            foreach (r2rTeam obj in lstTeam)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;
                sp1.Tag = obj;

                StackPanel sp1a = new StackPanel();
                sp1a.Orientation = Orientation.Vertical;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\team01.png", UriKind.Relative));
                i1.Height = 32;
                i1.Width = 32;

                Label l1 = new Label();
                l1.Content = obj.Title;
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
                l2.Content = obj.Region.Title;

                sp1a.Children.Add(l1);
                sp1a.Children.Add(l2);

                sp1.Children.Add(i1);
                sp1.Children.Add(sp1a);

                lbTeam.Items.Add(sp1);
            }

            Mouse.OverrideCursor = null;

        }

        private void FillRegions()
        {
            List<r2rRegion> lstRegion = r2rLib.GetRegions();

            cboRegion.ItemsSource = lstRegion;

            cboRegion.DisplayMemberPath = "Title";
            cboRegion.SelectedValuePath = "Id";

            cboFilterRegion.Items.Clear();

            Label l1 = new Label();
            l1.Content = "ALL REGIONS";
            l1.Tag = 0;

            cboFilterRegion.Items.Add(l1);

            foreach (r2rRegion region in lstRegion)
            {
                Label l1a = new Label();
                l1a.Content = region.Title;
                l1a.Tag = region.Id;

                cboFilterRegion.Items.Add(l1a);
            }
            cboFilterRegion.SelectedIndex = 0;
        }

        private void FillManagers()
        {
            List<r2rUser> lstManager = r2rLib.GetUsersByRole(2);

            cboManager.ItemsSource = lstManager;

            cboManager.DisplayMemberPath = "Name";
            cboManager.SelectedValuePath = "Id";
        }

        private void FillL4s()
        {
            List<r2rUser> lstManager = r2rLib.GetUsersByRole(3);

            cboL4.ItemsSource = lstManager;
            cboL4.DisplayMemberPath = "Name";
            cboL4.SelectedValuePath = "Id";
        }

        private void FillL3s()
        {
            List<r2rUser> lstManager = r2rLib.GetUsersByRole(4);

            cboL3.ItemsSource = lstManager;
            cboL3.DisplayMemberPath = "Name";
            cboL3.SelectedValuePath = "Id";
        }

        private void FillL2s()
        {
            List<r2rUser> lstManager = r2rLib.GetUsersByRole(5);

            cboL2.ItemsSource = lstManager;
            cboL2.DisplayMemberPath = "Name";
            cboL2.SelectedValuePath = "Id";
        }


        private void lbTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbTeam.SelectedItem == null)
            {
                return;
            }

            isNew = false;

            StackPanel sp1 = (StackPanel)lbTeam.SelectedItem;

            crTeam = (r2rTeam)sp1.Tag;

            if (crTeam.Id > 0)
            {
                txtTitle.Text = crTeam.Title;
                chkActive.IsChecked = crTeam.Active;

                cboManager.SelectedItem = null;
                if (crTeam.Manager.Id > 0)
                {
                    cboManager.SelectedValue = crTeam.Manager.Id;
                }

                cboL4.SelectedItem = null;
                if (crTeam.L4.Id > 0)
                {
                    cboL4.SelectedValue = crTeam.L4.Id;
                }

                cboL3.SelectedItem = null;
                if (crTeam.L3.Id > 0)
                {
                    cboL3.SelectedValue = crTeam.L3.Id;
                }

                cboL2.SelectedItem = null;
                if (crTeam.L2.Id > 0)
                {
                    cboL2.SelectedValue = crTeam.L2.Id;
                }

                cboRegion.SelectedItem = null;
                if (crTeam.Region.Id > 0)
                {
                    cboRegion.SelectedValue = crTeam.Region.Id;
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadTeams();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;

            txtTitle.Text = "";
            cboManager.SelectedIndex = -1;
            cboRegion.SelectedIndex = -1;
            chkActive.IsChecked = false;

            txtTitle.Focus();


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
