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

namespace JoJoSuite.Activities.IO.Design
{
    // Interaction logic for InputBoxDesigner.xaml
    public partial class InputBoxDesigner
    {
        public InputBoxDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(InputBox),
                new DesignerAttribute(typeof(InputBoxDesigner)),
                new DescriptionAttribute("Input Box"),
                new ToolboxBitmapAttribute(typeof(InputBox), "Icons.IO_InputBox.png"));
        }
    }
}
