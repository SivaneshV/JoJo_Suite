using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.Office.Excel;
using Microsoft.Office.Interop.Excel;
using System.Data;

namespace JoJoSuite.Actions.Office.Excel
{
    public sealed class DataSetToExcel : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide save location with path")]
        [DefaultValue("")]
        [DisplayName("File Path")]
        public InArgument<string> FilePath { get; set; }

        [Category("Input")]
        [Description("Please provide SheetName List eg 'sheet1|sheet2|sheet3'")]
        [DefaultValue("")]
        [DisplayName("SheetNames")]
        public InArgument<string> SheetNameslist { get; set; }

        [Category("Input")]
        [Description("Please provide Dataset")]
        [DisplayName("DataSet")]
        public InArgument<DataSet> DataSet { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            r2rDataSetToExcel oLib = new r2rDataSetToExcel();
            oLib.FilePath = context.GetValue(this.FilePath);
            oLib.SheetNameslist = context.GetValue(this.SheetNameslist)!=null ? context.GetValue(this.SheetNameslist):"";
            oLib.Ds = context.GetValue(this.DataSet);

            bool res = oLib.DoAction().Result;

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
