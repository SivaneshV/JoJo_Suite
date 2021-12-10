using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Office.Excel;
using Microsoft.Office.Interop.Excel;

namespace JoJoSuite.Actions.Office.Excel
{

    public sealed class RunMacro : NativeActivity<string>
    {
        // Define an activity input argument of type string
        [Category("Input")]
        [Description("Please provide filename with path")]
        [DefaultValue(null)]
        [DisplayName("File Path")]
        public InArgument<string> FilePath { get; set; }

        [Category("Input")]
        [Description("Please provide Macro Name")]
        [DisplayName("Macro Name")]
        public InArgument<string> MacroName { get; set; }

        [Category("Input")]
        [Description("Show excel")]
        [DisplayName("Show excel")]
        public bool xlvisible { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext context)
        {
            r2rRunMacro oLib = new r2rRunMacro();
            oLib.xlFileName = context.GetValue(this.FilePath);
            oLib.xlMacroName = context.GetValue(this.MacroName);
            oLib.xlVisible = this.xlvisible;
            bool res = oLib.DoAction();

            if (res)
            {
                this.Result.Set(context, oLib.Error.ToString());
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage).ToString());
            }
        }
    }
}
