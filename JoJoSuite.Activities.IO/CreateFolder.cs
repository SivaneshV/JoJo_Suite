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
    public sealed class CreateFolder : NativeActivity<string>    
    {
        [Category("Input")]
        [Description("Folder Path")]
        [DefaultValue(null)]
        public InArgument<string> FolderPath { get; set; }

        protected override void Execute(NativeActivityContext context)
        {

            r2rCreateFolder oLib = new r2rCreateFolder();
            oLib.FolderPath = context.GetValue(this.FolderPath);        


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
