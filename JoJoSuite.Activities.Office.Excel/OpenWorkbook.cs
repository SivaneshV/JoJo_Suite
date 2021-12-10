using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Office.Excel;
using Microsoft.Office.Interop.Excel;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class OpenWorkbook: NativeActivity<object>
    {
        public OpenWorkbook()
        {
            this.DisplayName = "Open Excel File";
        }
        [Category("Input")]
        [Description("Please provide filename with path")]
        [DefaultValue(null)]
        [DisplayName("File Path")]
        public InArgument<string> FilePath { get; set; }

        [Category("Input")]
        [Description("Show excel")]
        [DisplayName("Show excel")]
        public bool xlvisible { get; set; }

        [Category("Output")]
        [DisplayName("WorkBook")]
        public OutArgument<Microsoft.Office.Interop.Excel.Workbook> xlWorkBook { get; set; }
       
        protected override void Execute(NativeActivityContext context)
        {
            r2rOpenWorkbook oLib = new r2rOpenWorkbook();
            oLib.File = context.GetValue(this.FilePath);
            oLib.xlVisible = this.xlvisible;

            bool res = oLib.DoAction();

            if (res)
            {
                xlWorkBook.Set(context, oLib.xlWorkBook);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
