using System;
using System.Collections.Generic;
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
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Drawing;

namespace JoJoSuite.Actions.Office.Excel.Design
{
    // Interaction logic for AddSheetDesigner.xaml
    public partial class GetSheetDesigner
    {
        public GetSheetDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(GetSheet),
                new DesignerAttribute(typeof(GetSheetDesigner)),
                new DescriptionAttribute("Get Sheet"),
                new ToolboxBitmapAttribute(typeof(GetSheet), "Icons.Excel_SelectSheet.png"));
        }
    }
}
