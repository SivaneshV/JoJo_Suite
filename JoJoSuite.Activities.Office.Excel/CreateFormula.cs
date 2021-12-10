using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using JoJoSuite.Library.Office.Excel;

namespace JoJoSuite.Actions.Office.Excel
{

    public sealed class CreateFormula : NativeActivity<object>
    {
        public CreateFormula()
        {
            this.DisplayName = "Insert Formula";
        }
        [RequiredArgument, Category("Input")]
        [Description("Please provide Worksheet")]
        [DefaultValue(null)]
        [DisplayName("WorkSheet")]
        public InArgument<Worksheet> xlWorksheet { get; set; }

        [Category("Input")]
        [Description("Please provide Formula")]
        [DefaultValue(null)]
        [DisplayName("Formula")]
        public InArgument<string> xlFormula { get; set; }

        [Category("Input")]
        [Description("Please provide CellAddress")]
        [DefaultValue(null)]
        [DisplayName("CellAddress")]
        public InArgument<string> CellAddress { get; set; }



        protected override void Execute(NativeActivityContext context)
        {
            r2rXlFormula oLib = new r2rXlFormula();
            oLib.xlWorksheet = context.GetValue(this.xlWorksheet);
            oLib.xlFormula = context.GetValue(this.xlFormula);
            oLib.CellAddress = context.GetValue(this.CellAddress);
          
            bool res = oLib.DoAction();

            if (res)
            {
                Result.Set(context, oLib.Error.ToString());
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
