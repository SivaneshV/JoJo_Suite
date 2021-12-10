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

    public sealed class GetValue : NativeActivity<object>
    {
        public GetValue()
        {
            this.DisplayName = "Read Cell Value";
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


        [Category("Output")]       
        [DefaultValue(null)]
        [DisplayName("Get Value")]
        public OutArgument<string> GetValueResult { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            r2rGetValue oLib = new r2rGetValue();
            oLib.xlWorksheet = context.GetValue(this.xlWorksheet);
            oLib.CellAddress= context.GetValue(this.CellAddress);
            bool res = oLib.DoAction();

            if (res)
            {
                GetValueResult.Set(context, oLib.GetValue);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
