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
using System.Windows.Forms;

namespace JoJoSuite.Activities.Email.Design
{
    // Interaction logic for MailMoveToFolderDesigner.xaml
    public partial class MailMoveToFolderDesigner
    {
        public MailMoveToFolderDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(MailMoveToFolder),
                new DesignerAttribute(typeof(MailMoveToFolderDesigner)),
                new DescriptionAttribute("Mail move to another folder"),
                new ToolboxBitmapAttribute(typeof(MailMoveToFolder), "Icons.Email_OpenEmailAttachments.png"));
        }

     

    }
}
