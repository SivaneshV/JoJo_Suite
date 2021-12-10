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
    public partial class RegionWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        List<r2rRegion> lstRegion = new List<r2rRegion>();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        r2rRegion crRegion = new r2rRegion();

        bool isNew = false;

        public RegionWindow()
        {
            InitializeComponent();

            LoadRegions();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text.Trim().Length == 0)
            {
                SetStatus("Title cannot be blank.", StatusState.Danger);
                return;
            }

            crRegion.Title = txtTitle.Text.Trim();
            crRegion.Active = (chkActive.IsChecked == true);

            if (isNew)
            {
                crRegion.Id = r2rLib.AddRegion(crRegion);

                if (crRegion.Id > 0)
                {
                    StackPanel sp1 = new StackPanel();
                    sp1.Orientation = Orientation.Horizontal;
                    sp1.Tag = crRegion;

                    StackPanel sp1a = new StackPanel();
                    sp1a.Orientation = Orientation.Vertical;

                    Image i1 = new Image();
                    i1.Source = new BitmapImage(new Uri(@"\images\region01.png", UriKind.Relative));
                    i1.Height = 32;
                    i1.Width = 32;

                    Label l1 = new Label();
                    l1.Content = crRegion.Title;
                    l1.SetValue(Label.FontWeightProperty, FontWeights.Bold);
                    var bc = new BrushConverter();

                    if (crRegion.Active)
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

                    lbRegion.Items.Add(sp1);

                    SetStatus("Region added successfully.", StatusState.Success);
                    return;
                }
                else
                {
                    SetStatus("Not able to add new Region.", StatusState.Danger);
                    return;
                }
            }
            else
            {
                if (r2rLib.UpdateRegion(crRegion))
                {
                    if (lbRegion.SelectedIndex > 0)
                    {
                        StackPanel sp1 = (StackPanel)lbRegion.SelectedItem;
                        StackPanel sp1a = (StackPanel)sp1.Children[1];
                        Label l1 = (Label)sp1a.Children[0];
                        l1.Content = crRegion.Title;

                        var bc = new BrushConverter();

                        if (crRegion.Active)
                        {
                            l1.Foreground = (Brush)bc.ConvertFrom("#03a9f4");
                        }
                        else
                        {
                            l1.Foreground = Brushes.Red;
                        }
                    }

                    SetStatus("Region updated successfully.", StatusState.Success);
                    return;
                }
                else
                {
                    SetStatus("Not able to update Region.", StatusState.Danger);
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = crRegion.Title;
            chkActive.IsChecked = crRegion.Active;
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

        private void LoadRegions()
        {
            lstRegion.Clear();

            lbRegion.Items.Clear();

            Mouse.OverrideCursor = Cursors.Wait;

            lstRegion = r2rLib.GetRegions();

            foreach (r2rRegion obj in lstRegion)
            {
                StackPanel sp1 = new StackPanel();
                sp1.Orientation = Orientation.Horizontal;
                sp1.Tag = obj;

                StackPanel sp1a = new StackPanel();
                sp1a.Orientation = Orientation.Vertical;

                Image i1 = new Image();
                i1.Source = new BitmapImage(new Uri(@"\images\region01.png", UriKind.Relative));
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

                lbRegion.Items.Add(sp1);
            }

            Mouse.OverrideCursor = null;

        }

        private void lbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbRegion.SelectedItem == null)
            {
                return;
            }

            isNew = false;

            StackPanel sp1 = (StackPanel)lbRegion.SelectedItem;

            crRegion = (r2rRegion)sp1.Tag;

            if (crRegion.Id > 0)
            {
                txtTitle.Text = crRegion.Title;
                chkActive.IsChecked = crRegion.Active;
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadRegions();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            txtTitle.Text = "";
            chkActive.IsChecked = false;
            txtTitle.Focus();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
