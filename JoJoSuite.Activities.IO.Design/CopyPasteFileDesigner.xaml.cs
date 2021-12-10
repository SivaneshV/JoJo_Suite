using System.Windows;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;

namespace JoJoSuite.Activities.IO.Design
{
    // Interaction logic for CopyFileDesigner.xaml
    public partial class CopyPasteFileDesigner
    {
        public CopyPasteFileDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(CopyPasteFile),
                new DesignerAttribute(typeof(CopyPasteFileDesigner)),
                new DescriptionAttribute("CopyPaste File"),
                new ToolboxBitmapAttribute(typeof(CopyPasteFile), "Icons.IO_CopyPasteFile.png"));
        }

        

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(dlg.FileName);
                this.ModelItem.Properties["Filename"].SetValue(a1);
            }
        }

        private void ButtonDest_Click(object sender, RoutedEventArgs e)
        {
            
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Activities.InArgument<string> a1 = new System.Activities.InArgument<string>(dlg.SelectedPath);
                this.ModelItem.Properties["PasteFolderPath"].SetValue(a1);
            }
        }
    }
}
