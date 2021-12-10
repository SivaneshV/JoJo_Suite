using System;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Web;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web
{

    public sealed class ReDirectUrl : NativeActivity<object>
    {
        r2rReDirectUrl oLib = new r2rReDirectUrl();

        [Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }
        [RequiredArgument, Category("Input")]
        [Description("URL")]
        [DefaultValue("")]
        public InArgument<string> URL { get; set; }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            if (WebDriver == null)
            {
                metadata.AddValidationError("Value for required activity argument 'Connection' was not supplied");
            }
        }
        public ReDirectUrl()
        {
            this.DisplayName = "Redirect Url";
        }
        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);
            oLib.URL = context.GetValue(this.URL);
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
