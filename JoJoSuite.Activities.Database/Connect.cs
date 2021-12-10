using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Activities;
using System.ComponentModel;
using System.Data.SqlClient;

using JoJoSuite.Library.Database;

namespace JoJoSuite.Activities.Database
{
    public sealed class Connect : NativeActivity<string>
    {
        public Connect()
        {
            this.DisplayName = "Connect To DB Server";
        }

        [Category("Input")]
        [Description("Please provide server name or IP")]
        [DefaultValue(null)]
        public InArgument<string> Server { get; set; }

        [Category("Input")]
        [Description("Please provide database details")]
        [DefaultValue(null)]
        public InArgument<string> Database { get; set; }

        [Category("Input")]
        [Description("Please provide User details")]
        [DefaultValue(null)]
        public InArgument<string> User { get; set; }

        [Category("Input")]
        [Description("Please provide Password details")]
        [DefaultValue(null)]
        public InArgument<string> Password { get; set; }

        [Category("Input")]
        [Description("")]
        [DefaultValue(null)]
        public Activity Body { get; set; }

        [Category("Output")]
        [Description("Connection output")]
        public OutArgument<SqlConnection> Connection { get; set; }

        protected override void Execute(NativeActivityContext context)
        {

            r2rConnectToDatabase oLib = new r2rConnectToDatabase();

            oLib.Server = context.GetValue(this.Server);
            oLib.Database = context.GetValue(this.Database);
            oLib.User = context.GetValue(this.User);
            oLib.Password = context.GetValue(this.Password);

            bool res = oLib.DoAction();

            if (res)
            {
                this.Connection.Set(context, oLib.sqlConnection);
            }
            else
            {
                this.Result.Set(context, oLib.ErrorMessage);
            }
            if (this.Body!=null)
            {
                context.ScheduleActivity(this.Body);
            }

        }
    }
}
