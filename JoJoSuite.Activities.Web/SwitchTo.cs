using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using JoJoSuite.Library.Web;
using System.ComponentModel;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.Web
{

    public sealed class SwitchTo : NativeActivity<object>
    {
        public SwitchTo()
        {
            this.DisplayName = "Switch To";
        }
        r2rSwitchIframe oLib = new r2rSwitchIframe();

        [ Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }

        [Category("Input")]
        [Description("iframeid Or Name")]
        [DefaultValue(null)]
        public InArgument<string> Iframeid { get; set; }

        [Category("Input")]
        [Description("Please provide WaitTime")]
        [DefaultValue(1)]
        public InArgument<Int32> WaitTime { get; set; }

        [Category("Input")]
        [Description("Switch to Main")]
        [DisplayName("Main")]
        public bool Parent { get; set; }

        [Category("Input")]
        [Description("Switch to New window or New tab")]
        [DisplayName("Window")]
        public bool Window { get; set; }

        [Category("Input")]
        [Description("Switch to New Iframe")]
        [DisplayName("Iframe")]
        public bool Iframe { get; set; }
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
            oLib.Iframeid = context.GetValue(this.Iframeid);
            oLib.Parent = this.Parent;
            oLib.Iframe = this.Iframe;
            oLib.Window = this.Window;
            oLib.WaitingTime = Convert.ToInt32(context.GetValue(this.WaitTime) == 0 ? 30 : context.GetValue(this.WaitTime));
            bool res = oLib.DoAction();
            if (res)
            {
                this.Result.Set(context, res.ToString());
            }
            else
            {
                this.Result.Set(context, res.ToString());
            }

        }
    }
}
