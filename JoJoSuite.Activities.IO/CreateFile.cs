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
    public sealed class CreateFile : NativeActivity<string>    
    {
        [Category("Input")]
        [Description("Please provide input file with path")]
        [DefaultValue(null)]
        public InArgument<string> Filename { get; set; }

        [Category("Input")]
        [Description("To overwrite the existing file")]
        public bool Overwrite { get; set; }

        [Category("Input")]
        [Description("File content")]
        [DefaultValue(null)]
        public InArgument<string> Content { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rCreateFile oLib = new r2rCreateFile();          
            oLib.FileName = context.GetValue(this.Filename);
            oLib.Overwrite = this.Overwrite;
            oLib.Content = context.GetValue(this.Content);
          

            bool res = oLib.DoAction();

            if (res)
            {
                this.Result.Set(context, Convert.ToString(res));
            }
            else
            {
                this.Result.Set(context, Convert.ToString(new Exception(oLib.ErrorMessage)));
            }
        }
    }
}
