namespace CT.VehOne.BL.DatabaseSetup.Beans.MasterData;

[Install]
public class MasterDataLinesTable : UDOManager
{
    public const String MasterDataLinesTableName = "CT_VO_MDA1";
    public const String MasterDataLinesDescription = "VOMDA1: Items";
    public const String ROW_TABLE_BOM_ITEMS_DEFAULT_CODE = MasterDataLinesTableName + "_CODE";

    public MasterDataLinesTable(ICoreConnection connection, IDatabaseSetupManager manager) : base(connection,
        manager)
    {
    }


    protected override IUDOTable CreateUDOTable()
    {
        List<IUDOField> fields = CreateFieldsForTable();
        IUDOTable UdoTable = new UDOTable(fields, MasterDataLinesTableName, MasterDataLinesDescription,
            BoUTBTableType.bott_MasterDataLines);
        UdoTable.RegisteredUDOName = MasterDataLinesTableName;
        UdoTable.RegisteredUDOCode = ROW_TABLE_BOM_ITEMS_DEFAULT_CODE;

        UdoTable.CanArchive = BoYesNoEnum.tYES;
        UdoTable.ArchiveTableName = "CT_CS_AMDA1";

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


        IUDOField Factor = new UDOTableField();
        Factor.SetName("Factor");
        Factor.SetDescription("Factor");
        Factor.SetType(BoFieldTypes.db_Float);
        Factor.SetSubType(BoFldSubTypes.st_Quantity);
        Factor.SetEditSize(20);
        //Factor.SetDefaultValue("0");
        fields.Add(Factor);
        IUDOField Quantity = new UDOTableField();
        Quantity.SetName("Quantity");
        Quantity.SetDescription("Quantity");
        Quantity.SetType(BoFieldTypes.db_Float);
        Quantity.SetSubType(BoFldSubTypes.st_Quantity);
        Quantity.SetEditSize(20);
        //Quantity.SetDefaultValue("0");
        fields.Add(Quantity);

        IUDOField Result = new UDOTableField();
        Result.SetName("Result");
        Result.SetDescription("Result");
        Result.SetType(BoFieldTypes.db_Float);
        Result.SetSubType(BoFldSubTypes.st_Quantity);
        Result.SetEditSize(30);
        //Result.SetDefaultValue("0");
        fields.Add(Result);

        IUDOField BatchResult = new UDOTableField();
        BatchResult.SetName("BatchResult");
        BatchResult.SetDescription("Batch Result");
        BatchResult.SetType(BoFieldTypes.db_Float);
        BatchResult.SetSubType(BoFldSubTypes.st_Quantity);
        BatchResult.SetEditSize(30);
        //Result.SetDefaultValue("0");
        fields.Add(BatchResult);

        IUDOField Formula = new UDOTableField();
        Formula.SetName("Formula");
        Formula.SetDescription("Formula");
        Formula.SetType(BoFieldTypes.db_Memo);
        Formula.SetEditSize(250);
        //Formula.SetDefaultValue("0");
        fields.Add(Formula);


        IUDOField OcrCode = new UDOTableField();
        OcrCode.SetName("OcrCode");
        OcrCode.SetDescription("OcrCode");
        OcrCode.SetType(BoFieldTypes.db_Alpha);
        OcrCode.SetEditSize(50);
        //Formula.SetDefaultValue("0");
        fields.Add(OcrCode);
        IUDOField OcrCode2 = new UDOTableField();
        OcrCode2.SetName("OcrCode2");
        OcrCode2.SetDescription("OcrCode2");
        OcrCode2.SetType(BoFieldTypes.db_Alpha);
        OcrCode2.SetEditSize(50);
        //Formula.SetDefaultValue("0");
        fields.Add(OcrCode2);
        IUDOField OcrCode3 = new UDOTableField();
        OcrCode3.SetName("OcrCode3");
        OcrCode3.SetDescription("OcrCode3");
        OcrCode3.SetType(BoFieldTypes.db_Alpha);
        OcrCode3.SetEditSize(50);
        //Formula.SetDefaultValue("0");
        fields.Add(OcrCode3);
        IUDOField OcrCode4 = new UDOTableField();
        OcrCode4.SetName("OcrCode4");
        OcrCode4.SetDescription("OcrCode4");
        OcrCode4.SetType(BoFieldTypes.db_Alpha);
        OcrCode4.SetEditSize(50);
        //Formula.SetDefaultValue("0");
        fields.Add(OcrCode4);
        IUDOField OcrCode5 = new UDOTableField();
        OcrCode5.SetName("OcrCode5");
        OcrCode5.SetDescription("OcrCode5");
        OcrCode5.SetType(BoFieldTypes.db_Alpha);
        OcrCode5.SetEditSize(50);
        //Formula.SetDefaultValue("0");
        fields.Add(OcrCode5);
        return fields;
    }

}

