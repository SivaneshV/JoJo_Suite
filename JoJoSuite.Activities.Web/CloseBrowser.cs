using System;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Web;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web
{

    public sealed class CloseBrowser : NativeActivity<object>
    {
        r2rCloseBrowser oLib = new r2rCloseBrowser();

        [RequiredArgument, Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }
        public CloseBrowser()
        {
            this.DisplayName = "Close Browser";
        }
        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);           
            bool res = oLib.DoAction();
            if (res)
            {
                this.Result.Set(context, oLib.Error.ToString());
            }
            else
            {
                this.Result.Set(context, res.ToString());
            }

        }
    }
}
