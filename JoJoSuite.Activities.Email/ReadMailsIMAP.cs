using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Email;
using Microsoft.Exchange.WebServices.Data;
using MailKit.Net.Imap;

namespace JoJoSuite.Activities.Email
{
    public sealed class ReadMailsIMAP : NativeActivity<object>
    {
        public ReadMailsIMAP()
        {
            this.DisplayName = "Read Mails using IMAP";
        }

        [Category("Input")]
        [Description("Please provide IMAPClient")]
        [DefaultValue(null)]
        public InArgument<ImapClient> IMAPClient { get; set; }

        [Category("Input")]
        [Description("Please provide Folder Name in Email")]
        [DefaultValue(null)]
        public InArgument<string> Folder { get; set; }

        [Category("Input")]
        [Description("Please provide Subject Filter")]
        [DefaultValue(null)]
        public InArgument<string> SubjectFilter { get; set; }

        [Category("Input")]
        [Description("Please provide From address Filter")]
        [DefaultValue(null)]
        public InArgument<string> FromAddressFilter { get; set; }

        [Category("Input")]
        [Description("To check only unread Mails")]
        public r2rEmailNewOnly NewOnly { get; set; }

        [Category("Input")]
        [Description("To check Mail mark as read")]
        public r2rEmailNewOnly MarkAsRead { get; set; }
                      
        public OutArgument<MailItems> MailList { get; set; }
        //public OutArgument<MailKit.IMailFolder> IMAPFolder { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            r2rReadMailsIMAP oLib = new r2rReadMailsIMAP();
            oLib.Folder = context.GetValue(this.Folder);
            oLib.SubjectFilter = context.GetValue(this.SubjectFilter);
            oLib.FromAddressFilter = context.GetValue(this.FromAddressFilter);
            oLib.IMAPClient = context.GetValue(this.IMAPClient);
            oLib.OnlyNew = (this.NewOnly == r2rEmailNewOnly.True);
            oLib.MarkAsRead = (this.MarkAsRead == r2rEmailNewOnly.True);
            
            bool res = oLib.DoAction();
            if (res)
            {
                this.MailList.Set(context, oLib.MAILS);
                //this.IMAPFolder.Set(context, oLib._IMAPFolder);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }

        }
    }
}
