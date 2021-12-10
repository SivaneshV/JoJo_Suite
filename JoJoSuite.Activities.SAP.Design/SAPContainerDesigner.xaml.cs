using JoJoSuite.Activities.SAP;
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
    // Interaction logic for SAPContainerDesigner.xaml
    public partial class SAPContainerDesigner
    {
        public SAPContainerDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SAPContainer),
                new DesignerAttribute(typeof(SAPContainerDesigner)),
                new DescriptionAttribute("Container"),
                new ToolboxBitmapAttribute(typeof(SAPContainer), "Icons.SAP_Container.png"));
        }
    }
}
