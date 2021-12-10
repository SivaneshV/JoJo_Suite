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

    public partial class CodeWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        List<r2rRole> lstRole = new List<r2rRole>();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        r2rRole crRole = new r2rRole();

        bool isNew = false;

        public CodeWindow()
        {
            InitializeComponent();

            LoadRoles();

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //if (txtTitle.Text.Trim().Length == 0)
            //{
            //    SetStatus("Title cannot be blank.", StatusState.Danger);
            //    return;
            //}

            //crRole.Title = txtTitle.Text.Trim();
            //crRole.Active = (chkActive.IsChecked == true);

            //if (isNew)
            //{
            //    crRole.Id = r2rLib.AddRole(crRole);

            //    if (crRole.Id > 0)
            //    {
            //        StackPanel sp1 = new StackPanel();
            //        sp1.Orientation = Orientation.Horizontal;
            //        sp1.Tag = crRole;

            //        StackPanel sp1a = new StackPanel();
            //        sp1a.Orientation = Orientation.Vertical;

            //        Image i1 = new Image();
            //        i1.Source = new BitmapImage(new Uri(@"\images\role01.png", UriKind.Relative));
            //        i1.Height = 32;
            //        i1.Width = 32;

            //        Label l1 = new Label();
            //        l1.Content = crRole.Title;
            //        l1.SetValue(Label.FontWeightProperty, FontWeights.Bold);
            //        var bc = new BrushConverter();

            //        if (crRole.Active)
            //        {
            //            l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");
            //        }
            //        else
            //        {
            //            l1.Foreground = Brushes.Red;
            //        }

            //        sp1a.Children.Add(l1);

            //        sp1.Children.Add(i1);
            //        sp1.Children.Add(sp1a);

            //        lbRole.Items.Add(sp1);

            //        SetStatus("Role added successfully.", StatusState.Success);
            //        return;
            //    }
            //    else
            //    {
            //        SetStatus("Not able to add new Role.", StatusState.Danger);
            //        return;
            //    }
            //}
            //else
            //{
            //    if (r2rLib.UpdateRole(crRole))
            //    {
            //        if (lbRole.SelectedIndex > 0)
            //        {
            //            StackPanel sp1 = (StackPanel)lbRole.SelectedItem;
            //            StackPanel sp1a = (StackPanel)sp1.Children[1];
            //            Label l1 = (Label)sp1a.Children[0];
            //            l1.Content = crRole.Title;

            //            var bc = new BrushConverter();

            //            if (crRole.Active)
            //            {
            //                l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");
            //            }
            //            else
            //            {
            //                l1.Foreground = Brushes.Red;
            //            }
            //        }

            //        SetStatus("Role updated successfully.", StatusState.Success);
            //        return;
            //    }
            //    else
            //    {
            //        SetStatus("Not able to update Role.", StatusState.Danger);
            //        return;
            //    }
            //}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //txtTitle.Text = crRole.Title;
            //chkActive.IsChecked = crRole.Active;
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

        private void LoadRoles()
        {
            lstRole.Clear();

            lbRole.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            lstRole = r2rLib.GetRoles();

            foreach (r2rRole obj in lstRole)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;
                sp1.Tag = obj;

                StackPanel sp1a = new StackPanel();
                sp1a.Orientation = Orientation.Vertical;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\role01.png", UriKind.Relative));
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

                sp1a.Children.Add(l1);

                sp1.Children.Add(i1);
                sp1.Children.Add(sp1a);

                lbRole.Items.Add(sp1);
            }

            Mouse.OverrideCursor = null;

        }

        private void lbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbRole.SelectedItem == null)
            {
                return;
            }

            isNew = false;

            StackPanel sp1 = (StackPanel)lbRole.SelectedItem;

            crRole = (r2rRole)sp1.Tag;

            if (crRole.Id > 0)
            {
                //txtTitle.Text = crRole.Title;
                //chkActive.IsChecked = crRole.Active;
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadRoles();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            //txtTitle.Text = "";
            //chkActive.IsChecked = false;
            //txtTitle.Focus();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
