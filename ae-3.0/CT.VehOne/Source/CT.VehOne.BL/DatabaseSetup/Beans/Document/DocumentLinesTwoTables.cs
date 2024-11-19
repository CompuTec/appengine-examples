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
        List<IUDOField> fields = CreateFieldsForTable();
        IUDOTable UdoTable = new UDOTable(fields, ROW_TABLE_BOM_ITEMS_NAME, ROW_TABLE_BOM_ITEMS_DESCRIPTION,
            BoUTBTableType.bott_DocumentLines);
        UdoTable.RegisteredUDOName = ROW_TABLE_BOM_ITEMS_NAME;
        UdoTable.RegisteredUDOCode = ROW_TABLE_BOM_ITEMS_DEFAULT_CODE;

        UdoTable.CanArchive = BoYesNoEnum.tYES;
        UdoTable.ArchiveTableName = "CT_VO_ADOC2";

        return UdoTable;
    }

    protected List<IUDOField> CreateFieldsForTable()
    {
        List<IUDOField> fields = new List<IUDOField>();

        IUDOField Sequence = new UDOTableField();
        Sequence.SetName("Sequence");
        Sequence.SetDescription("Sequence");
        Sequence.SetType(BoFieldTypes.db_Numeric);
        Sequence.SetEditSize(10);
        //Sequence.SetDefaultValue("0");
        fields.Add(Sequence);

        IUDOField ItemCode = new UDOTableField();
        ItemCode.SetName("ItemCode");
        ItemCode.SetDescription("ItemCode");
        ItemCode.SetType(BoFieldTypes.db_Alpha);
        ItemCode.SetEditSize(50);
        //ItemCode.SetDefaultValue("0");
        fields.Add(ItemCode);

        IUDOField Description = new UDOTableField();
        Description.SetName("Description");
        Description.SetDescription("Item Description");
        Description.SetType(BoFieldTypes.db_Alpha);
        Description.SetEditSize(200);
        //Description.SetDefaultValue("0");
        fields.Add(Description);

        IUDOField Revision = new UDOTableField();
        Revision.SetName("Revision");
        Revision.SetDescription("Revision");
        Revision.SetType(BoFieldTypes.db_Alpha);
        Revision.SetEditSize(20);
        //Revision.SetDefaultValue("0");
        fields.Add(Revision);


        return fields;
    }

}