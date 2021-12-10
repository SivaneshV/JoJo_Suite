using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Email;
using Microsoft.Exchange.WebServices.Data;

namespace JoJoSuite.Activities.Email
{
    public sealed class ReadMails : NativeActivity<object>
    {
        public ReadMails()
        {
            this.DisplayName = "Read Mails";
        }

        [Category("Input")]
        [Description("Please provide exchangeService")]
        [DefaultValue(null)]
        public InArgument<ExchangeService> exchangeService { get; set; }

        [Category("Input")]
        [Description("Please provide Folder Name in Email")]
        [DefaultValue(null)]
        public InArgument<string> Folder { get; set; }

        [Category("Input")]
        [Description("Please provide Subject Filter")]
        [DefaultValue(null)]
        public InArgument<string> SubjectFilter { get; set; }

        [Category("Input")]
        [Description("To check only unread Mails")]
        public r2rEmailNewOnly NewOnly { get; set; }

        [Category("Input")]
        [Description("Please provide MailId or GMB details")]
        [DefaultValue(null)]
        public InArgument<string> MailBox { get; set; }
              
        public OutArgument<List<JoJoSuite.Library.Email.r2rMailItem>> MailList { get; set; }
        

        protected override void Execute(NativeActivityContext context)
        {
            r2rReadMails oLib = new r2rReadMails();
            oLib.Folder = context.GetValue(this.Folder);
            oLib.SubjectFilter = context.GetValue(this.SubjectFilter);
            oLib.OnlyNew = (this.NewOnly == r2rEmailNewOnly.True);
            oLib.MBOX = context.GetValue(this.MailBox);
            
            oLib.EWSCONN = context.GetValue(this.exchangeService);
            bool res = oLib.DoAction();
            if (res)
            {
                this.MailList.Set(context, oLib.MAILS);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }

        }
    }

    public enum r2rEmailNewOnly
    {
        True,
        False
    }
}
