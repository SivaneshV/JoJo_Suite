using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Tracking;

namespace JoJoSuite.Activities.Tracking
{

    public sealed class CountTracker : NativeActivity<object>
    {
        public CountTracker()
        {
            this.DisplayName = "Log Transactions";
        }

        r2rCountTracker oLib = new r2rCountTracker();

        [RequiredArgument, Category("Input")]
        [Description("Please provide BotId")]
        [DefaultValue(null)]
        public InArgument<int> BotId { get; set; }

        [RequiredArgument, Category("Input")]
        [Description("Please provide RunID")]
        [DefaultValue(null)]
        public InArgument<int> RunID { get; set; }

        [Category("Input")]
        [Description("Please provide Transcation Count")]
        [DefaultValue(null)]
        public InArgument<int> TranscationCount { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            oLib.RunID = context.GetValue(this.RunID);
            oLib.BotId = context.GetValue(this.BotId);
            oLib.TranscationCount = context.GetValue(this.TranscationCount);
           
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
