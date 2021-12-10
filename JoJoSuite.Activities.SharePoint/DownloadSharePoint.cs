using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace JoJoSuite.Activities.SharePoint
{

    public sealed class DownloadSharePoint : NativeActivity<object>
    {
        // Define an activity input argument of type string
        [Category("Input")]
        [Description("Provide Filepath to Doawload")]
        [DisplayName("Folder Path")]
        public InArgument<string> FolderPath { get; set; }
        [Category("Input")]
        [Description("Provide Filepath to Doawload")]
        [DisplayName("File Name")]
        public InArgument<string> FileName { get; set; }
        [Category("Input")]
        [Description("Provide Sharepoint site URL")]
        [DisplayName("Site URL")]
        public InArgument<string> SiteUrl { get; set; }
        [Category("Input")]
        [Description("Provide Download location")]
        [DisplayName("Download Location")]
        public InArgument<string> DownloadLocation { get; set; }
        [Category("Input")]
        [Description("Provide Scenario of File Download")]
        [DisplayName("Search Type")]
        public r2rSearchType Type { get; set; }
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

        //[Category("Input")]
        //[Description("Provide sharepoint username")]
        //[DisplayName("UserName")]
        //public InArgument<string> UserName { get; set; }
        //[Category("Input")]
        //[Description("Provide sharepoint password")]
        //[DisplayName("Password")]
        //public InArgument<string> Password { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext context)
        {
            var FolderPath = context.GetValue(this.FolderPath);
            var FileName = context.GetValue(this.FileName);
            var SiteUrl = context.GetValue(this.SiteUrl);
            var DownloadLocation = context.GetValue(this.DownloadLocation);
            var ClientId = context.GetValue(this.ClientId);
            var ClientSecret = context.GetValue(this.ClientSecret);
            var Type = this.Type.ToString();
            var AuthType = this.AuthType;
            HP.Robotics.Sharepoint.SharePoint objSharePoint = new HP.Robotics.Sharepoint.SharePoint();
            objSharePoint.DownloadFile(DownloadLocation, SiteUrl, FolderPath, FileName, ClientId, ClientSecret, Type, AuthType.ToString());
            // Obtain the runtime value of the Text input argument

        }
    }

    public enum r2rSearchType
    {
        All,
        Contains,
        Latest,
        Specific,
        SpecificWithExtension
    }

    public enum r2rAuthType
    {
        ClientAuth,
        WindowsAuth
    }
}