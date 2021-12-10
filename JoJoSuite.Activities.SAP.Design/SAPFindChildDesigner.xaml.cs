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
    // Interaction logic for SAPFindChildDesigner.xaml
    public partial class SAPFindChildDesigner
    {
        public SAPFindChildDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SAPFindChild),
            new DesignerAttribute(typeof(SAPFindChildDesigner)),
            new DescriptionAttribute("Find Child"),
            new ToolboxBitmapAttribute(typeof(SAPFindChild), "Icons.SAP_FindChild.png"));
        }
    }
}
