using r2rStudio.Activities.Database;
using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JoJoSuite.Activities.Database.Design
{
    // Interaction logic for ActivityDesigner1.xaml
    public partial class ConnectToServerDesigner
    {
        public ConnectToServerDesigner()
        {
            InitializeComponent();
        }

        private static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(ConnectToServer), new DesignerAttribute(typeof(ConnectToServerDesigner)));
            builder.AddCustomAttributes(typeof(ConnectToServer), new DescriptionAttribute("sample activity"));


        }
    }
}
