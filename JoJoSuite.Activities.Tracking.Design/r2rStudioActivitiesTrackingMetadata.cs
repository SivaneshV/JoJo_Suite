using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Presentation.Metadata;
namespace JoJoSuite.Activities.Tracking.Design
{
    public sealed class  r2rStudioActivitiesTrackingMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            CountTrackerDesigner.RegisterMetadata(builder);
            LogMessageDesigner.RegisterMetadata(builder);
            UpdateStatusDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        public void Register()
        {
            RegisterAll();
        }
    }
}
