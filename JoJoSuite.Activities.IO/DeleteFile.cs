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
    public sealed class DeleteFile : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide input file with path")]
        [DefaultValue(null)]
        public InArgument<string> Filename { get; set; }

        [Category("Output")]
        [Description("")]
        [DefaultValue(null)]
        public OutArgument<bool> isSuccess { get; set; }
        protected override void Execute(NativeActivityContext context)
        {

            r2rDeleteFile oLib = new r2rDeleteFile();
            oLib.FileName = context.GetValue(this.Filename);
            bool res = oLib.DoAction();

            if (res)
            {
                this.isSuccess.Set(context, oLib.Output);
            }
            else
            {
                this.Result.Set(context, Convert.ToString(new Exception(oLib.ErrorMessage)));
            }
        }
    }
}
