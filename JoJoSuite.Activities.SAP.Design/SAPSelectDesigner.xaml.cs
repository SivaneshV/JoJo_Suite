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
    // Interaction logic for SAPSelectDesigner.xaml
    public partial class SAPSelectDesigner
    {
        public SAPSelectDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SAPSelect),
                new DesignerAttribute(typeof(SAPSelectDesigner)),
                new DescriptionAttribute("Select"),
                new ToolboxBitmapAttribute(typeof(SAPSelect), "Icons.SAP_Select.png"));
        }
    }
}
