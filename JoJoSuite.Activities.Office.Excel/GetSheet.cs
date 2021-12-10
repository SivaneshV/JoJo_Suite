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
    public sealed class GetSheet : NativeActivity<object>
    {
        [Category("Input")]
        [Description("Please provide WorkBook")]
        [DisplayName("WorkBook")]
        public InArgument<Workbook> xlWorkBook { get; set; }

        [Category("Input")]
        [Description("Please provide SheetName")]
        [DefaultValue(null)]
        [DisplayName("Sheet Name")]
        public InArgument<string> SheetName { get; set; }

        [Category("Output")]
        [DefaultValue(null)]
        [DisplayName("WorkSheet")]
        public OutArgument<Worksheet> xlWorksheet { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rGetSheet oLib = new r2rGetSheet();
            oLib.xlWorkBook = context.GetValue(this.xlWorkBook);
            oLib.SheetName = context.GetValue(this.SheetName);


            bool res = oLib.DoAction();

            if (res)
            {
                xlWorksheet.Set(context, oLib.xlWorksheet);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
