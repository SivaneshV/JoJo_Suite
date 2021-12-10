using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Activities.Presentation.Metadata;

namespace JoJoSuite.Activities.ClipBoard.Design
{
    public sealed class r2rStudioActivitiesClipBoardMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();            
            CopyDesigner.RegisterMetadata(builder);
            PasteDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        public void Register()
        {
            RegisterAll();
        }
    }
}
