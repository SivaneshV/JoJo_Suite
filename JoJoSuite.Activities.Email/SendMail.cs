using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Email;

namespace JoJoSuite.Activities.Email
{
    public sealed class SendMail : NativeActivity<object>    
    {
        public SendMail()
        {
            this.DisplayName = "Send Email";
        }
        [Category("Input")]
        [Description("Please provide Username details")]
        [DefaultValue(null)]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [Description("Please provide Password details")]
        [DefaultValue(null)]
        public InArgument<string> Password { get; set; }

        [Category("Input")]
        [Description("Please provide From details")]
        [DefaultValue(null)]
        public InArgument<string> From { get; set; }

        [Category("Input")]
        [Description("Please provide To details")]
        [DefaultValue(null)]
        public InArgument<string> To { get; set; }

        [Category("Input")]
        [Description("Please provide CC details")]
        [DefaultValue(null)]
        public InArgument<string> CC { get; set; }

        [Category("Input")]
        [Description("Please provide Subject details")]
        [DefaultValue(null)]
        public InArgument<string> Subject { get; set; }

        [Category("Input")]
        [Description("Please provide SMTPHost")]
        [DefaultValue(null)]
        public InArgument<string> SMTPHost { get; set; }

        [Category("Input")]
        [Description("Please provide Body details")]
        [DefaultValue(null)]
        public InArgument<string> Body { get; set; }

        [Category("Input")]
        [Description("Please provide File Path")]
        [DefaultValue(null)]
        public InArgument<string> FilePath { get; set; }

        [Category("Input")]
        [Description("To check HTML body")]
        public bool isHtml { get; set; }


        protected override void Execute(NativeActivityContext context)
        {

            r2rSendMail oLib = new r2rSendMail();
            oLib.Username = context.GetValue(this.Username);
            oLib.Password = context.GetValue(this.Password);
            oLib.From = context.GetValue(this.From);
            oLib.To = context.GetValue(this.To);
            oLib.Cc = context.GetValue(this.CC);
            oLib.Subject = context.GetValue(this.Subject);
            oLib.SMTPHost = context.GetValue(this.SMTPHost);
            oLib.Body = context.GetValue(this.Body);
            oLib.FilePath = context.GetValue(this.FilePath);
            oLib.IsBodyHtml = this.isHtml;

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
