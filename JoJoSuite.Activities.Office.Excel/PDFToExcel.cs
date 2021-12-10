using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using JoJoSuite.Library.Office.Excel;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class PDFToExcel: NativeActivity<object>
    {
        public PDFToExcel()
        {
            this.DisplayName = "PDF To Excel Converter";
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

        [Category("Outputs")]
        [DisplayName("WorkBook")]
        public OutArgument<Workbook> xlWorkBook { get; set; }

        [Category("Outputs")]
        [DisplayName("WorkSheet")]
        public OutArgument<Worksheet> xlWorkSheet { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rPDFToExcel oLib = new r2rPDFToExcel();
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
