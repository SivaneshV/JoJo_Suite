using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using JoJoSuite.Library.Office.Excel;

namespace JoJoSuite.Actions.Office.Excel
{

    public sealed class ExcelCopyPaste : NativeActivity<object>
    {
        public ExcelCopyPaste()
        {
            this.DisplayName = "Copy Paste";
        }
        [RequiredArgument, Category("Input")]
        [Description("Please provide Worksheet")]
        [DefaultValue(null)]
        [DisplayName("Copy WorkSheet")]
        public InArgument<Worksheet> xlWorksheetCopy { get; set; }

        [RequiredArgument, Category("Input")]
        [Description("Please provide Worksheet")]
        [DefaultValue(null)]
        [DisplayName("Paste WorkSheet")]
        public InArgument<Worksheet> xlWorksheetPaste { get; set; }

        [Category("Input")]
        [Description("Please provide cellAddressCopy")]
        [DefaultValue(null)]
        [DisplayName("Copy cellAddress")]
        public InArgument<string> cellAddressCopy { get; set; }

        [Category("Input")]
        [Description("Please provide cellAddressCopy")]
        [DefaultValue(null)]
        [DisplayName("Paste cellAddress")]
        public InArgument<string> cellAddressPaste { get; set; }


        [Category("Input")]
        [DisplayName("EntireColumn")]
        public bool xlEntireColumn { get; set; }

        [Category("Input")]
        [DisplayName("EntireRow")]
        public bool xlEntireRow { get; set; }

        [Category("Input")]
        [DisplayName("PasteType")]
        [Description("Select Paste type")]
        public r2rPasteType PasteType { get; set; }

       
        protected override void Execute(NativeActivityContext context)
        {
            r2rXlCopyPaste oLib = new r2rXlCopyPaste();
            oLib.xlWorksheetCopy = context.GetValue(this.xlWorksheetCopy);
            oLib.xlWorksheetPaste = context.GetValue(this.xlWorksheetPaste);
            oLib.cellAddressCopy = context.GetValue(this.cellAddressCopy);
            oLib.cellAddressPaste = context.GetValue(this.cellAddressPaste);
            oLib.xlEntireColumn = this.xlEntireColumn;
            oLib.xlEntireRow = this.xlEntireRow;

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
    public enum r2rPasteType
    {
        Values,
        Formulas
    }
}
