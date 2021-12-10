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
using JoJoSuite.Activities.Web.Design;
using System.Activities.Presentation.Model;
using System.Activities;

namespace JoJoSuite.Activities.Database.Design
{
    // Interaction logic for DataQueryDesigner.xaml
    public partial class DataQueryDesigner
    {
        public DataQueryDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(DataQuery),
                new DesignerAttribute(typeof(DataQueryDesigner)),
                new DescriptionAttribute("Select Query"),
                new ToolboxBitmapAttribute(typeof(DataQuery), "Icons.Database_SelectQuery.png"));
        }
        private void btnVars_Click(object sender, RoutedEventArgs e)
        {
            bool bMulti = false;
            int vCount = 0;

            object v2a = new object();

            frmList f1 = new frmList();
            ModelItem model = this.ModelItem.Root;

            f1.lbVars.Items.Clear();

            foreach (var v1 in model.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;

                if (v2.Type.ToString().Contains("SqlConnection"))
                {
                    v2a = v2;

                    f1.lbVars.Items.Add(v2.Name);
                    vCount++;
                }
            }

            bMulti = (vCount > 1);

            if (bMulti)
            {
                if (f1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var v1 in model.Properties["Variables"].Collection)
                    {
                        var v2 = v1.GetCurrentValue() as Variable;

                        if (v2.Type.ToString().Contains("SqlConnection"))
                        {
                            if (v2.Name == f1.lbVars.SelectedItem.ToString())
                            {
                                System.Activities.InArgument<SqlConnection> a1 = new System.Activities.InArgument<SqlConnection>(v2);
                                this.ModelItem.Properties["Connection"].SetValue(a1);
                            }
                        }
                    }
                }
            }
            else
            {
                if (vCount > 0)
                {
                    System.Activities.InArgument<SqlConnection> a1 = new System.Activities.InArgument<SqlConnection>(v2a as Variable);
                    this.ModelItem.Properties["Connection"].SetValue(a1);
                }
            }
        }
    }
}
