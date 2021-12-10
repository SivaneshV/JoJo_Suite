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
    public sealed class LastRowIndex : NativeActivity<object>
    {
        [Category("Input")]
        [Description("Please provide WorkSheet")]
        [DisplayName("WorkSheet")]
        public InArgument<Worksheet> xlWorkSheet { get; set; }

        [Category("Input")]
        [Description("Has Column Filter Condition")]
        [DisplayName("IsColumnFilter")]
        public bool IsColumnFilter { get; set; }

        [Category("Input")]
        [Description("Please provide Column Index")]
        [DefaultValue(null)]
        [DisplayName("ColumnIndex")]
        public InArgument<int> ColumnIndex { get; set; }

        [Category("Input")]
        [Description("Please provide Column Value")]
        [DefaultValue(null)]
        [DisplayName("ColumnValue")]
        public InArgument<string> ColumnValue { get; set; }

        [Category("Output")]
        [DefaultValue(null)]
        [DisplayName("LastRowIndex")]
        public OutArgument<int> RowIndex { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rGetLastRowIndex oLib = new r2rGetLastRowIndex();
            oLib.xlWorkSheet = context.GetValue(this.xlWorkSheet);
            oLib.IsColumnValueSearch = this.IsColumnFilter;
            oLib.ColumnIndex = context.GetValue(this.ColumnIndex);
            oLib.ColumnValue = context.GetValue(this.ColumnValue);
            bool res = oLib.DoAction();

            if (res)
            {
                RowIndex.Set(context, oLib.RowIndex);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
