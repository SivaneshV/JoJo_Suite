using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JoJoSuite.UI
{
    /// <summary>
    /// Interaction logic for WebInspectWindow.xaml
    /// </summary>
    public partial class WebInspectWindow : Window
    {
        public WebInspectWindow()
        {
            InitializeComponent();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wbMain.Navigate(new Uri(txtUrl.Text));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

                //throw;
            }
        }

        mshtml.IHTMLElement e1;

        private void btnInspect_Click(object sender, RoutedEventArgs e)
        {
            mshtml.HTMLDocument doc = (mshtml.HTMLDocument)wbMain.Document;

            
            var sId = e1.id;
            var uId = Guid.NewGuid().ToString();
            e1.id = uId;

            var doc2 = new HtmlAgilityPack.HtmlDocument();
            doc2.LoadHtml(doc.documentElement.outerHTML);

            e1.id = sId;

            var node = doc2.GetElementbyId(uId);

            txtXPath.Text = "";
            if (node != null)
            {
                var xpath = node.XPath;
                txtXPath.Text = xpath.ToString();
            }

        }

        private void wbMain_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            mshtml.HTMLDocument doc;
            doc = (mshtml.HTMLDocument)wbMain.Document;

            mshtml.HTMLDocumentEvents2_Event iEvent;
            iEvent = (mshtml.HTMLDocumentEvents2_Event)doc;
            iEvent.onclick += IEvent_onclick;
        }

        private bool IEvent_onclick(IHTMLEventObj pEvtObj)
        {
            e1 = pEvtObj.srcElement as mshtml.IHTMLElement;

            e1.setAttribute("style", "background-color: red;");

            System.Windows.MessageBox.Show(e1.id);

            return true;
        }
    }
}
