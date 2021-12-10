using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Resources;

namespace WebIntegra2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChromiumWebBrowser chrome = new ChromiumWebBrowser();

        public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Cef.IsInitialized == false)
            {
                Cef.Initialize();
            }

            chrome.Address = "http://www.maps.google.com";
            //chrome.Address = "chrome://version/";

            Grid.SetRow(chrome, 1);
            Grid.SetColumn(chrome, 0);

            mainGrid.Children.Add(chrome);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Cef.Shutdown();
        }

        private void btnDev_Click(object sender, RoutedEventArgs e)
        {
            chrome.ShowDevTools();
        }

        private void btnCustomHtml_Click(object sender, RoutedEventArgs e)
        {
            chrome.LoadHtml("Hello World", "http://customrendering/");
        }

        private void btnAddress_Click(object sender, RoutedEventArgs e)
        {
            chrome.Load(txtAddress.Text);
        }

        private void btnVersion_Click(object sender, RoutedEventArgs e)
        {
            chrome.Address = "chrome://version/";
            chrome.Reload();
        }

        private void btnRegisterObj_Click(object sender, RoutedEventArgs e)
        {
            
            mainGrid.Children.Remove(chrome);

            var page = new Uri(string.Format("file:///{0}HTMLResources/html/WinformInteractionExample.html", GetAppLocation()));

            chrome = new ChromiumWebBrowser();

            chrome.Address = page.ToString();

            JavaScriptInteractionObj jsObj = new JavaScriptInteractionObj();
            jsObj.SetChromeBrowser(chrome);

            chrome.RegisterJsObject("wpfObj", jsObj);

            Grid.SetRow(chrome, 1);
            Grid.SetColumn(chrome, 0);

        }

        private void btnRunJs_Click(object sender, RoutedEventArgs e)
        {

            var script = "document.body.style.backgroundColor = 'red';";

            chrome.ExecuteScriptAsync(script);

        }

        private void btnRunJs2_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function tempFunction() {");
            sb.AppendLine("     var w = window.innerWidth;");
            sb.AppendLine("     var h = window.innerHeight;");
            sb.AppendLine("");
            sb.AppendLine("     return w*h;");
            sb.AppendLine("}");
            sb.AppendLine("tempFunction();");

            var task = chrome.EvaluateScriptAsync(sb.ToString());

            task.ContinueWith(t => {
                if (!t.IsFaulted)
                {
                    var res = t.Result;

                    if (res.Success == true)
                    {
                        MessageBox.Show(res.Result.ToString());

                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
