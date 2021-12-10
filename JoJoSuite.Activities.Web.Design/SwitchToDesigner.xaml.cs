using OpenQA.Selenium;
using System;
using System.Activities;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Model;
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

namespace JoJoSuite.Activities.Web.Design
{
    // Interaction logic for SwitchToDesigner.xaml
    public partial class SwitchToDesigner
    {
        public SwitchToDesigner()
        {
            InitializeComponent();
        }
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(SwitchTo),
                new DesignerAttribute(typeof(SwitchToDesigner)),
                new DescriptionAttribute("Switch To"),
                new ToolboxBitmapAttribute(typeof(SwitchTo), "Icons.Web_SwitchTo.png"));
        }

        /// <summary>
        /// TO assign driver variable  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (v2.Type.ToString().Contains("IWebDriver"))
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

                        if (v2.Type.ToString().Contains("IWebDriver"))
                        {
                            if (v2.Name == f1.lbVars.SelectedItem.ToString())
                            {
                                System.Activities.InArgument<IWebDriver> a1 = new System.Activities.InArgument<IWebDriver>(v2);
                                this.ModelItem.Properties["WebDriver"].SetValue(a1);
                            }
                        }
                    }
                }
            }
            else
            {
                if (vCount > 0)
                {
                    System.Activities.InArgument<IWebDriver> a1 = new System.Activities.InArgument<IWebDriver>(v2a as Variable);
                    this.ModelItem.Properties["WebDriver"].SetValue(a1);
                }
            }
        }
    }
}
