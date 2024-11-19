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
        List<IUDOField> fields = CreateFieldsForTable();
        IUDOTable UdoTable = new UDOTable(fields, ROW_TABLE_BOM_ITEMS_NAME, ROW_TABLE_BOM_ITEMS_DESCRIPTION,
            BoUTBTableType.bott_DocumentLines);
        UdoTable.RegisteredUDOName = ROW_TABLE_BOM_ITEMS_NAME;
        UdoTable.RegisteredUDOCode = ROW_TABLE_BOM_ITEMS_DEFAULT_CODE;

        UdoTable.CanArchive = BoYesNoEnum.tYES;
        UdoTable.ArchiveTableName = "CT_VO_ADOC3";

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

        IUDOField Intermediat = new UDOTableField();
        Intermediat.SetName("Intermediat");
        Intermediat.SetDescription("Intermediat");
        Intermediat.SetType(BoFieldTypes.db_Alpha);
        Intermediat.SetEditSize(20);
        //Revision.SetDefaultValue("0");
        fields.Add(Intermediat);


        IUDOField Parent = new UDOTableField();
        Parent.SetName("Parent");
        Parent.SetDescription("Parent");
        Parent.SetType(BoFieldTypes.db_Numeric);
        Parent.SetEditSize(10);
        //Factor.SetDefaultValue("0");
        fields.Add(Parent);

        IUDOField Quantity = new UDOTableField();
        Quantity.SetName("Quantity");
        Quantity.SetDescription("Quantity");
        Quantity.SetType(BoFieldTypes.db_Float);
        Quantity.SetSubType(BoFldSubTypes.st_Quantity);
        Quantity.SetEditSize(20);
        //Quantity.SetDefaultValue("0");
        fields.Add(Quantity);


        return fields;
    }

    protected override void SetChildTables()
    {
    }
}