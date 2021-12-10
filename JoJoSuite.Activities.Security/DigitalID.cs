using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Security;

namespace JoJoSuite.Activities.Security
{

    public sealed class DigitalID : NativeActivity<object>
    {

        public DigitalID()
        {
            this.Env = "prod";
        }
         r2rDigitalID oLib = new r2rDigitalID();
        [Category("Input")]
        [Description("Please provide AccountNt id")]
        [DefaultValue("")]
        [DisplayName("Account NtId")]
        public InArgument<string> AccountNtid { get; set; }

        [Category("Input")]
        [Description("Please provide AccountEmailid")]
        [DefaultValue("")]
        [DisplayName("Account EmailId")]
        public InArgument<string> AccountEmailid { get; set; }

        [Category("Input")]
        [Description("Please provide Env")]
        [DefaultValue("")]
        public InArgument<string> Env { get; set; }

        [Category("OutPut")]       
        [DefaultValue("")]
        public OutArgument<bool> Error { get; set; }

        [Category("OutPut")]
        [DefaultValue("")]
        public OutArgument<string> Errormsg { get; set; }

        [Category("OutPut")]
        [DefaultValue("")]
        [DisplayName("Password")]
        public OutArgument<string> Pass { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            oLib.AccountNtid = context.GetValue(this.AccountNtid);
            oLib.AccountEmailid = context.GetValue(this.AccountEmailid);
            oLib.Env = context.GetValue(this.Env);
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
