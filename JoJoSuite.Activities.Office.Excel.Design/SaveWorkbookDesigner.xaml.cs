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
    // Interaction logic for SaveWorkbookDesigner.xaml
    public partial class SaveWorkbookDesigner
    {
        public SaveWorkbookDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SaveWorkbook),
                new DesignerAttribute(typeof(SaveWorkbookDesigner)),
                new DescriptionAttribute("Save Workbook"),
                new ToolboxBitmapAttribute(typeof(SaveWorkbook), "Icons.Excel_SaveExcel.png"));
        }
    }
}
