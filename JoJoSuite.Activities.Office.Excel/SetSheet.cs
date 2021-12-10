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
    public sealed class SetSheet : NativeActivity<object>
    {
        public SetSheet()
        {
            this.DisplayName = "Select Sheet";
        }
        [Category("Input")]
        [Description("Please provide WorkBook")]
        [DisplayName("WorkBook")]
        public InArgument<Workbook> xlWorkBook { get; set; }

        [Category("Input")]
        [Description("Please provide SheetName")]
        [DefaultValue(null)]
        [DisplayName("SheetName")]
        public InArgument<string> SheetName { get; set; }
      
        [Category("Output")]
        [DisplayName("WorkSheet")]
        public  OutArgument<Worksheet> xlWorksheet { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rSetSheet oLib = new r2rSetSheet();
            oLib.xlWorkBook = context.GetValue(this.xlWorkBook);
            oLib.SheetName = context.GetValue(this.SheetName);


            bool res = oLib.DoAction();

            if (res)
            {
                this.xlWorksheet.Set(context, oLib.xlWorksheet);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
