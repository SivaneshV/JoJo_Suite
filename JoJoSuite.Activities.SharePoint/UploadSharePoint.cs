using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace JoJoSuite.Activities.SharePoint
{

    public sealed class UploadSharePoint : NativeActivity<object>
    {
        // Define an activity input argument of type string
        [Category("Input")]
        [Description("Select file path")]
        [DisplayName("File Path")]
        public InArgument<string> FilePath { get; set; }
        [Category("Input")]
        [Description("Provide sharepoint url")]
        [DisplayName("Site URL")]
        public InArgument<string> SiteUrl { get; set; }
        [Category("Input")]
        [Description("Provide Folder Name")]
        [DisplayName("Foler Name")]
        public InArgument<string> FolderName { get; set; }

        [Category("Input")]
        [Description("Provide sharepoint username / client Id")]
        [DisplayName("Username")]
        public InArgument<string> ClientId { get; set; }

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

        [Category("Input")]
        [Description("Provide sharepoint password / client secret key")]
        [DisplayName("Password")]
        public InArgument<string> ClientSecret { get; set; }
        [Category("Input")]
        [Description("Is overWrite to sharepoint file if exist")]
        [DisplayName("Overwrite")]
        public bool OverWrite { get; set; }
        [Category("Output")]
        [Description("File path for uploaded sharepoint file")]
        [DisplayName("Uploaded URL")]
        public OutArgument<string> newUrl { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(NativeActivityContext context)
        {
            var FilePath = context.GetValue(this.FilePath);
            var SiteUrl = context.GetValue(this.SiteUrl);
            var FolderName = context.GetValue(this.FolderName);
            var ClientId = context.GetValue(this.ClientId);
            var ClientSecret = context.GetValue(this.ClientSecret);
            var OverWrite = this.OverWrite;
            var newUrl = string.Empty;
            var AuthType = this.AuthType;

            HP.Robotics.Sharepoint.SharePoint objSharePoint = new HP.Robotics.Sharepoint.SharePoint();
            objSharePoint.UploadToSharePoint(FilePath, SiteUrl, FolderName, ClientId, ClientSecret, OverWrite, AuthType.ToString(), out newUrl);
            // Obtain the runtime value of the Text input argument
           
        }
    }
}
