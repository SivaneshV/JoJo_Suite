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
    // Interaction logic for MoveFileDesigner.xaml
    public partial class MoveFileDesigner
    {
        public MoveFileDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(MoveFile),
                new DesignerAttribute(typeof(MoveFileDesigner)),
                new DescriptionAttribute("Move File"),
                new ToolboxBitmapAttribute(typeof(MoveFile), "Icons.IO_MoveFile.png"));
        }
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(dlg.FileName);
                this.ModelItem.Properties["SourcePath"].SetValue(a1);
            }
        }

    }
}
