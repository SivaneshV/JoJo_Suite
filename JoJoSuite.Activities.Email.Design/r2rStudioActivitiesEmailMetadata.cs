using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Presentation.Metadata;
namespace JoJoSuite.Activities.Email.Design
{
    public sealed class r2rStudioActivitiesEmailMetadata : IRegisterMetadata    
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            GetMailDesigner.RegisterMetadata(builder);
            LoginToServerDesigner.RegisterMetadata(builder);
            ReadMailDesigner.RegisterMetadata(builder);
            SendMailDesigner.RegisterMetadata(builder);
            MailMoveToFolderDesigner.RegisterMetadata(builder);
            GetMailAttachmentsDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
