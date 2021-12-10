using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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

using JoJoSuite.Business.Lib;

namespace JoJoSuite.Business.Designer
{
    /// <summary>
    /// Interaction logic for BotBlankWindow.xaml
    /// </summary>
    public partial class BotBlankWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        public r2rBot crBot = new r2rBot();
        public r2rUser crUser = new r2rUser();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        public BotBlankWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtBotTitle.Text.Trim().Length == 0)
            {
                SetStatus("Title cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotFunctionality.Text.Trim().Length == 0)
            {
                SetStatus("Functionality cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotBenefit.Text.Trim().Length == 0)
            {
                SetStatus("Quantative benefits, cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotPeople.Text.Trim().Length == 0)
            {
                SetStatus("No of people, cannot be blank.", StatusState.Danger);
                return;
            }

            if (txtBotHrs.Text.Trim().Length == 0)
            {
                SetStatus("Average hours, cannot be blank.", StatusState.Danger);
                return;
            }

            int noOfPpl = 0;
            int manualHrs = 0;

            Int32.TryParse(txtBotPeople.Text.Trim(), out noOfPpl);
            Int32.TryParse(txtBotHrs.Text.Trim(), out manualHrs);

            if (noOfPpl <= 0)
            {
                SetStatus("Please enter numeric value for No of people.", StatusState.Danger);
                return;
            }

            if (manualHrs <= 0)
            {
                SetStatus("Please enter numeric value for Manual hours.", StatusState.Danger);
                return;
            }
            if (Convert.ToInt32(((ComboBoxItem)cboBotType.SelectedItem).Tag.ToString())==0)
            {
                SetStatus("Please select transaction type.", StatusState.Danger);
                return;
            }

            crBot.Title = txtBotTitle.Text.Trim();
            crBot.Functionality = txtBotFunctionality.Text.Trim();
            crBot.Benefit = txtBotBenefit.Text.Trim();
            crBot.CreatedBy = crUser;
            crBot.Type = Convert.ToInt32(((ComboBoxItem)cboBotType.SelectedItem).Tag.ToString());
            crBot.XAML = "";
            crBot.CreatedBy = crUser;
            crBot.ApproverAdmin = new r2rUser() { Id = 0 };
            crBot.ApproverManager = new r2rUser() { Id = 0 };
            crBot.Team = new r2rTeam() { Id = 0 };
            crBot.ApprovedByAdmin = false;
            crBot.ApprovedByManager = false;
            crBot.NumberOfPeople = noOfPpl;
            crBot.ManualMinutes = manualHrs;
            crBot.Applications = txtBotApps.Text.Trim();
            crBot.Technologies = txtBotTech.Text.Trim();
            crBot.isProduction = Convert.ToBoolean(Convert.ToInt16(((ComboBoxItem)cboProduction.SelectedItem).Tag)); 
            crBot.Id = r2rLib.AddBot(crBot);

            if (crBot.Id > 0)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                SetStatus("Not able to create Bot.", StatusState.Danger);
                return;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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
                lblStatus.Foreground = (Brush)Application.Current.Resources["ThemeColor4"];
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

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void txtBotHrs_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
