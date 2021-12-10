using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using JoJoSuite.Library.Web;
using System.ComponentModel;

namespace JoJoSuite.Activities.Web
{

    public sealed class FileDownload : NativeActivity<object>
    {
        public FileDownload()
        {
            this.DisplayName = "Download File";
        }

        r2rFileDownload oLib = new r2rFileDownload();

        [RequiredArgument, Category("Input")]
        [Description("Please provide URL")]
        [DefaultValue(null)]
        public InArgument<string> URL { get; set; }

        [RequiredArgument, Category("Input")]
        [Description("Please provide Username")]
        [DefaultValue(null)]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [Description("Please provide Password")]
        [DefaultValue(null)]
        public InArgument<string> Password { get; set; }

        [Category("Input")]
        [Description("Please provide DownloadPath")]
        [DefaultValue(null)]
        public InArgument<string> DownloadPath { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            oLib.URL = context.GetValue(this.URL);
            oLib.Username = context.GetValue(this.Username);
            oLib.Password = context.GetValue(this.Password);
            oLib.DownloadPath = context.GetValue(this.DownloadPath);

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
