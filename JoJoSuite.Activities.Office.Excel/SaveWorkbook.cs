using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Office.Excel;
using Microsoft.Office.Interop.Excel;

namespace JoJoSuite.Actions.Office.Excel
{

    public sealed class SaveWorkbook : NativeActivity<object>
    {
        public SaveWorkbook()
        {
            this.DisplayName = "Save Excel File";
        }
        [RequiredArgument, Category("Input")]
        [Description("Please provide xlWorkBook")]
        [DisplayName("WorkBook")]
        public InArgument<Workbook> xlWorkBook { get; set; }

        [Category("Input")]
        [Description("Please provide File")]
        [DisplayName("File Path")]
        public InArgument<string> FilePath { get; set; }

        [ Category("Input")]
        [Description("Please provide xlWorkBook")]
        [DisplayName("SaveAs")]
        public bool xlSaveAs { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            r2rSaveWorkbook oLib = new r2rSaveWorkbook();
            oLib.xlWorkBook = context.GetValue(this.xlWorkBook);
            oLib.SaveAs = this.xlSaveAs;
            oLib.File = context.GetValue(this.FilePath);
            bool res = oLib.DoAction();

            if (res)
            {
                this.Result.Set(context, oLib.Error.ToString());
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
