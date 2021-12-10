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

namespace JoJoSuite.Activities.Tracking.Design
{
    // Interaction logic for LogMessageDesigner.xaml
    public partial class LogMessageDesigner
    {
        public LogMessageDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(LogMessage),
                new DesignerAttribute(typeof(LogMessageDesigner)),
                new DescriptionAttribute("Log Message"),
                new ToolboxBitmapAttribute(typeof(LogMessage), "Icons.Tracking_LogMessage.png"));
        }
    }
}
