using JoJoSuite.Business.Lib;
using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// Interaction logic for importSapScript.xaml
    /// </summary>
    public partial class SapImportWindow : Window
    {
        enum StatusState { Info, Warning, Success, Danger };

        public r2rBot crBot = new r2rBot();
        public r2rUser crUser = new r2rUser();

        r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

        public List<r2rSapActivity> lstSapActs = new List<r2rSapActivity>();

        public List<string> VariableList = new List<string>();
        public SapImportWindow()
        {
            InitializeComponent();
            ChangeVariables();
        }

        private void ChangeVariables()
        {
            
            this.Dispatcher.BeginInvoke(new Action(()=>{
                drpVariables.ItemsSource = VariableList;
            }));
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtScript.Text.Trim().Length == 0)
            {
                SetStatus("SAP Script cannot be blank.", StatusState.Danger);
                return;
            }
            else
            {
                var data = txtScript.Text.Split(new[] { "session" }, StringSplitOptions.None);

                           
                // Write here
                for (int i = 0; i < data.Length; i++)
                {
                    // Show a message box with its contents.
                    string tempLine = data.GetValue(i).ToString();
                    string line = tempLine.Trim().ToLower();

                    //Console.WriteLine(line);

                    if (line.Contains(").text"))
                    {
                        // Then Do this
                        CreateSapAct(tempLine, "text");

                    }
                    else if (line.Contains(").press"))
                    {
                        // Then Do this
                        CreateSapAct(tempLine, "press");

                    }
                    else if (line.Contains(").select")&& !line.Contains(").selected"))
                    {
                        // Then Do this
                        CreateSapAct(tempLine, "select");

                    }
                    else if (line.Contains(").key"))
                    {
                        // Then Do this
                        CreateSapAct(tempLine, "key");

                    }
                    else if (line.Contains(").selected"))
                    {
                        // Then Do this
                        CreateSapAct(tempLine, "selected");

                    }
                    else if (line.Contains(").sendvkey"))
                    {
                        // Then Do this
                        CreateSapAct(tempLine, "enter");

                    }
                }
            }

            this.DialogResult = true;
            this.Close();
         
        }

        public void CreateSapAct(string line, string type)
        {
            r2rSapActivity act1 = new r2rSapActivity();

            if (type == "text")
            {
                act1.Name = "SapSetText";

                var reg = new Regex("\".*?\"");
                var matches = reg.Matches(line);
                string path = matches[0].ToString();
                path = path.Replace('"', ' ').Trim();

                string value = matches[1].ToString();
                value = value.Replace('"', ' ').Trim();

                act1.Path = path;
                act1.Value = value;
                act1.AddContainer =(bool)ChkAddContainer.IsChecked;
                act1.Variable = Convert.ToString(drpVariables.SelectedItem);
            }
            else if (type == "press")
            {
                act1.Name = "SapClick";

                var reg = new Regex("\".*?\"");
                var matches = reg.Matches(line);
                string path = matches[0].ToString();
                path = path.Replace('"', ' ').Trim();

                act1.Path = path;
                act1.AddContainer = (bool)ChkAddContainer.IsChecked;
                act1.Variable = Convert.ToString(drpVariables.SelectedItem);
            }
            else if (type == "select")
            {
                act1.Name = "SapSelectTab";

                var reg = new Regex("\".*?\"");
                var matches = reg.Matches(line);
                string path = matches[0].ToString();
                path = path.Replace('"', ' ').Trim();

                act1.Path = path;
                act1.Value = "true";
                act1.AddContainer = (bool)ChkAddContainer.IsChecked;
                act1.Variable = Convert.ToString(drpVariables.SelectedItem);
            }
            else if (type == "selected")
            {
                act1.Name = "SapCheckBox";

                var reg = new Regex("\".*?\"");
                var matches = reg.Matches(line);
                string path = matches[0].ToString();
                path = path.Replace('"', ' ').Trim();

                act1.Path = path;
                string value = Regex.Replace(line, "^.*(?:=)", string.Empty);
                value = value.Replace('"', ' ').Trim();
                act1.Value = Regex.Replace(value, @"(?:\r\n|\n|\r)", string.Empty);
                act1.AddContainer = (bool)ChkAddContainer.IsChecked;
                act1.Variable = Convert.ToString(drpVariables.SelectedItem);
            }
            else if (type == "key")
            {
                act1.Name = "SapDropdown";

                var reg = new Regex("\".*?\"");
                var matches = reg.Matches(line);
                string path = matches[0].ToString();
                path = path.Replace('"', ' ').Trim();

                act1.Path = path;
                string value = matches[1].ToString();
                value = value.Replace('"', ' ').Trim();
                act1.Value = Regex.Replace(value, @"(?:\r\n|\n|\r)", string.Empty);
                act1.AddContainer = (bool)ChkAddContainer.IsChecked;
                act1.Variable = Convert.ToString(drpVariables.SelectedItem);
            }
            else if (type == "enter")
            {
                act1.Name = "SapSendKeys";

                var reg = new Regex("\".*?\"");
                var matches = reg.Matches(line);
                string path = matches[0].ToString();
                path = path.Replace('"', ' ').Trim();
                act1.Path = path;

                string value = line.Split(' ').Last();
                act1.Value = value;
                act1.AddContainer = (bool)ChkAddContainer.IsChecked;
                act1.Variable = Convert.ToString(drpVariables.SelectedItem);
            }

            lstSapActs.Add(act1);

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

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }

    public class r2rSapActivity
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Value { get; set; }
        public string Variable { get; set; }
        public bool AddContainer { get; set; }
    }
}
