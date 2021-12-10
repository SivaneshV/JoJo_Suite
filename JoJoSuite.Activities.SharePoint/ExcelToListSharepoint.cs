using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;

namespace JoJoSuite.Activities.SharePoint
{

    public sealed class ExcelToListSharepoint : NativeActivity<object>
    {
        // Define an activity input argument of type string
        [Category("Input")]
        [Description("Provide Excel Filepath to Upload")]
        [DisplayName("Excel Filepath")]
        public InArgument<string> ExcelFilePath { get; set; }
        [Category("Input")]
        [Description("Provide Sharepoint Site Path (URL)")]
        [DisplayName("Site Path")]
        public InArgument<string> SharepointSitePath { get; set; }
        [Category("Input")]
        [Description("Provide Sharepoint List Title")]
        [DisplayName("List Title")]
        public InArgument<string> SharepointListTitle { get; set; }
        [Category("Input")]
        [Description("Delete All Existing Records")]
        [DisplayName("Delete Existing Records")]
        public InArgument<bool> IsDeleteExisting { get; set; }

        [Category("Input")]
        [Description("Provide sharepoint username / client Id")]
        [DisplayName("Username")]        
        public InArgument<string> ClientId { get; set; }
        [Category("Input")]
        [Description("Provide sharepoint password / client secret key")]
        [DisplayName("Password")]
        public InArgument<string> ClientSecret { get; set; }

        [Category("Input")]
        [Description("Provide sharepoint authentication type")]
        [DisplayName("Auth Type")]
        public r2rAuthType AuthType { get; set; }


        protected override void Execute(NativeActivityContext context)
        {
            var ExcelFilePath = context.GetValue(this.ExcelFilePath);
            var SharepointSitePath = context.GetValue(this.SharepointSitePath);
            var SharepointListTitle = context.GetValue(this.SharepointListTitle);
            var IsDeleteExisting = context.GetValue(this.IsDeleteExisting);
            var ClientId = context.GetValue(this.ClientId);
            var ClientSecret = context.GetValue(this.ClientSecret);
            var AuthType = this.AuthType;

            DataSet dataSet = new DataSet();

            string sexcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + "; Extended Properties = 'Excel 8.0;HDR=Yes';";

            OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
            oledbconn.Open();

            var tables = oledbconn.GetSchema("Tables");

            var tablename = tables.Rows[0]["TABLE_NAME"].ToString().Replace("'", "");

            string myexceldataquery = "select * from [" + tablename + "]";
            OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);

            OleDbDataAdapter adapter = new OleDbDataAdapter(oledbcmd);

            adapter.Fill(dataSet);

            oledbconn.Close();

            HP.Robotics.Sharepoint.SharePoint objSharePoint = new HP.Robotics.Sharepoint.SharePoint();
            objSharePoint.UploadDatasetToList(dataSet, SharepointSitePath, SharepointListTitle, ClientId, ClientSecret, IsDeleteExisting);

        }
    }
}