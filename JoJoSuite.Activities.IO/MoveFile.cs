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

    public sealed class MoveFile : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide Source File ")]
        [DefaultValue(null)]
        [DisplayName("Source File")]
        public InArgument<string> SourcePath { get; set; }

        [Category("Input")]
        [Description("Please provide Destination Path")]
        [DefaultValue(null)]
        [DisplayName("Destination Path")]
        public InArgument<string> DestinationPath { get; set; }

        [Category("Input")]
        [Description("To Keep Source File")]
        [DefaultValue(false)]
        public bool KeepCopy { get; set; }
        
        protected override void Execute(NativeActivityContext context)
        {
            r2rMoveFile oLib = new r2rMoveFile();
            oLib.SourcePath = context.GetValue(this.SourcePath);
            oLib.DestinationPath = context.GetValue(this.DestinationPath); 

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
