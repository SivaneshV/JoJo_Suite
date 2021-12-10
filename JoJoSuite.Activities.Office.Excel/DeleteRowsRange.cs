using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using JoJoSuite.Library.Office.Excel;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class DeleteRowsRange : NativeActivity<object>
    {
        [Category("Input")]
        [Description("Please provide Source WorkSheet")]
        [DisplayName("Source WorkSheet")]
        public InArgument<Worksheet> xlWorkSheet { get; set; }      

        [Category("Input")]
        [Description("Range Column From")]
        [DisplayName("Range Column From")]
        public InArgument<string> RangeColumnFrom { get; set; }

        [Category("Input")]
        [Description("Range Column To")]
        [DisplayName("Range Column To")]
        public InArgument<string> RangeColumnTo { get; set; }

        [Category("Input")]
        [Description("Range Row From Index")]
        [DisplayName("Range Row From Index")]
        public InArgument<int> RangeRowIndexFrom { get; set; }

        [Category("Input")]
        [Description("Range Row To Index")]
        [DisplayName("Range Row To Index")]
        public InArgument<int> RangeRowIndexTo { get; set; }

        [Category("Output")]
        [DefaultValue(null)]
        [DisplayName("IsSucess")]
        public OutArgument<bool> IsSuccess { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rDeleteRowsRange oLib = new r2rDeleteRowsRange();
            oLib.xlWorkSheet = context.GetValue(this.xlWorkSheet);
           
            oLib.RangeColumnFrom = context.GetValue(this.RangeColumnFrom);
            oLib.RangeColumnTo = context.GetValue(this.RangeColumnTo);
            oLib.RangeRowFrom = context.GetValue(this.RangeRowIndexFrom);
            oLib.RangeRowTo = context.GetValue(this.RangeRowIndexTo);


            bool res = oLib.DoAction();

            if (res)
            {
                IsSuccess.Set(context, oLib.IsSucces);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage));
            }
        }
    }
}
