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

    public sealed class DialogAction : NativeActivity<object>
    {
        r2rDialogAction oLib = new r2rDialogAction();

        [RequiredArgument, Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }

        [Category("Input")]
        [Description("Click Ok")]
        [DisplayName("Ok")]
        public bool ActionOk { get; set; }

        [Category("Input")]
        [Description("Click Cancel")]
        [DisplayName("Cancel")]
        public bool ActionCancel { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);
            oLib.ActionOk = this.ActionOk;
            oLib.ActionCancel = this.ActionCancel;
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
