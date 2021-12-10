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

    public sealed class CreateTable : NativeActivity<object>
    {
        [Category("Input")]
        [Description("Please provide Worksheet")]
        [DefaultValue(null)]
        [DisplayName("WorkSheet")]
        public InArgument<Worksheet> xlWorksheet { get; set; }

        [Category("Input")]
        [Description("Please provide CellAddress(ex: \"A1\",\"D25\"")]
        [DefaultValue(null)]
        [DisplayName("CellAddress")]
        public InArgument<string> CellAddress { get; set; }

        [Category("Input")]
        [Description("Please provide TableName")]
        [DefaultValue(null)]
        [DisplayName("TableName")]
        public InArgument<string> xlTableName { get; set; }

     

        protected override void Execute(NativeActivityContext context)
        {
            r2rCreateTable oLib = new r2rCreateTable();
            oLib.xlWorksheet = context.GetValue(this.xlWorksheet);
            oLib.xlTableName = context.GetValue(this.xlTableName);
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
