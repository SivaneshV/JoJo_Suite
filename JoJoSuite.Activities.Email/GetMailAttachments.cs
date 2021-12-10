using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Microsoft.Exchange.WebServices.Data;
using JoJoSuite.Library.Email;
using MailKit.Net.Imap;

namespace JoJoSuite.Activities.Email
{
    public sealed class GetMailAttachments : NativeActivity<object>
    {
        public GetMailAttachments()
        {
            this.DisplayName = "Open Email Attachments";
        }

        [Category("Input")]
        [Description("Select Read Mail Type")]
        public ReadMailType MailType { get; set; }

        [Category("Input")]
        [Description("Please provide exchangeService")]
        [DefaultValue(null)]
        public InArgument<ExchangeService> exchangeService { get; set; }

        [Category("Input")]
        [Description("Please provide IMAPFolder")]
        [DefaultValue(null)]
        public InArgument<ImapClient> IMAPClient { get; set; }

        [Category("Input")]
        [Description("Please provide MailId")]
        [DefaultValue(null)]
        public InArgument<object> MailId { get; set; }

        [Category("Input")]
        [Description("Please provide download FolderPath")]
        [DefaultValue(null)]
        public InArgument<string> FolderPath { get; set; }

        [Category("Input")]
        [Description("Please provide Mailbox Foldername")]
        [DefaultValue(null)]
        public InArgument<string> FolderName { get; set; }

        [Category("Output")]
        [Description("List of filename with path")]
        public OutArgument<string[]> FileList { get; set; }


        protected override void Execute(NativeActivityContext context)
        {

            r2rGetMailAttachments oLib = new r2rGetMailAttachments();
            oLib.EwsConn = context.GetValue(this.exchangeService);
            oLib.IMAPClient = context.GetValue(this.IMAPClient);
            oLib.MailType = Convert.ToString(this.MailType);
            oLib.EmailId = context.GetValue(this.MailId);
            oLib.FolderPath = context.GetValue(this.FolderPath);
            oLib.FolderName = context.GetValue(this.FolderName);

            bool res = oLib.DoAction();
            if (res)
            {
                this.FileList.Set(context, oLib.FileList);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }

        }
    }


    public enum ReadMailType
    {
        ExchangeService,
        IMAPClient
    }
}
