using JoJoSuite.Activities.Security.Design;
using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Activities.SharePoint.Design
{
    public sealed class r2rStudioActivitiesSecurityMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            DigitalIDDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
