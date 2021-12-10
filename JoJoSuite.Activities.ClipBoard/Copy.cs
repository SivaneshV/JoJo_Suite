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
    public sealed class Copy : NativeActivity<string>
    {
        public Copy()
        {
            this.DisplayName = "Copy";
           
        }

        R2rCopy oLib = new R2rCopy();

        [Category("Input")]
        [Description("Please provide FilePath")]
        [DefaultValue(null)]
        public InArgument<string> FilePath { get; set; }

        [Category("Text File")]
        [Description("Please tick for Text file")]
        [DefaultValue(false)]
        public bool TextFile { get; set; }

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
            oLib.ExcelFile = this.ExcelFile;
            oLib.ExcelSheet = context.GetValue(this.ExcelSheet);
            oLib.CellAddress = context.GetValue(this.CellAddress);
            
            bool res = oLib.DoAction();

            if (res)
            {
                this.Result.Set(context, res.ToString());
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }


        }       
    }

}
