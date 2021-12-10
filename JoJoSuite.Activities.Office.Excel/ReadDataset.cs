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
    public sealed class ReadDataset : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide filename with path")]
        [DefaultValue("")]
        [DisplayName("File Path")]
        public InArgument<string> FilePath { get; set; }

        [Category("Input")]
        [Description("First row as column")]
        [DisplayName("UseHeaderRow")]
        public bool UserHeaderRow { get; set; }

        [Category("Output")]
        [DisplayName("DataSet")]
        public OutArgument<DataSet> ResultDataSet { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            r2rReadDataset oLib = new r2rReadDataset();
            oLib.FilePath = context.GetValue(this.FilePath);
            oLib.UserHeaderRow = this.UserHeaderRow;

            bool res = oLib.DoAction().Result;

            if (res)
            {
                this.Result.Set(context, oLib.Error.ToString());
                this.ResultDataSet.Set(context, oLib.Ds);
            }
            else
            {
                this.Result.Set(context, new Exception(oLib.ErrorMessage).ToString());
            }
        }
    }
}
