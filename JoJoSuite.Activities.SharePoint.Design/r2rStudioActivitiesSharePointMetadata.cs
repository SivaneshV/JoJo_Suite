using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Activities.SharePoint.Design
{
    public sealed class r2rStudioActivitiesSharePointMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            UploadSharePointDesigner.RegisterMetadata(builder);
            DownloadSharepoint.RegisterMetadata(builder);
            ExcelToListSharepointDesigner.RegisterMetadata(builder);
            CreateFolderSharpointDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
