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
using System.Activities.Presentation.Model;
using System.Activities;
using Microsoft.Office.Interop.Excel;
using JoJoSuite.Actions.Office.Excel;

namespace JoJoSuite.Business.Designer
{
    // Interaction logic for OpenWorkbookDesigner.xaml
    public partial class OpenWorkbookDesigner
    {
        public OpenWorkbookDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(OpenWorkbook),
                new DesignerAttribute(typeof(OpenWorkbookDesigner)),
                new DescriptionAttribute("Open Excel File"),
                new ToolboxBitmapAttribute(typeof(OpenWorkbook), "Icons.Excel_OpenExcelFile.png"));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Excel files|*.xls*|CSV files|*.csv|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(ofd.FileName);
                this.ModelItem.Properties["FilePath"].SetValue(a1);
            }
        }


        private void btnCreateConnection_Click(object sender, RoutedEventArgs e)
        {
            ModelItem model = this.ModelItem.Root;

            bool bExists = false;

            object eV = new object();

            foreach (var v1 in model.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;

                if (v2.Type.ToString().Contains("Excel.Workbook"))
                {
                    eV = v2;
                    bExists = true;
                    break;
                }
            }

            if (bExists)
            {
                MessageBoxResult mbRes = MessageBox.Show("ExcelConnection " + ((Variable)eV).Name + " already exists. \nYES - Use this variable or \nNO - create a new variable?", "Existing WebDriver found", MessageBoxButton.YesNoCancel);

                if (mbRes == MessageBoxResult.Yes)
                {
                    System.Activities.OutArgument<Microsoft.Office.Interop.Excel.Workbook> a1 = new System.Activities.OutArgument<Microsoft.Office.Interop.Excel.Workbook>((Variable)eV);
                    this.ModelItem.Properties["xlWorkBook"].SetValue(eV);
                }
                if (mbRes == MessageBoxResult.No)
                {
                    Variable<Workbook> v3 = new Variable<Workbook>("ExcelConnection_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    model.Properties["Variables"].Collection.Add(v3);

                    System.Activities.OutArgument<Workbook> a1 = new System.Activities.OutArgument<Workbook>(v3);
                    this.ModelItem.Properties["xlWorkBook"].SetValue(a1);

                }
            }
            else
            {
                Variable<Workbook> v3 = new Variable<Workbook>("ExcelConnection_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
                model.Properties["Variables"].Collection.Add(v3);

                System.Activities.OutArgument<Workbook> a1 = new System.Activities.OutArgument<Workbook>(v3);
                this.ModelItem.Properties["xlWorkBook"].SetValue(a1);
            }
        }

    }
}
