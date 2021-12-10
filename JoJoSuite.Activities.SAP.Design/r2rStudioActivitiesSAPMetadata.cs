using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Presentation.Metadata;

namespace JoJoSuite.Activities.SAP.Design
{
    public sealed class r2rStudioActivitiesSAPMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();

            SAPContainerDesigner.RegisterMetadata(builder);
            SAPClickDesigner.RegisterMetadata(builder);
            SAPEnterKeyDesigner.RegisterMetadata(builder);
            SAPGetTextDesigner.RegisterMetadata(builder);
            SAPSetTextDesigner.RegisterMetadata(builder);
            SAPSelectDesigner.RegisterMetadata(builder);
            SAPSetFocusDesigner.RegisterMetadata(builder);
            SAPSelectNodeDesigner.RegisterMetadata(builder);
            SAPFindChildDesigner.RegisterMetadata(builder);
            SAPScrollBarDesigner.RegisterMetadata(builder);
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
