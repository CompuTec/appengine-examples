namespace CT.VehOne.BL.DatabaseSetup.Beans.Document;

[Install]
public class DocumentSubLines1Table : UDOManager
{
    public const String ROW_TABLE_BOM_ITEMS_NAME = "CT_VO_DOC3";
    public const String ROW_TABLE_BOM_ITEMS_DESCRIPTION = "VODoc3: Items3";
    public const String ROW_TABLE_BOM_ITEMS_DEFAULT_CODE = ROW_TABLE_BOM_ITEMS_NAME + "_CODE";

    public DocumentSubLines1Table(ICoreConnection connection, IDatabaseSetupManager manager) : base(connection,
        manager)
    {
    }


    protected override IUDOTable CreateUDOTable()
    {
        var builder = UDOTable.CreateDocumentLinesBuilder()
            .WithTable(ROW_TABLE_BOM_ITEMS_NAME, ROW_TABLE_BOM_ITEMS_DESCRIPTION)
            .WithArchiveTable("CT_VO_ADOC3")
            .AddField("Sequence", "Sequence").AsNumeric().Add()
            .AddField("ItemCode", "ItemCode").AsAlpha(50).Add()
            .AddField("Description", "Item Description").AsAlpha(200).Add()
            .AddField("Intermediat", "Intermediat").AsAlpha(20).Add()
            .AddField("Parent", "Parent").AsNumeric().Add()
            .AddField("Quantity", "Quantity").AsQuantity().Add();

        return builder.Build();
    }

    protected override void SetChildTables()
    {
    }
}