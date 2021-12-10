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
    // Interaction logic for CountTrackerDesigner.xaml
    public partial class CountTrackerDesigner
    {
        public CountTrackerDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(CountTracker),
                new DesignerAttribute(typeof(CountTrackerDesigner)),
                new DescriptionAttribute("Log Transactions"),
                new ToolboxBitmapAttribute(typeof(CountTracker), "Icons.Tracking_LogTransactions.png"));
        }
    }
}
