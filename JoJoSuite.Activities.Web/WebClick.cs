using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Drawing;
using JoJoSuite.Library.Web;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web
{
    [ToolboxBitmap(typeof(WebClick), "Webclick.png")]
    public sealed class WebClick : NativeActivity<string>
    {
        public WebClick()
        {
            this.DisplayName = "Mouse Click";
        }

        r2rWebClick oLib = new r2rWebClick();
     
        [DisplayName("Connection")]
        [Category("Input")]
        [Description("WebDriver")]
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

        [Category("Input")]
        [Description("Wait until to load")]
        [DisplayName("WaitToLoad")]
        [DefaultValue(false)]
        public bool WaitToLoad { get; set; }


        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            //if (WebDriver == null)
            //{
            //    metadata.AddValidationError("Value for required activity argument 'Connection' was not supplied");
            //}
        }
        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);
            oLib.WebElement = context.GetValue(this.WebElement);
            oLib.Xpath = context.GetValue(this.XPath);
            oLib.WaitingTime = Convert.ToInt32(context.GetValue(this.WaitTime) == 0 ? 30 : context.GetValue(this.WaitTime));
            oLib.WaitToload = this.WaitToLoad;


            bool res = oLib.DoAction();
            if (res)
            {
                this.Result.Set(context, res.ToString());
            }
            else
            {
                this.Result.Set(context, oLib.ErrorMessage.ToString());
            }
        }
    }
}
