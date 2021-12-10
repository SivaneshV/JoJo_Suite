using System.Activities.Presentation.Metadata;

namespace JoJoSuite.Activities.Web.Design
{
   public sealed class r2rStudioActivitiesWebMetadata:IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            GetBrowserDesigner.RegisterMetadata(builder);
            GetTextDesigner.RegisterMetadata(builder);
            SetTextDesigner.RegisterMetadata(builder);
            WebClickDesigner.RegisterMetadata(builder);
            GetCollectionsDesigner.RegisterMetadata(builder);
            CloseBrowserDesigner.RegisterMetadata(builder);
            FileDownloadDesigner.RegisterMetadata(builder);
            SwitchIframeDesigner.RegisterMetadata(builder);
            SwitchToDesigner.RegisterMetadata(builder);
            DialogActionDesigner.RegisterMetadata(builder);
            DialogReadTextDesigner.RegisterMetadata(builder);
            DialogWriteTextDesigner.RegisterMetadata(builder);
            ListSelectDesigner.RegisterMetadata(builder);
            ReDirectUrlDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
