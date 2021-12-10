using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.ComponentModel;

namespace r2rStudio.Designer
{
    public partial class wfdhSample : Window
    {
        private WorkflowDesigner wd;

        public wfdhSample()
        {
            InitializeComponent();

            RegisterMetadata();
            AddDesigner();
            AddToolBox();
            AddPropertyInspector();

            //System.Activities.Statements.
        }

        private void AddDesigner()
        {
            this.wd = new WorkflowDesigner();
            Grid.SetColumn(this.wd.View, 1);
            this.wd.Load(new Flowchart());

            grid1.Children.Add(this.wd.View);
        }

        private void RegisterMetadata()
        {
            DesignerMetadata dm = new DesignerMetadata();
            dm.Register();
        }

        private ToolboxControl GetToolBoxControl()
        {
            ToolboxControl ctrl = new ToolboxControl();

            ToolboxCategory category = new ToolboxCategory("Magesh");

            //ToolboxItemWrapper tool1 = new ToolboxItemWrapper("System.Activities.Statements.Assign", typeof(Assign).Assembly.FullName, null, "Assign");
            //ToolboxItemWrapper tool2 = new ToolboxItemWrapper("System.Activities.Statements.Sequence", typeof(Sequence).Assembly.FullName, null, "Sequence");
            //ToolboxItemWrapper tool3 = new ToolboxItemWrapper("System.Activities.Statements.Delay", typeof(Sequence).Assembly.FullName, null, "Delay");
            ToolboxItemWrapper tool4 = new ToolboxItemWrapper("NumberGuessWorkflowActivities", typeof(Sequence).Assembly.FullName, null, "Delay");

            //category.Add(tool1);
            //category.Add(tool2);
            //category.Add(tool3);
            category.Add(tool4);

            ctrl.Categories.Add(category);

            return ctrl;
        }

        private void AddToolBox()
        {
            ToolboxControl tc = GetToolBoxControl();
            Grid.SetColumn(tc, 0);
            grid1.Children.Add(tc);
        }

        private void AddPropertyInspector()
        {
            Grid.SetColumn(wd.PropertyInspectorView, 2);
            grid1.Children.Add(wd.PropertyInspectorView);
        }
    }
}
