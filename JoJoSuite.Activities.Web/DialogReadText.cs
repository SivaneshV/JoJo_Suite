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

    public sealed class DialogReadText : NativeActivity<object>
    {
        r2rDialogReadText oLib = new r2rDialogReadText();

        [RequiredArgument, Category("Input")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }

        [RequiredArgument, Category("Output")]
        [Description("Read text")]        
        public OutArgument<string> Readtext { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);

            bool res = oLib.DoAction();
            if (res)
            {
                this.Readtext.Set(context, oLib.ReadText.Trim());               
            }
            else
            {
                this.Result.Set(context, res.ToString());
            }

        }
    }
}
