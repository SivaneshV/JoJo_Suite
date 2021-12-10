using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Office.Excel;
using Microsoft.Office.Interop.Excel;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class SetValue : NativeActivity<object>
    {
        public SetValue()
        {
            this.DisplayName = "Write Cell Value";
        }
        [Category("Input")]
        [Description("Please provide Worksheet")]
        [DefaultValue(null)]
        [DisplayName("WorkSheet")]
        public InArgument<Worksheet> xlWorksheet { get; set; }

        [Category("Input")]
        [Description("Please provide CellAddress(ex: \"A1\",\"D25\"")]
        [DefaultValue(null)]
        [DisplayName("CellAddress")]
        public InArgument<string> CellAddress { get; set; }

        [Category("Input")]
        [Description("Please provide input value")]
        [DefaultValue(null)]
        [DisplayName("Value")]
        public InArgument<string> Value { get; set; }
        
            
            
        protected override void Execute(NativeActivityContext context)
        {
            r2rSetValue oLib = new r2rSetValue();
            oLib.xlWorksheet = context.GetValue(this.xlWorksheet);
            oLib.CellAddress = context.GetValue(this.CellAddress);
            oLib.SetValue = context.GetValue(this.Value);
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
