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
using static JoJoSuite.Library.Office.Excel.r2rAggregateField;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class AggregateField : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide Input Record Set")]
        [DefaultValue("")]
        [DisplayName("Input Record Set")]
        public InArgument<System.Data.DataTable> InputRecordSet { get; set; }

        [Category("Input")]
        [Description("Aggregate Column")]
        [DisplayName("Aggregate Column Name")]
        public InArgument<string> AggregateColumn { get; set; }

        [Category("Input")]
        [Description("Aggregate Column Type")]
        [DisplayName("Aggregate Column Name")]
        public AggregateColumnType ColumnType { get; set; }
       
        [Category("Input")]
        [Description("Aggregation Type")]
        [DisplayName("Aggregation Type")]
        public AggregateType Aggregation { get; set; }

        [Category("Output")]
        [DisplayName("AggregateResult")]
        public OutArgument<dynamic> AggregateResult { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            r2rAggregateField oLib = new r2rAggregateField();
            oLib.DataTableInput = context.GetValue(this.InputRecordSet);           
            oLib.AggregateColumn = context.GetValue(this.AggregateColumn);           
            oLib.ColumnType = this.ColumnType;
            oLib.Aggregation = this.Aggregation;
            bool res = oLib.DoAction().Result;

            if (res)
            {
                this.Result.Set(context, oLib.Error.ToString());
                this.AggregateResult.Set(context, oLib.AggregationResult);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage).ToString());
            }
        }
    }
}
