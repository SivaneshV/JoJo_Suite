using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Web;
using System.Data;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web
{
    public sealed class GetCollections : NativeActivity<object>
    {
        public GetCollections()
        {
            this.DisplayName = "Read Collection";
        }

        r2rGetCollections oLib = new r2rGetCollections();
    
        [Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }
        [Category("Input")]
        [Description("WebElement")]
        [DefaultValue(null)]
        public InArgument<IWebElement> WebElement { get; set; }

        //[Browsable(false)]
        [Category("Input")]
        [Description("Please provide Xpath details")]
        [DefaultValue(null)]
        public InArgument<string> XPath { get; set; }

        [Category("Input")]
        [Description("Please provide WaitTime")]
        [DefaultValue(1)]
        public InArgument<Int32> WaitTime { get; set; }

        [Category("Output")]
        [DefaultValue(null)]
        public OutArgument<IReadOnlyCollection<IWebElement>> ValuesList { get; set; }
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            if (WebDriver == null)
            {
                metadata.AddValidationError("Value for required activity argument 'Connection' was not supplied");
            }
        }
        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);
            oLib.WebElement = context.GetValue(this.WebElement);
            oLib.Xpath = context.GetValue(this.XPath);
            oLib.WaitingTime = Convert.ToInt32(context.GetValue(this.WaitTime) == 0 ? 30 : context.GetValue(this.WaitTime));
            bool res = oLib.DoAction();

            if (res)
            {
                ValuesList.Set(context, oLib.OutputCollections);
                //context.SetValue(ValuesList, oLib.OutputCollections);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
                this.ValuesList = new OutArgument<IReadOnlyCollection<IWebElement>>();
            }

        }
    }
}
