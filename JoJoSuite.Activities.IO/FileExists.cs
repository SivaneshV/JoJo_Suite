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
    public sealed class FileExists : NativeActivity<string>  
    {
        [Category("Input")]
        [Description("Please provide input file with path")]
        [DefaultValue(null)]
        public InArgument<string> Filename { get; set; }

        [Category("Output")]
        [Description("File exist result")]
        [DefaultValue(null)]
        public OutArgument<bool> isSuccess { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rFileExists oLib = new r2rFileExists();
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
