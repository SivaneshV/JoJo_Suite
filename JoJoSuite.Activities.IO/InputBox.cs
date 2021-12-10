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
    public sealed class InputBox : NativeActivity<string>
    {
        [Category("Input")]
        [Description("For Password")]
        public bool Password { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            r2rInputBox oLib = new r2rInputBox();
            oLib.Password = this.Password;
            bool res = oLib.DoAction();

            if (res)
            {
                this.Result.Set(context, Convert.ToString(oLib.Result));
            }
            else
            {
                this.Result.Set(context, Convert.ToString(new Exception(oLib.ErrorMessage)));
            }
        }
    }
}
