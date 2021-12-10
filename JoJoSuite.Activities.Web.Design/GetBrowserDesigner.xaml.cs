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
using System.Activities;
using System.Activities.Presentation.Model;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web.Design
{
    // Interaction logic for GetBrowserDesigner.xaml
    public partial class GetBrowserDesigner
    {
        public GetBrowserDesigner()
        {
            InitializeComponent();
        }
   

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(GetBrowser),
                new DesignerAttribute(typeof(GetBrowserDesigner)),
                new DescriptionAttribute("Open Browser"),
                new ToolboxBitmapAttribute(typeof(GetBrowser), "Icons.Web_OpenBrowser.png"));
        }



        private void WorkflowItemPresenter_Initialized(object sender, EventArgs e)
        {
            MessageBox.Show("WorkflowItemPresenter_Initialized");
        }

        private void WorkflowItemPresenter_DragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("WorkflowItemPresenter_DragEnter");
        }

        private void btnCreateDriver1_Click(object sender, RoutedEventArgs e)
        {
            ModelItem model = this.ModelItem.Root;

            bool bExists = false;

            object eV = new object();

            foreach (var v1 in model.Properties["Variables"].Collection)
            {
                var v2 = v1.GetCurrentValue() as Variable;

                if (v2.Type.ToString().Contains("IWebDriver"))
                {
                    eV = v2;
                    bExists = true;
                    break;
                }
            }

            if (bExists)
            {
                MessageBoxResult mbRes = MessageBox.Show("WebDriver " + ((Variable)eV).Name + " already exists. \nYES - Use this variable or \nNO - create a new variable?", "Existing WebDriver found", MessageBoxButton.YesNoCancel);

                if (mbRes == MessageBoxResult.Yes)
                {
                    System.Activities.OutArgument<IWebDriver> a1 = new System.Activities.OutArgument<IWebDriver>((Variable)eV);
                    this.ModelItem.Properties["BrowserDriver"].SetValue(a1);
                }
                if (mbRes == MessageBoxResult.No)
                {
                    Variable<IWebDriver> v3 = new Variable<IWebDriver>("WebDriver_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    model.Properties["Variables"].Collection.Add(v3);

                    System.Activities.OutArgument<IWebDriver> a1 = new System.Activities.OutArgument<IWebDriver>(v3);
                    this.ModelItem.Properties["BrowserDriver"].SetValue(a1);

                }
            }
            else
            {
                Variable<IWebDriver> v3 = new Variable<IWebDriver>("WebDriver_" + DateTime.Now.ToString("ddMMyyyyhhmmss"));
                model.Properties["Variables"].Collection.Add(v3);

                System.Activities.OutArgument<IWebDriver> a1 = new System.Activities.OutArgument<IWebDriver>(v3);
                this.ModelItem.Properties["BrowserDriver"].SetValue(a1);
            }
        }
    }
}
