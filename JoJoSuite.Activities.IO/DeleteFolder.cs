using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.IO;

namespace JoJoSuite.Activities.IO
{
    public sealed class DeleteFolder : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide folder path")]
        [DefaultValue(null)]
        public InArgument<string> FolderPath { get; set; }

        [Category("Output")]
        [Description("Please provide input file with path")]
        [DefaultValue(null)]
        public OutArgument<bool> isSuccess { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rDeleteFolder oLib = new r2rDeleteFolder();
            oLib.FolderPath = context.GetValue(this.FolderPath);
            bool res = oLib.DoAction();
            if (res)
            {
                this.isSuccess.Set(context, oLib.Output);
            }
            else
            {
                this.Result.Set(context,Convert.ToString(new Exception(oLib.ErrorMessage)));
            }
        }
    }
}
