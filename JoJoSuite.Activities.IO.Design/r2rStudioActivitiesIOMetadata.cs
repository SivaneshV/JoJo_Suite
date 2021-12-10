using System.Activities.Presentation.Metadata;

namespace JoJoSuite.Activities.IO.Design
{
    public sealed class r2rStudioActivitiesIOMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            FileExistsDesigner.RegisterMetadata(builder);
            CreateFileDesigner.RegisterMetadata(builder);
            DeleteFileDesigner.RegisterMetadata(builder);
            OpenFileDesigner.RegisterMetadata(builder);
            CopyPasteFileDesigner.RegisterMetadata(builder);
            FolderExistsDesigner.RegisterMetadata(builder);
            CreateFolderDesigner.RegisterMetadata(builder);
            DeleteFolderDesigner.RegisterMetadata(builder);
            InputBoxDesigner.RegisterMetadata(builder);
            MoveFileDesigner.RegisterMetadata(builder);           
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
