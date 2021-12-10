using r2rStudio.Activities.Database.Design;
using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r2rStudio.Activities.Database.Designer
{
    class ConnectToServerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            RegisterAll();
        }

        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();

            ConnectToServerDesigner.RegisterMetadata(builder);

            MetadataStore.AddAttributeTable(builder.CreateTable());


        }
    }
}
