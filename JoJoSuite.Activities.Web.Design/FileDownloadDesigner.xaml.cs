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
using System.ComponentModel;
using System.Drawing;
using System.Activities;
using System.Activities.Presentation.Metadata;

namespace JoJoSuite.Activities.Web.Design
{
    // Interaction logic for FileDownloadDesigner.xaml
    public partial class FileDownloadDesigner
    {
        public FileDownloadDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(FileDownload),
                new DesignerAttribute(typeof(FileDownloadDesigner)),
                new DescriptionAttribute("File Download"),
                new ToolboxBitmapAttribute(typeof(FileDownload), "Icons.Web_DownloadFile.png"));
        }
    }
}
