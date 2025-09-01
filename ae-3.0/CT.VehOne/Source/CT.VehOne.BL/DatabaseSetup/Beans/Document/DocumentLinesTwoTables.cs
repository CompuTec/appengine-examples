namespace CT.VehOne.BL.DatabaseSetup.Beans.Document;

[Install]
public class DocumentLinesTwoTables : UDOManager
{
    public const String ROW_TABLE_BOM_ITEMS_NAME = "CT_VO_DOC2";
    public const String ROW_TABLE_BOM_ITEMS_DESCRIPTION = "VODoc2: Items2";
    public const String ROW_TABLE_BOM_ITEMS_DEFAULT_CODE = ROW_TABLE_BOM_ITEMS_NAME + "_CODE";

    public DocumentLinesTwoTables(ICoreConnection connection, IDatabaseSetupManager manager) : base(connection,
        manager)
    {
    }


    protected override IUDOTable CreateUDOTable()
    {
        var builder = UDOTable.CreateDocumentLinesBuilder()
            .WithTable(ROW_TABLE_BOM_ITEMS_NAME, ROW_TABLE_BOM_ITEMS_DESCRIPTION)
            .WithArchiveTable("CT_VO_ADOC2")
            .AddField("Sequence", "Sequence").AsNumeric().Add()
            .AddField("ItemCode", "ItemCode").AsAlpha(50).Add()
            .AddField("Description", "Item Description").AsAlpha(200).Add()
            .AddField("Revision", "Revision").AsAlpha(20).Add();

        return builder.Build();
    }
}