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

namespace r2rStudio.Designer
{
    /// <summary>
    /// Interaction logic for DC1.xaml
    /// </summary>
    public partial class DC1 : Window
    {
        public DC1()
        {
            InitializeComponent();

            this.DataContext = this;
        }
    }
}
