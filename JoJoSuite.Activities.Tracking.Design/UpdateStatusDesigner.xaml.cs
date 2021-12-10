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

namespace JoJoSuite.Activities.Tracking.Design
{
    // Interaction logic for UpdateStatusDesigner.xaml
    public partial class UpdateStatusDesigner
    {
        public UpdateStatusDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(UpdateStatus),
                new DesignerAttribute(typeof(UpdateStatusDesigner)),
                new DescriptionAttribute("Update Status"),
                new ToolboxBitmapAttribute(typeof(UpdateStatus), "Icons.Tracking_UpdateStatus.png"));
        }
    }
}
