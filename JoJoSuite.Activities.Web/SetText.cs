using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Web;
using OpenQA.Selenium;
using System.Windows;

namespace JoJoSuite.Activities.Web
{

    public sealed class SetText : NativeActivity<string>
    {
        public SetText()
        {
            this.DisplayName = "Write Value";
            this.ClearText = true; 

        }

        r2rSetText oLib = new r2rSetText();
       
        [DisplayName("Connection")]
        [Category("Input")]
        [Description("Connection")]
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
        [Description("Please provide Setvalue")]
        [DefaultValue(null)]
        public InArgument<string> Value { get; set; }

        [Category("Input")]
        [Description("Please provide WaitTime")]
        [DefaultValue(1)]
        public InArgument<Int64> WaitTime { get; set; }

        [Category("Input")]
        [Description("Clear the text before write the value")]
        [DisplayName("Clear")]
        [DefaultValue(false)]
        public bool ClearText { get; set; }


        [Category("Input")]
        [Description("send data using Javascript")]
        [DisplayName("ScriptExecutor")]
        [DefaultValue(false)]
        public bool ScriptExecutor { get; set; }



        [Category("Input")]
        [Description("Wait until to load")]
        [DisplayName("WaitToLoad")]
        [DefaultValue(false)]
        public bool WaitToLoad { get; set; }

        [Category("Input")]
        [Description("Send key")]
        [DisplayName("Send key")]
        public r2rWebSendKey SendKey { get; set; }


        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            if (WebDriver==null)
            {
                metadata.AddValidationError("Value for required activity argument 'Connection' was not supplied");
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            oLib.WebDriver = context.GetValue(this.WebDriver);
            oLib.Xpath = context.GetValue(this.XPath);
            oLib.WebElement = context.GetValue(this.WebElement);
            oLib.WaitingTime = Convert.ToInt32(context.GetValue(this.WaitTime) == 0 ? 30 : context.GetValue(this.WaitTime));
            oLib.Settext = context.GetValue(this.Value);
            oLib.ClearText = this.ClearText;
            oLib.ScriptExecutor = this.ScriptExecutor;
            oLib.SendKeys = this.SendKey.ToString();
            oLib.WaitToload = this.WaitToLoad;

            
            bool res = oLib.DoAction();
            if (res)
            {
                this.Result.Set(context, res.ToString());
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage).ToString());
            }


        }

        public enum r2rWebSendKey
        {
            Select,
            Enter          
        }
    }
}
