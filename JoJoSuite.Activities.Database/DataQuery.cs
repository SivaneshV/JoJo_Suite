using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Data;
using JoJoSuite.Library.Database;
using System.Data.SqlClient;

namespace JoJoSuite.Activities.Database
{
    public sealed class DataQuery : NativeActivity<string>
    {
        public DataQuery()
        {
            this.DisplayName = "Select Query";
        }
        [Category("Input")]
        [Description("Please provide Db Connection")]
        [DefaultValue(null)]
        public InArgument<SqlConnection> Connection { get; set; }
        [Category("Input")]
        [Description("Please provide Query")]
        [DefaultValue(null)]
        public InArgument<string> Query { get; set; }

        [Category("Input")]
        [Description("Please provide Parameters in array")]
        [DefaultValue(null)]
        public InArgument<string[]> Parameters { get; set; }

        [Category("Input")]
        [Description("Select query type")]
        public r2rDatabaseQueryType QueryType { get; set; }

        [Category("Input")]
        [Description("Please provide Values in array")]
        [DefaultValue(null)]
        public InArgument<string[]> Values { get; set; }

        [Category("Output")]
        [Description("Dataset result")]
        public OutArgument<DataTable> DataResult { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rDataQuery oLib = new r2rDataQuery();
            oLib.SqlConn = context.GetValue(this.Connection);
            oLib.Query = context.GetValue(this.Query);
            oLib.Parameters = context.GetValue(this.Parameters);
            oLib.ValuesList = context.GetValue(this.Values);
            oLib.QueryType = this.QueryType.ToString();

            bool res = oLib.DoAction();

            if (res)
            {
                this.DataResult.Set(context, oLib.Resultdatatable);
            }
            else
            {
                this.Result.Set(context, oLib.ErrorMessage);
            }
        }
    }
    public enum r2rDatabaseQueryType
    {
        StoreProcedure,
        Text
    }
}
