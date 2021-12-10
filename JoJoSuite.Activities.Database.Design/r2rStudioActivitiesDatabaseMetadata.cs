using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Activities.Presentation.Metadata;

namespace JoJoSuite.Activities.Database.Design
{
    public sealed class r2rStudioActivitiesDatabaseMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            ConnectDesigner.RegisterMetadata(builder);
            DataQueryDesigner.RegisterMetadata(builder);
            NonDataQueryDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        public void Register()
        {
            RegisterAll();
        }
    }
}
