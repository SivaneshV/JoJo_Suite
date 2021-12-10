using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Database;
using System.Data.SqlClient;

namespace JoJoSuite.Activities.Database
{
    public sealed class NonDataQuery : NativeActivity<string>
    {
        public NonDataQuery()
        {
            this.DisplayName = "Action Query";
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

        protected override void Execute(NativeActivityContext context)
        {
            r2rNonDataQuery oLib = new r2rNonDataQuery();
            oLib.SqlConn = context.GetValue(this.Connection);
            oLib.Query = context.GetValue(this.Query);
            oLib.Parameters = context.GetValue(this.Parameters);
            oLib.ValuesList = context.GetValue(this.Values);
            oLib.QueryType = this.QueryType.ToString();

            bool res = oLib.DoAction();

            if (res)
            {
                this.Result.Set(context,Convert.ToString(oLib.Result));
            }
            else
            {
                this.Result.Set(context, oLib.ErrorMessage);
            }

        }
    }

   
}
