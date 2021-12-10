using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Web;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web
{
    public sealed class GetText : NativeActivity<string>
    {
        public GetText()
        {
            this.DisplayName = "Read Value";
        }

        r2rGetText oLib = new r2rGetText();
      
        [DisplayName("Connection")]
        [Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }

        [Category("Input")]
        [Description("WebElement")]
        [DefaultValue(null)]
        public InArgument<IWebElement> WebElement { get; set; }


        [Category("Input")]
        [Description("Get Attribute")]
        [DefaultValue(null)]
        public InArgument<string> GetAttribute { get; set; }

        //[Browsable(false)]
        [Category("Input")]
        [DefaultValue(null)]
        public InArgument<string> XPath { get; set; }

        [Category("Input")]
        [Description("Please provide WaitTime")]
        [DefaultValue(1)]
        public InArgument<Int32> WaitTime { get; set; }

        [Category("Input")]
        [Description("Wait until to load")]
        [DisplayName("WaitToLoad")]
        [DefaultValue(false)]
        public bool WaitToLoad { get; set; }

        [Category("OutPut")]    
        [DefaultValue(null)]
        public OutArgument<string> GetValue { get; set; }

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
            oLib.GetAttribute = context.GetValue(this.GetAttribute);
            oLib.WaitingTime = Convert.ToInt32(context.GetValue(this.WaitTime) == 0 ? 30 : context.GetValue(this.WaitTime));
            oLib.WaitToload = this.WaitToLoad;
            bool res = oLib.DoAction();
            if (res)
            {
                GetValue.Set(context, oLib.OutputStr.Trim());
            }
            else
            {
                this.Result.Set(context, oLib.ErrorMessage.ToString());
            }           

        }
    }

}
