using JoJoSuite.Business.Designer;
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
using System.Windows.Threading;

namespace JoJoSuite.UI
{
    public partial class SplashWindow : Window
    {

        DispatcherTimer dispatchTimer = new DispatcherTimer();

        int count = 0;

        public SplashWindow()
        {
            InitializeComponent();
            lblVersion.Content= "V " + ConfigurationManager.AppSettings["Version"];
            lblA.Visibility = lblB.Visibility = lblC.Visibility = lblD.Visibility = Visibility.Collapsed;

            r2rLib r2rLib = new r2rLib(ConfigurationManager.AppSettings["r2rDbConStr"]);

            if (r2rLib.CheckDBConnection() == false)
            {
                MessageBox.Show("R2r Database Connection Not Available.", "R@r", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            r2rTheme t1 = r2rLib.ActiveTheme();
            var bc = new BrushConverter();

            Application.Current.Resources["ThemeColor1"] = (Brush)bc.ConvertFrom(t1.Color1);
            Application.Current.Resources["ThemeColor2"] = (Brush)bc.ConvertFrom(t1.Color2);
            Application.Current.Resources["ThemeColor3"] = (Brush)bc.ConvertFrom(t1.Color3);
            Application.Current.Resources["ThemeColor4"] = (Brush)bc.ConvertFrom(t1.Color4);

        }

        private void DispatchTimer_Tick(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
            {
                lblA.Visibility = Visibility.Visible;
            }

            if (count == 2)
            {
                lblB.Visibility = Visibility.Visible;
            }

            if (count == 3)
            {
                lblC.Visibility = Visibility.Visible;
            }

            if (count == 4)
            {
                lblD.Visibility = Visibility.Visible;
            }

            if (count > 4)
            {
                dispatchTimer.Stop();
                FirstWindow fw1 = new FirstWindow();
                fw1.Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatchTimer.Tick += DispatchTimer_Tick;
            dispatchTimer.Interval = new TimeSpan(0, 0, 1);
            dispatchTimer.Start();
        }
    }
}
