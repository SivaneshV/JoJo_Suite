using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

namespace JoJoSuite.Activities.SharePoint.Design
{
    // Interaction logic for DownloadSharepoint.xaml
    public partial class DownloadSharepoint
    {
        public DownloadSharepoint()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(DownloadSharePoint),
                new DesignerAttribute(typeof(DownloadSharepoint)),
                new DescriptionAttribute("Site URL"),
                new ToolboxBitmapAttribute(typeof(DownloadSharePoint), "Icons.Sharepoint_Download.png"));
        }
    }
}
