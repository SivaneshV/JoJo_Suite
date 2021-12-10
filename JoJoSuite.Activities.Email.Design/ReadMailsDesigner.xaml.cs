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
    // Interaction logic for ReadMailDesigner.xaml
    public partial class ReadMailDesigner
    {
        public ReadMailDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(ReadMails),
                new DesignerAttribute(typeof(ReadMailDesigner)),
                new DescriptionAttribute("Read Mails"),
                new ToolboxBitmapAttribute(typeof(ReadMails), "Icons.Email_ReadEmails.png"));
        }
    }
}
