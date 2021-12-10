using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Microsoft.Exchange.WebServices.Data;
using JoJoSuite.Library.Email;

namespace JoJoSuite.Activities.Email
{
    public sealed class MailMoveToFolder : NativeActivity<object>
    {
        public MailMoveToFolder()
        {
            this.DisplayName = "Mail move to another folder";
        }
        [Category("Input")]
        [Description("Please provide exchangeService")]
        [DefaultValue(null)]
        public InArgument<ExchangeService> exchangeService { get; set; }
        [Category("Input")]
        [Description("Please provide MailId")]
        [DefaultValue(null)]
        public InArgument<string> MailId { get; set; }

        [Category("Input")]
        [Description("Please provide download FolderPath")]
        [DefaultValue(null)]
        public InArgument<string> Tofolder { get; set; }

   


        protected override void Execute(NativeActivityContext context)
        {

            r2rMailMoveToFolder oLib = new r2rMailMoveToFolder();
            oLib.EwsConn = context.GetValue(this.exchangeService);
            oLib.EmailId = context.GetValue(this.MailId);
            oLib.ToFolder = context.GetValue(this.Tofolder);

            bool res = oLib.DoAction();
            if (res)
            {
                this.Result.Set(context, res);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }

        }
    }
}
