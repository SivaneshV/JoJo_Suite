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

namespace JoJoSuite.Activities.IO.Design
{
    // Interaction logic for CreateFileDesigner.xaml
    public partial class CreateFileDesigner
    {
        public CreateFileDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(CreateFile),
                new DesignerAttribute(typeof(CreateFileDesigner)),
                new DescriptionAttribute("Create File"),
                new ToolboxBitmapAttribute(typeof(CreateFile), "Icons.IO_CreateFile.png"));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog() == true)
            {
                System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(dlg.FileName);
                this.ModelItem.Properties["Filename"].SetValue(a1);
            }
        }


    }
}
