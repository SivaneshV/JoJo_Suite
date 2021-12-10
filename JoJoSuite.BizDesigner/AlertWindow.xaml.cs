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
using System.Windows.Shapes;

namespace JoJoSuite.UI
{
    public partial class AlertWindow : Window
    {
        public int btnRes = 0;

        public AlertWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            btnRes = 0;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            btnRes = 1;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnRes = 2;
            this.Close();
        }

        private void gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnWinClose_Click(object sender, RoutedEventArgs e)
        {
            btnRes = 2;
            this.Close();
        }
    }
}
