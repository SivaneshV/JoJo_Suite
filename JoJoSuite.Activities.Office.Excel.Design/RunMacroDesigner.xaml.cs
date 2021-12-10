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

namespace JoJoSuite.Actions.Office.Excel.Design
{
    // Interaction logic for RunMacroDesigner.xaml
    public partial class RunMacroDesigner
    {
        public RunMacroDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(RunMacro),
                new DesignerAttribute(typeof(RunMacroDesigner)),
                new DescriptionAttribute("Run Excel Macro"),
                new ToolboxBitmapAttribute(typeof(RunMacro), "Icons.Excel_RunMacro.png"));
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
    }
}
