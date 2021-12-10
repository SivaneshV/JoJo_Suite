using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities.Presentation.Metadata;
using JoJoSuite.Business.Designer;

namespace JoJoSuite.Actions.Office.Excel.Design
{
    public sealed class r2rStudioActivitiesOfficeExcelMetadata : IRegisterMetadata
    {
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            CreateWorkbookDesigner.RegisterMetadata(builder);
            OpenWorkbookDesigner.RegisterMetadata(builder);
            GetSheetDesigner.RegisterMetadata(builder);
            LastRowIndexDesigner.RegisterMetadata(builder);
            CopyPasteRangeDesigner.RegisterMetadata(builder);
            DeleteRowsRangeDesigner.RegisterMetadata(builder);
            DeleteRowSpecificCellsDesigner.RegisterMetadata(builder);
            AddSheetDesigner.RegisterMetadata(builder);
            SetSheetDesigner.RegisterMetadata(builder);
            GetValueDesigner.RegisterMetadata(builder);
            SetValueDesigner.RegisterMetadata(builder);
            ShowExcelDesigner.RegisterMetadata(builder);
            SaveWorkbookDesigner.RegisterMetadata(builder);
            CreateTableDesigner.RegisterMetadata(builder);
            AddColumnDesigner.RegisterMetadata(builder);
            AddRowDesigner.RegisterMetadata(builder);
            DeleteColumnDesigner.RegisterMetadata(builder);
            DeleteRowDesigner.RegisterMetadata(builder);
            CreateFormulaDesigner.RegisterMetadata(builder);
            ExcelCopyPasteDesigner.RegisterMetadata(builder);
            RunMacroDesigner.RegisterMetadata(builder);
            ReadDatasetDesigner.RegisterMetadata(builder);
            FilterDataTableDesigner.RegisterMetadata(builder);
            AggregateFieldDesigner.RegisterMetadata(builder);
            DataSetToExcelDesigner.RegisterMetadata(builder);

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        public void Register()
        {
            RegisterAll();
        }
    }
}
