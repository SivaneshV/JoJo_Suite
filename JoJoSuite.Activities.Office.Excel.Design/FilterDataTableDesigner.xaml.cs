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
using Microsoft.Win32;

namespace JoJoSuite.Actions.Office.Excel.Design
{
    // Interaction logic for OpenWorkbookDesigner.xaml
    public partial class FilterDataTableDesigner
    {
        public FilterDataTableDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(FilterDataTable),
                new DesignerAttribute(typeof(FilterDataTableDesigner)),
                new DescriptionAttribute("Filter DataTable"),
                new ToolboxBitmapAttribute(typeof(FilterDataTable), "Icons.Excel_SelectSheet.png"));
        }


    }
}
