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
    // Interaction logic for DeleteFileDesigner.xaml
    public partial class DeleteFileDesigner
    {
        public DeleteFileDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(DeleteFile),
                new DesignerAttribute(typeof(DeleteFileDesigner)),
                new DescriptionAttribute("Delete File"),
                new ToolboxBitmapAttribute(typeof(DeleteFile), "Icons.IO_DeleteFile.png"));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(ofd.FileName);
                this.ModelItem.Properties["Filename"].SetValue(a1);
            }
        }
    }
}
