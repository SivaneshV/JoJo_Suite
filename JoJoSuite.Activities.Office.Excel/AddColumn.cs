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

    public sealed class AddColumn : NativeActivity<object>
    {
        [RequiredArgument, Category("Input")]
        [Description("Please provide Worksheet")]
        [DefaultValue(null)]
        [DisplayName("WorkSheet")]
        public InArgument<Worksheet> xlWorksheet { get; set; }

        [Category("Input")]
        [Description("Please provide TableName")]
        [DefaultValue(null)]
        [DisplayName("TableName")]
        public InArgument<string> xlTableName { get; set; }

        [Category("Input")]
        [Description("Please provide RowNo")]
        [DefaultValue(null)]
        [DisplayName("ColumnNo Or Name")]
        public InArgument<string> xlColumnNoOrName { get; set; }

        [Category("Input")]
        [Description("Please provide Count")]
        [DefaultValue(1)]
        [DisplayName("Count")]
        public InArgument<Int32> xlCount { get; set; }

     
        protected override void Execute(NativeActivityContext context)
        {
            r2rAddColumn oLib = new r2rAddColumn();
            oLib.xlWorksheet = context.GetValue(this.xlWorksheet);
            oLib.xlTableName = context.GetValue(this.xlTableName);
            oLib.xlColumnNoOrName = context.GetValue(this.xlColumnNoOrName);
            oLib.xlCount = context.GetValue(this.xlCount);
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
