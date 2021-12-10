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

namespace JoJoSuite.Activities.ClipBoard.Design
{
    // Interaction logic for CopyDesigner.xaml
    public partial class CopyDesigner
    {
        public CopyDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(Copy),
                new DesignerAttribute(typeof(CopyDesigner)),
                new DescriptionAttribute("Copy"),
                new ToolboxBitmapAttribute(typeof(Copy), "Icons.ClipBoard_Copy.png"));
        }
    }
}
