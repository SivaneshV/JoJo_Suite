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
using System.Data.SqlClient;
using System.Activities.Presentation.Model;
using System.Activities;

namespace JoJoSuite.Activities.Database.Design
{
    // Interaction logic for ConnectDesigner.xaml
    public partial class ConnectDesigner
    {
        public ConnectDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(Connect),
                new DesignerAttribute(typeof(ConnectDesigner)),
                new DescriptionAttribute("Connect To DB Server"),
                new ToolboxBitmapAttribute(typeof(Connect), "Icons.Database_ConnectToDBServer.png"));
        }

        private void btnCreateConnection_Click(object sender, RoutedEventArgs e)
        {
            ModelItem model = this.ModelItem.Root;
            bool bExists = false;

            object eV = new object();

            foreach (var v1 in model.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;

                if (v2.Type.ToString().Contains("SqlConnection"))
                {
                    eV = v2;
                    bExists = true;
                    break;
                }
            }

            if (bExists)
            {
                MessageBoxResult mbRes = MessageBox.Show("SqlConncectionDriver " + ((Variable)eV).Name + " already exists. \nYES - Use this variable or \nNO - create a new variable?", "Existing SqlConncectionDriver found", MessageBoxButton.YesNoCancel);

                if (mbRes == MessageBoxResult.Yes)
                {
                    System.Activities.OutArgument<SqlConnection> a1 = new System.Activities.OutArgument<SqlConnection>((Variable)eV);
                    this.ModelItem.Properties["Connection"].SetValue(a1);
                }
                if (mbRes == MessageBoxResult.No)
                {
                    Variable<SqlConnection> v3 = new Variable<SqlConnection>("SqlConncectionDriver_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    model.Properties["Variables"].Collection.Add(v3);

                    System.Activities.OutArgument<SqlConnection> a1 = new System.Activities.OutArgument<SqlConnection>(v3);
                    this.ModelItem.Properties["Connection"].SetValue(a1);

                }
            }
            else
            {
                Variable<SqlConnection> v3 = new Variable<SqlConnection>("SqlConncectionDriver_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
                model.Properties["Variables"].Collection.Add(v3);

                System.Activities.OutArgument<SqlConnection> a1 = new System.Activities.OutArgument<SqlConnection>(v3);
                this.ModelItem.Properties["Connection"].SetValue(a1);
            }
        }
    }
}
