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
using System.Drawing;

namespace JoJoSuite.Activities.SAP.Design
{
    // Interaction logic for SAPSetFocusDesigner.xaml
    public partial class SAPSetFocusDesigner
    {
        public SAPSetFocusDesigner()
        {
            InitializeComponent();
        }
      

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SAPSetFocus),
                new DesignerAttribute(typeof(SAPSetFocusDesigner)),
                new DescriptionAttribute("Set Focus"),
                new ToolboxBitmapAttribute(typeof(SAPSetFocus), "Icons.SAP_SetFocus.png"));
        }
    }
}
