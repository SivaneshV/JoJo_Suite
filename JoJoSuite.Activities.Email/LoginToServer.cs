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
    public sealed class LoginToServer : NativeActivity<object>
    {
        public LoginToServer()
        {
            this.DisplayName = "Connect to Email Server";
        }

        [Category("Input")]
        [Description("Select Read Mail Type")]
        public ReadMailType MailType { get; set; }

        [Category("Input")]
        [Description("Please provide Server details")]
        [DefaultValue(null)]
        public InArgument<string> Server { get; set; }

        [Category("Input")]
        [Description("Please provide Domain(auth details) / IMAPHost")]
        [DefaultValue(null)]
        public InArgument<string> Domain { get; set; }

        [Category("Input")]
        [Description("Please provide IMAP Port")]
        [DefaultValue(null)]
        public InArgument<Int32> EmailIMAPPort { get; set; }

        [Category("Input")]
        [Description("Please provide Username")]
        [DefaultValue(null)]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [Description("Please provide Password")]
        [DefaultValue(null)]
        public InArgument<string> Password { get; set; }

        [Category("Output")]       
        [DefaultValue(null)]
        public OutArgument<ExchangeService> exchangeService { get; set; }

        [Category("Output")]
        [DefaultValue(null)]
        public OutArgument<ImapClient> IMAPClient { get; set; }

        [DefaultValue(null)]
        public Activity Body { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rLoginToServer oLib = new r2rLoginToServer();
            oLib.Server = context.GetValue(this.Server);
            oLib.Domain = context.GetValue(this.Domain);
            oLib.user = context.GetValue(this.Username);
            oLib.Password = context.GetValue(this.Password);
            oLib.EmailIMAPPort = context.GetValue(this.EmailIMAPPort);
            oLib.MailType = this.MailType.ToString();

            bool res = oLib.DoAction();
            if (res)
            {
                if (this.MailType.ToString().ToLower() == "exchangeservice")
                {
                    this.exchangeService.Set(context, oLib.ewsConnection);
                }
                else
                {
                    this.IMAPClient.Set(context, oLib.IMAPClient);
                }
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }

           

        }
    }
}
