using JoJoSuite.Activities.Common;
using System.Activities.Presentation.Metadata;

namespace JoJoSuite.Activities.IO.Design
{
    public sealed class r2rStudioActivitiesCommonMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            CommentOutDesigner.RegisterMetadata(builder);

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
