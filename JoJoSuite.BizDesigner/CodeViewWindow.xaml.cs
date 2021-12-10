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


namespace JoJoSuite.Business.Designer
{
    /// <summary>
    /// Interaction logic for NewWindow.xaml
    /// </summary>
    public partial class CodeViewWindow : Window
    {
       

        public r2rBot crBot = new r2rBot();

        

        public CodeViewWindow()
        {
            InitializeComponent();        

            pnlStatus.Children.Clear();
            
        }

        public CodeViewWindow(r2rBot crBot)
        {
            InitializeComponent();

            this.crBot = crBot;

            txtXaml.Text = this.crBot.XAML;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtXaml.Text.Trim().Length == 0)
            {
                
                return;
            }

            crBot.XAML = txtXaml.Text;
            this.DialogResult = true;
            this.Close();
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

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void txtBotHrs_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
