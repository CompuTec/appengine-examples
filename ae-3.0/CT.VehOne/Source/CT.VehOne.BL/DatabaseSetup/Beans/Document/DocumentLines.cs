 
namespace CT.VehOne.BL.DatabaseSetup.Beans.Document;

[Install]
public class DocumentLines : UDOManager
{
    public const String ROW_TABLE_BOM_ITEMS_NAME = "CT_VO_DOC1";
    public const String ROW_TABLE_BOM_ITEMS_DESCRIPTION = "VODoc1: Items";
    public const String ROW_TABLE_BOM_ITEMS_DEFAULT_CODE = ROW_TABLE_BOM_ITEMS_NAME + "_CODE";

    public DocumentLines(ICoreConnection connection, IDatabaseSetupManager manager) : base(connection, manager)
    {
    }


    protected override IUDOTable CreateUDOTable()
    {
        var builder = UDOTable.CreateDocumentLinesBuilder()
            .WithTable(ROW_TABLE_BOM_ITEMS_NAME, ROW_TABLE_BOM_ITEMS_DESCRIPTION)
            .WithArchiveTable("CT_VO_ADOC1")
            .AddField("Sequence", "Sequence").AsNumeric().Add()
            .AddField("ItemCode", "ItemCode").AsAlpha(50).Add()
            .AddField("Description", "Item Description").AsAlpha(200).Add()
            .AddField("Revision", "Revision").AsAlpha(20).Add()
            .AddField("Factor", "Factor").AsQuantity().Add()
            .AddField("Quantity", "Quantity").AsQuantity().Add()
            .AddField("Result", "Result").AsQuantity().Add()
            .AddField("BatchResult", "Batch Result").AsQuantity().Add()
            .AddField("Formula", "Formula").AsMemo().Add();

        return builder.Build();
    }
}
