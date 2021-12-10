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
namespace JoJoSuite.Activities.Email.Design
{
    // Interaction logic for GetMailDesigner.xaml
    public partial class GetMailDesigner
    {
        public GetMailDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(GetMail),
                new DesignerAttribute(typeof(GetMailDesigner)),
                new DescriptionAttribute("Get Mail"),
                new ToolboxBitmapAttribute(typeof(GetMail), "Icons.email01.png"));
        }
    }
}
