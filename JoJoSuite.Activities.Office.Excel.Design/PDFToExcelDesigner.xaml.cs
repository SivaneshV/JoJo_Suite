using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace JoJoSuite.Actions.Office.Excel.Design
{
    // Interaction logic for CreateWorkbookDesigner.xaml
    public partial class PDFToExcelDesigner
    {
        public PDFToExcelDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(CreateWorkbook),
                new DesignerAttribute(typeof(CreateWorkbookDesigner)),
                new DescriptionAttribute("PDF To Excel File"),
                new ToolboxBitmapAttribute(typeof(CreateWorkbook), @"Icons.PDF_To_Excel.png"));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {


            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {

                    System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(fbd.SelectedPath + "\\New.xlsx");
                    this.ModelItem.Properties["FilePath"].SetValue(a1);


                }
            }

            //OpenFileDialog ofd = new OpenFileDialog();

            //ofd.Filter = "Excel files|*.xls*|CSV files|*.csv|All files (*.*)|*.*";

            //if (ofd.ShowDialog() == true)
            //{
            //    System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(ofd.FileName);
            //    this.ModelItem.Properties["FilePath"].SetValue(a1);
            //}
        }

        private void btnCreateWorkbook_Click(object sender, RoutedEventArgs e)
        {
            //ModelItem model = this.ModelItem.Root;

            //bool bExists = false;

            //object eV = new object();

            //foreach (var v1 in model.Properties["Variables"].Collection)
            //{
            //    var v2 = v1.GetCurrentValue() as Variable;

            //    if (v2.Type.ToString().Contains("Workbook"))
            //    {
            //        eV = v2;
            //        bExists = true;
            //        break;
            //    }
            //}

            //if (bExists)
            //{
            //    MessageBoxResult mbRes = MessageBox.Show("Workbook " + ((Variable)eV).Name + " already exists. \nYES - Use this variable or \nNO - create a new variable?", "Existing Workbook found", MessageBoxButton.YesNoCancel);

            //    if (mbRes == MessageBoxResult.Yes)
            //    {
            //        System.Activities.OutArgument<Workbook> a1 = new System.Activities.OutArgument<Workbook>((Variable)eV);
            //        this.ModelItem.Properties["xlWorkBook"].SetValue(a1);
            //    }
            //    if (mbRes == MessageBoxResult.No)
            //    {
            //        Variable<Workbook> v3 = new Variable<Workbook>("Workbook_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
            //        model.Properties["Variables"].Collection.Add(v3);

            //        System.Activities.OutArgument<Workbook> a1 = new System.Activities.OutArgument<Workbook>(v3);
            //        this.ModelItem.Properties["xlWorkBook"].SetValue(a1);

            //    }
            //}
            //else
            //{
            //    Variable<Workbook> v3 = new Variable<Workbook>("Workbook_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
            //    model.Properties["Variables"].Collection.Add(v3);

            //    System.Activities.OutArgument<Workbook> a1 = new System.Activities.OutArgument<Workbook>(v3);
            //    this.ModelItem.Properties["xlWorkBook"].SetValue(a1);
            //}

        }
    }
}
