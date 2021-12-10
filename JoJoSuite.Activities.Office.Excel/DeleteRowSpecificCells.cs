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
    public sealed class DeleteRowSpecificCells : NativeActivity<object>
    {
        [Category("Input")]
        [Description("Please provide WorkSheet")]
        [DisplayName("WorkSheet")]
        public InArgument<Worksheet> xlWorkSheet { get; set; }

        [Category("Input")]
        [Description("Please provide ColumnNamesList")]
        [DisplayName("ColumnList")]
        public InArgument<string> ColumnList { get; set; }

        [Category("Input")]
        [Description("Please provide Row Index")]
        [DisplayName("Row Index")]
        public InArgument<int> RowIndex { get; set; }

        [Category("Output")]
        [DefaultValue(null)]
        [DisplayName("IsSucess")]
        public OutArgument<bool> IsSuccess { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rDeleteRowSpecificCells oLib = new r2rDeleteRowSpecificCells();
            oLib.xlWorkSheet = context.GetValue(this.xlWorkSheet);
            oLib.ColumnNamesList = context.GetValue(this.ColumnList);
            oLib.RowIndex = context.GetValue(this.RowIndex);

            bool res = oLib.DoAction();

            if (res)
            {
                IsSuccess.Set(context, oLib.IsSucces);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
