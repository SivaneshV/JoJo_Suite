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
using System.Windows.Forms;

namespace JoJoSuite.Activities.IO.Design
{
    // Interaction logic for CreateFolderDesigner.xaml
    public partial class CreateFolderDesigner
    {
        public CreateFolderDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(CreateFolder),
                new DesignerAttribute(typeof(CreateFolderDesigner)),
                new DescriptionAttribute("Create Folder"),
                new ToolboxBitmapAttribute(typeof(CreateFolder), "Icons.IO_CreateFolder.png"));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(dlg.SelectedPath);
                this.ModelItem.Properties["FolderPath"].SetValue(a1);
            }
        }
    }
}
