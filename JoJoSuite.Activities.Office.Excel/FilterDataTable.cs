using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Office.Excel;
using Microsoft.Office.Interop.Excel;
using System.Data;
using static JoJoSuite.Library.Office.Excel.r2rFilterDataTable;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class FilterDataTable : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide Input Record Set")]
        [DefaultValue("")]
        [DisplayName("Input Record Set")]
        public InArgument<System.Data.DataTable> InputRecordSet { get; set; }

        [Category("Input")]
        [Description("First row as column")]
        [DisplayName("UseHeaderRow")]
        public bool UserHeaderRow { get; set; }

        [Category("Input")]
        [Description("Filter Column")]
        [DisplayName("Filter Column Name")]
        public InArgument<string> FilterColumn { get; set; }

        [Category("Input")]
        [Description("Filter Column Type")]
        [DisplayName("Filter Column Type")]
        public FilterColumnType ColumnType { get; set; }

        [Category("Input")]
        [Description("Filter Value")]
        [DisplayName("Filter Value")]
        public InArgument<string> FilterValue { get; set; }

        [Category("Input")]
        [Description("Condition Type")]
        [DisplayName("Condition Type")]
        public FilterCondtion Condition { get; set; }

        [Category("Output")]
        [DisplayName("ResultRecordSet")]
        public OutArgument<System.Data.DataTable> ResultRecordSet { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            r2rFilterDataTable oLib = new r2rFilterDataTable();
            oLib.DataTableInput = context.GetValue(this.InputRecordSet);
            oLib.UseHeaderRow = this.UserHeaderRow;
            oLib.FilterColumn = context.GetValue(this.FilterColumn);
            oLib.FilterValue = context.GetValue(this.FilterValue);
            oLib.ColumnType = this.ColumnType;
            oLib.Condition = this.Condition;
            bool res = oLib.DoAction().Result;

            if (res)
            {
                this.Result.Set(context, oLib.Error.ToString());
                this.ResultRecordSet.Set(context, oLib.ResultRecordSet);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage).ToString());
            }
        }
    }
}
