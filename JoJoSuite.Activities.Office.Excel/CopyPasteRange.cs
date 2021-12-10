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
    public sealed class CopyPasteRange : NativeActivity<object>
    {
        [Category("Input")]
        [Description("Please provide Source WorkSheet")]
        [DisplayName("Source WorkSheet")]
        public InArgument<Worksheet> xlWorkSheetSource { get; set; }

        [Category("Input")]
        [Description("Please provide Destination WorkSheet")]
        [DisplayName("Destination WorkSheet")]
        public InArgument<Worksheet> xlWorkSheetDestination { get; set; }

        [Category("Input")]
        [Description("Source Column From")]
        [DisplayName("Source Column From")]
        public InArgument<string> SourceColumnFrom { get; set; }

        [Category("Input")]
        [Description("Source Column To")]
        [DisplayName("Source Column To")]
        public InArgument<string> SourceColumnTo { get; set; }

        [Category("Input")]
        [Description("Source Row From Index")]
        [DisplayName("Source Row From Index")]
        public InArgument<int> SourceRowIndexFrom { get; set; }

        [Category("Input")]
        [Description("Source Row To Index")]
        [DisplayName("Source Row To Index")]
        public InArgument<int> SourceRowIndexTo { get; set; }



        [Category("Input")]
        [Description("Destination Column From")]
        [DisplayName("Destination Column From")]
        public InArgument<string> DestinationColumnFrom { get; set; }

        [Category("Input")]
        [Description("Destination Column To")]
        [DisplayName("Destination Column To")]
        public InArgument<string> DestinationColumnTo { get; set; }

        [Category("Input")]
        [Description("Destination Row From Index")]
        [DisplayName("Destination Row From Index")]
        public InArgument<int> DestinationRowIndexFrom { get; set; }

        [Category("Input")]
        [Description("Destination Row To Index")]
        [DisplayName("Destination Row To Index")]
        public InArgument<int> DestinationRowIndexTo { get; set; }

        [Category("Output")]
        [DefaultValue(null)]
        [DisplayName("IsSucess")]
        public OutArgument<bool> IsSuccess { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            r2rCopyPasteRange oLib = new r2rCopyPasteRange();
            oLib.xlWorkSheetSource = context.GetValue(this.xlWorkSheetSource);
            oLib.xlWorkSheetDestination = context.GetValue(this.xlWorkSheetDestination);
         
            oLib.SourceColumnFrom = context.GetValue(this.SourceColumnFrom);
            oLib.SourceColumnTo = context.GetValue(this.SourceColumnTo);
            oLib.SourceRowFrom = context.GetValue(this.SourceRowIndexFrom);
            oLib.SourceRowTo = context.GetValue(this.SourceRowIndexTo);

            oLib.DestColumnFrom = context.GetValue(this.DestinationColumnFrom);
            oLib.DestColumnTo = context.GetValue(this.DestinationColumnTo);
            oLib.DestRowFrom = context.GetValue(this.DestinationRowIndexFrom);
            oLib.DestRowTo = context.GetValue(this.DestinationRowIndexTo);

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
