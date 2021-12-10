using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Office.Excel;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class ShowExcel : NativeActivity<object>
    {
        [Category("Input")]
        [Description("Please provide filename with path")]
        [DefaultValue(null)]
        [DisplayName("File Name")]
        public InArgument<string> FileName { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            r2rShowExcel oLib = new r2rShowExcel();
            oLib.FileName = context.GetValue(this.FileName);      
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
