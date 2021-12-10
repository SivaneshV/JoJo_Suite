using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

namespace JoJoSuite.Activities.SAP.Design
{
    // Interaction logic for SAPEnterKeyDesigner.xaml
    public partial class SAPEnterKeyDesigner
    {
        public SAPEnterKeyDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SAPEnterKey),
                new DesignerAttribute(typeof(SAPEnterKeyDesigner)),
                new DescriptionAttribute("Send Keys"),
                new ToolboxBitmapAttribute(typeof(SAPEnterKey), "Icons.SAP_SendKeys.png"));
        }
    }
}
