
using System;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Web;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web
{
    public sealed class GetBrowser : NativeActivity<object>
    {
       public GetBrowser()
        {
            this.DisplayName = "Open Browser";
        }

        r2rOpenSite oLib = new r2rOpenSite();

        [Category("Input")]
        [Description("Select browser type")]
        [DisplayName("Browser Type")]
        public r2rWebBrowserType BrowserType { get; set; }

        [Category("Input")]
        [Description("Please provide Open URL")]
        [DisplayName("URL")]
        [DefaultValue(null)]
        public InArgument<string> URL { get; set; }

        [Category("Input")]
        [Description("Please provide Download Path")]
        [DisplayName("Download Path")]
        [DefaultValue(null)]
        public InArgument<string> DownloadPath { get; set; }

        [DefaultValue(null)]
        public Activity Body { get; set; }

        [Category("Output")]
        public OutArgument<IWebDriver> BrowserDriver { get; set; }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            if (BrowserDriver == null)
            {
                metadata.AddValidationError("Value for required activity argument 'Connection' was not supplied");
            }
        }
        protected override void Execute(NativeActivityContext context)
        {
            oLib.Browser = this.BrowserType.ToString();// context.GetValue()//"Chrome" ;
            oLib.Url = context.GetValue(this.URL);
            oLib.DownloadPath = @context.GetValue(this.DownloadPath);
            bool res = oLib.DoAction();

            if (res)
            {
                BrowserDriver.Set(context, oLib.WebDriver);
                Result.Set(context, oLib.WebDriver);
            }
            else
            {
                Result.Set(context, new Exception(oLib.ErrorMessage));
            }

            if (this.Body != null)
            {
                context.ScheduleActivity(this.Body);
            }
        }
    }
    
    public enum r2rWebBrowserType
    {
        Chrome,
        Firefox,
        IE
    }
}


