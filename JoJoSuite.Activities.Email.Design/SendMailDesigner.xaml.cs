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
    // Interaction logic for SendMailDesigner.xaml
    public partial class SendMailDesigner
    {
        public SendMailDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SendMail),
                new DesignerAttribute(typeof(SendMailDesigner)),
                new DescriptionAttribute("Send Mail"),
                new ToolboxBitmapAttribute(typeof(SendMail), "Icons.Email_SendEmail.png"));
        }
    }
}
