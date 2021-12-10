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

    public sealed class DialogWriteText : NativeActivity<object>
    {
        r2rDialogWriteText oLib = new r2rDialogWriteText();

        [RequiredArgument, Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }

        [RequiredArgument, Category("Input")]
        [Description("Write text")]
        public InArgument<string> WriteText { get; set; }

        [Category("Input")]
        [Description("Waiting time")]
        public InArgument<Int32> WaitingTime { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);
            oLib.WriteText = context.GetValue(this.WriteText);
            oLib.WaitingTime = context.GetValue(this.WaitingTime);
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
