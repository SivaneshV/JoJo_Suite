using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.ClipBoard;
using OpenQA.Selenium;

namespace JoJoSuite.Activities.ClipBoard
{
    public sealed class Paste : NativeActivity<string>
    {
        public Paste()
        {
            this.DisplayName = "Paste";          
        }

        R2rPaste oLib = new R2rPaste();


        [Category("Output")]
        [Description("Please provide FilePath")]
        [DefaultValue(null)]
        public InArgument<string> FilePath { get; set; }

        [Category("Text File")]
        [Description("Please tick for Text file")]
        [DefaultValue(false)]
        public bool TextFile { get; set; }

        //Web 
        [Category("Web")]
        [Description("Please tick for Web")]
        [DefaultValue(false)]
        public bool WebPage { get; set; }

        [Category("Web")]
        [Description("Webdriver")]
        [DefaultValue(null)]
        public InArgument<IWebDriver> WebDriver { get; set; }

        [Category("Web")]
        [DefaultValue(null)]
        public InArgument<string> XPath { get; set; }

        [Category("Web")]
        [Description("Please provide WaitTime")]
        [DefaultValue(1)]
        public InArgument<Int32> WaitTime { get; set; }

        //Excel
        [Category("Excel")]
        [Description("Please tick for Excel")]
        [DefaultValue(false)]
        public bool ExcelFile { get; set; }
        [Category("Excel")]
        [Description("Please provide SheetName")]
        public InArgument<string> ExcelSheet { get; set; }
        [Category("Excel")]
        [Description("Please provide CellAddress with range")]
        public InArgument<string> CellAddress { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            oLib.FilePath = context.GetValue(this.FilePath);
            oLib.TxtFile = this.TextFile;

            oLib.Web = this.WebPage;
            oLib.WebDriver = context.GetValue(this.WebDriver);
            oLib.Xpath = context.GetValue(this.XPath);
            oLib.WaitTime = Convert.ToInt32(context.GetValue(this.WaitTime) == 0 ? 5 : context.GetValue(this.WaitTime));

            oLib.ExcelFile = this.ExcelFile;
            oLib.ExcelSheet = context.GetValue(this.ExcelSheet);
            oLib.CellAddress = context.GetValue(this.CellAddress);

            bool res = oLib.DoAction();
            if (res)
            {
                //GetValue.Set(context, oLib.OutputStr.Trim());
            }
            else
            {
                this.Result.Set(context, oLib.ErrorMessage.ToString());
            }

        }
    }

}
