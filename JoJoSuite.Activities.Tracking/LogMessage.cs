using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Tracking;
namespace JoJoSuite.Activities.Tracking
{

    public sealed class LogMessage : NativeActivity<object>
    {
        public LogMessage()
        {
            this.DisplayName = "Log Message";
        }
        r2rLogMessage oLib = new r2rLogMessage();

        [RequiredArgument, Category("Input")]
        [Description("Please provide BotId")]
        [DefaultValue(null)]
        public InArgument<int> BotId { get; set; }

        [RequiredArgument, Category("Input")]
        [Description("Please provide RunID")]
        [DefaultValue(null)]
        public InArgument<int> RunID { get; set; }

        [Category("Input")]
        [Description("Please provide LogMessage")]
        [DefaultValue(null)]
        [DisplayName("Log Message")]
        public InArgument<string> Log { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            oLib.RunID = context.GetValue(this.RunID);
            oLib.BotId = context.GetValue(this.BotId);
            oLib.LogMessage = context.GetValue(this.Log);

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
