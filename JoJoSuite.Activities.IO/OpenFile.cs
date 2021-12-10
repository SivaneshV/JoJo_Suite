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
    public sealed class OpenFile : NativeActivity<string>  
    {
        [Category("Input")]
        [Description("Please provide input file with path")]
        [DefaultValue(null)]
        public InArgument<string> Filename { get; set; }

        [Category("Input")]
        [Description("Please check for getting file content in a array.")]
        public bool SplitLines { get; set; }
               

        [Category("Output")]
        [Description("File content as string array.")]
        public OutArgument<string[]> ResultArray { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rOpenFile oLib = new r2rOpenFile();
            oLib.FileName = context.GetValue(this.Filename);           
            oLib.SplitLines = this.SplitLines;
           
            bool res = oLib.DoAction();
            if (res)
            {
                this.Result.Set(context, oLib.Result);
                this.ResultArray.Set(context, oLib.Resultarray);
            }
            else
            {
                this.Result.Set(context, Convert.ToString(new Exception(oLib.ErrorMessage)));
            }
        }
    }
}
