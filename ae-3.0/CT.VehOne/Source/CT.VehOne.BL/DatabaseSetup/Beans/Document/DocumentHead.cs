namespace CT.VehOne.BL.DatabaseSetup.Beans.Document;

[Install]
public class DocumentHead : UDOManager
{
    public const String OBJECT_CODE = "CT_VO_DOCCode";
    public const String HEADER_TABLE_NAME = "CT_VO_ODOC";
    public const String HEADER_TABLE_DESCRIPTION = "VO_DOC";
    private readonly ITranslationService _translationService;

    public DocumentHead(ICoreConnection connection, ITranslationService translationService,
        IDatabaseSetupManager manager) : base(connection, manager)
    {
        _translationService = translationService;
    }


    protected override IUDOTable CreateUDOTable()
    {
        IUDOTable UdoTable = new UDOTable(CreateFieldsForHeaderTable(), CreateFindColumnsList(), HEADER_TABLE_NAME, HEADER_TABLE_DESCRIPTION,
            BoUTBTableType.bott_Document, CreateKeys());
        UdoTable.RegisteredUDOName = HEADER_TABLE_NAME;
        UdoTable.RegisteredUDOCode = OBJECT_CODE;
        UdoTable.CanArchive = BoYesNoEnum.tYES;
        UdoTable.CanCancel = BoYesNoEnum.tNO;
        UdoTable.CanClose = BoYesNoEnum.tYES;
        UdoTable.CanCreateDefaultForm = BoYesNoEnum.tYES;
        UdoTable.CanDelete = BoYesNoEnum.tYES;
        UdoTable.CanFind = BoYesNoEnum.tYES;
        UdoTable.CanLog = BoYesNoEnum.tYES;
        UdoTable.CanYearTransfer = BoYesNoEnum.tYES;
        UdoTable.ManageSeries = BoYesNoEnum.tYES;
        //UdoTable.ReportType = new ReportTypeUDO("BillOfManufacturingReportType", "", "CT_PF_OBOMCode", reportList);
        
        UdoTable.ArchiveTableName = "CT_VO_ADOC";
        return UdoTable;
    }

    private List<IUDOTableKey> CreateKeys()
    {
        List<IUDOTableKey> list = new List<IUDOTableKey>();
        // UDOTableKey klucz = new UDOTableKey("Index", HEADER_TABLE_NAME, new string[] { "ItemCode", "Revision" });
        // klucz.Unique = BoYesNoEnum.tYES;
        // list.Add(klucz);
        return list;
    }

    private List<IUDOFindColumn> CreateFindColumnsList()
    {
        List<IUDOFindColumn> findColumns = new List<IUDOFindColumn>();

        IUDOFindColumn column1 = new UDOFindColumn();
        column1.SetColumnAlias("U_ItemCode");
        column1.SetColumnDescription("Item Code");
        findColumns.Add(column1);

        //InventoryUoM Revision Quantity Factor WhsCode OccrCode Price Currency Remarks

        IUDOFindColumn uomColumn = new UDOFindColumn();
        uomColumn.SetColumnAlias("U_InventoryUoM");
        uomColumn.SetColumnDescription("UoM");
        findColumns.Add(uomColumn);

        IUDOFindColumn revisionColumn = new UDOFindColumn();
        revisionColumn.SetColumnAlias("U_Revision");
        revisionColumn.SetColumnDescription("Revision");
        findColumns.Add(revisionColumn);

        IUDOFindColumn quantityColumn = new UDOFindColumn();
        quantityColumn.SetColumnAlias("U_Quantity");
        quantityColumn.SetColumnDescription("Quantity");
        findColumns.Add(quantityColumn);

        IUDOFindColumn factorColumn = new UDOFindColumn();
        factorColumn.SetColumnAlias("U_Factor");
        factorColumn.SetColumnDescription("Factor");
        findColumns.Add(factorColumn);

        IUDOFindColumn whsCodeColumn = new UDOFindColumn();
        whsCodeColumn.SetColumnAlias("U_WhsCode");
        whsCodeColumn.SetColumnDescription("Warehouse Code");
        findColumns.Add(whsCodeColumn);

        IUDOFindColumn occrCode = new UDOFindColumn();
        occrCode.SetColumnAlias("U_OccrCode");
        occrCode.SetColumnDescription("Distribution Rule");
        findColumns.Add(occrCode);

        IUDOFindColumn price = new UDOFindColumn();
        price.SetColumnAlias("U_Price");
        price.SetColumnDescription("Price");
        findColumns.Add(price);

        IUDOFindColumn currency = new UDOFindColumn();
        currency.SetColumnAlias("U_Currency");
        currency.SetColumnDescription("Currency");
        findColumns.Add(currency);

        IUDOFindColumn remarks = new UDOFindColumn();
        remarks.SetColumnAlias("U_Remarks");
        remarks.SetColumnDescription("Remarks");
        findColumns.Add(remarks);

        IUDOFindColumn desc = new UDOFindColumn();
        desc.SetColumnAlias("U_Description");
        desc.SetColumnDescription("Item Description");
        findColumns.Add(desc);

        return findColumns;
    }

    private List<IUDOField> CreateFieldsForHeaderTable()
    {
        List<IUDOField> fields = new List<IUDOField>();

        IUDOField ItemCode = new UDOTableField();
        ItemCode.SetName("ItemCode");
        ItemCode.SetDescription("Item Code");
        ItemCode.SetEditSize(50);
        ItemCode.SetType(BoFieldTypes.db_Alpha);
        ItemCode.SetPrimaryKey(true);
        //ItemCode.SetDefaultValue("0");
        fields.Add(ItemCode);

        IUDOField Browser = new UDOTableField();
        Browser.SetName("Browser");
        Browser.SetDescription("Field to browse BOMs");
        Browser.SetEditSize(70);
        Browser.SetType(BoFieldTypes.db_Alpha);
        //ItemCode.SetDefaultValue("0");
        fields.Add(Browser);

        IUDOField Description = new UDOTableField();
        Description.SetName("Description");
        Description.SetDescription("Item Description");
        Description.SetEditSize(200);
        Description.SetType(BoFieldTypes.db_Alpha);
        //Description.SetDefaultValue("0"$);
        fields.Add(Description);

        IUDOField InventoryUoM = new UDOTableField();
        InventoryUoM.SetName("InventoryUoM");
        InventoryUoM.SetDescription("Unit of Measure");
        InventoryUoM.SetEditSize(20);
        InventoryUoM.SetType(BoFieldTypes.db_Alpha);
        //InventoryUoM.SetDefaultValue("0");
        fields.Add(InventoryUoM);

        IUDOField Revision = new UDOTableField();
        Revision.SetName("Revision");
        Revision.SetDescription("Revision");
        Revision.SetEditSize(20);
        Revision.SetType(BoFieldTypes.db_Alpha);
        Revision.SetPrimaryKey(true);
        //Revision.SetDefaultValue("0");
        fields.Add(Revision);


        IUDOField Quantity = new UDOTableField();
        Quantity.SetName("Quantity");
        Quantity.SetDescription("Quantity");
        Quantity.SetEditSize(20);
        Quantity.SetType(BoFieldTypes.db_Float);
        Quantity.SetSubType(BoFldSubTypes.st_Quantity);
        //Quantity.SetDefaultValue("0");
        fields.Add(Quantity);

        IUDOField Factor = new UDOTableField();
        Factor.SetName("Factor");
        Factor.SetDescription("Factor");
        Factor.SetEditSize(20);
        Factor.SetType(BoFieldTypes.db_Float);
        Factor.SetSubType(BoFldSubTypes.st_Quantity);
        //Factor.SetDefaultValue("0");
        fields.Add(Factor);


        IUDOField Warehouse = new UDOTableField();
        Warehouse.SetName("WhsCode");
        Warehouse.SetDescription("Warehouse");
        Warehouse.SetType(BoFieldTypes.db_Alpha);
        Warehouse.SetEditSize(8);
        fields.Add(Warehouse);

        IUDOField occrCode = new UDOTableField();
        occrCode.SetName("OccrCode");
        occrCode.SetDescription("Distribution Rule");
        occrCode.SetType(BoFieldTypes.db_Alpha);
        occrCode.SetEditSize(8);
        fields.Add(occrCode);

        IUDOField OcrCode = new UDOTableField();
        OcrCode.SetName("OcrCode");
        OcrCode.SetDescription("Distribution Rule");
        OcrCode.SetType(BoFieldTypes.db_Alpha);
        OcrCode.SetEditSize(8);
        fields.Add(OcrCode);

        IUDOField OcrCode2 = new UDOTableField();
        OcrCode2.SetName("OcrCode2");
        OcrCode2.SetDescription("Costing Code 2");
        OcrCode2.SetEditSize(8);
        OcrCode2.SetType(BoFieldTypes.db_Alpha);
        fields.Add(OcrCode2);

        IUDOField OcrCode3 = new UDOTableField();
        OcrCode3.SetName("OcrCode3");
        OcrCode3.SetDescription("Costing Code 3");
        OcrCode3.SetEditSize(8);
        OcrCode3.SetType(BoFieldTypes.db_Alpha);
        fields.Add(OcrCode3);

        IUDOField OcrCode4 = new UDOTableField();
        OcrCode4.SetName("OcrCode4");
        OcrCode4.SetDescription("Costing Code 4");
        OcrCode4.SetEditSize(8);
        OcrCode4.SetType(BoFieldTypes.db_Alpha);
        fields.Add(OcrCode4);

        IUDOField OcrCode5 = new UDOTableField();
        OcrCode5.SetName("OcrCode5");
        OcrCode5.SetDescription("Costing Code 5");
        OcrCode5.SetEditSize(8);
        OcrCode5.SetType(BoFieldTypes.db_Alpha);
        fields.Add(OcrCode5);

        IUDOField Project = new UDOTableField();
        Project.SetName("Project");
        Project.SetDescription("Project");
        Project.SetEditSize(20);
        Project.SetType(BoFieldTypes.db_Alpha);
        fields.Add(Project);

        IUDOField Price = new UDOTableField();
        Price.SetName("Price");
        Price.SetDescription("Price");
        Price.SetEditSize(20);
        Price.SetType(BoFieldTypes.db_Float);
        Price.SetSubType(BoFldSubTypes.st_Price);
        //Factor.SetDefaultValue("0");
        fields.Add(Price);

        IUDOField PriceList = new UDOTableField();
        PriceList.SetName("PriceList");
        PriceList.SetDescription("Price List");
        PriceList.SetEditSize(10);
        PriceList.SetType(BoFieldTypes.db_Numeric);
        fields.Add(PriceList);

        IUDOField Currency = new UDOTableField();
        Currency.SetName("Currency");
        Currency.SetDescription("Currency");
        Currency.SetEditSize(3);
        Currency.SetType(BoFieldTypes.db_Memo);
        fields.Add(Currency);
        //Factor.SetDefaultValue("0");

        IUDOField Remarks = new UDOTableField();
        Remarks.SetName("Remarks");
        Remarks.SetDescription("Remarks");
        Remarks.SetType(BoFieldTypes.db_Memo);
        //Remarks.SetEditSize(254);
        fields.Add(Remarks);

        IUDOField batchSize = new UDOTableField();
        batchSize.SetName("BatchSize");
        batchSize.SetDescription("Batch Size");
        batchSize.SetType(BoFieldTypes.db_Float);
        batchSize.SetSubType(BoFldSubTypes.st_Quantity);
        batchSize.DefaultValue = "1";
        fields.Add(batchSize);

        IUDOField prodType = new UDOTableField();
        prodType.SetName("ProdType");
        prodType.SetDescription("Production Type");
        prodType.SetEditSize(1);
        prodType.SetType(BoFieldTypes.db_Alpha);
        prodType.DefaultValue = "I";
        prodType.ValidValuesMD.Add("I", _translationService.GetTranslatedMessage("ProdTypeFieldI", "internal"));
        prodType.ValidValuesMD.Add("E", _translationService.GetTranslatedMessage("ProdTypeFieldE", "external"));
        fields.Add(prodType);

        var Yield = new UDOTableField();
        Yield.SetName("Yield");
        Yield.SetDescription("Yield Percentage");
        Yield.SetType(BoFieldTypes.db_Float);
        Yield.SetSubType(BoFldSubTypes.st_Percentage);
        Yield.SetEditSize(11);
        Yield.DefaultValue = "100";
        fields.Add(Yield);

        var YieldFormula = new UDOTableField();
        YieldFormula.SetName("YieldFormula");
        YieldFormula.SetDescription("Yield Formula");
        YieldFormula.SetType(BoFieldTypes.db_Memo);
        fields.Add(YieldFormula);

        var ItemFormula = new UDOTableField();
        ItemFormula.SetName("ItemFormula");
        ItemFormula.SetDescription("Item Formula");
        ItemFormula.SetType(BoFieldTypes.db_Memo);
        fields.Add(ItemFormula);

        var ScrapFormula = new UDOTableField();
        ScrapFormula.SetName("ScrapFormula");
        ScrapFormula.SetDescription("Scrap Formula");
        ScrapFormula.SetType(BoFieldTypes.db_Memo);
        fields.Add(ScrapFormula);

        var CoproductFormula = new UDOTableField();
        CoproductFormula.SetName("CoproductFormula");
        CoproductFormula.SetDescription("Coproduct Formula ");
        CoproductFormula.SetType(BoFieldTypes.db_Memo);
        fields.Add(CoproductFormula);

        var Instructions = new UDOTableField();
        Instructions.SetName("Instructions");
        Instructions.SetDescription("Instructions");
        Instructions.SetType(BoFieldTypes.db_Memo);
        fields.Add(Instructions);
        IUDOField ClrLns = new UDOTableField();
        ClrLns.SetName("ClrLns");
        ClrLns.SetDescription("ClrLns");
        ClrLns.SetType(BoFieldTypes.db_Alpha);
        ClrLns.SetEditSize(10);
        fields.Add(ClrLns);
        IUDOField ClrLnsL1 = new UDOTableField();
        ClrLnsL1.SetName("ClrLnsL1");
        ClrLnsL1.SetDescription("ClrLnsL1");
        ClrLnsL1.SetType(BoFieldTypes.db_Alpha);
        ClrLnsL1.SetEditSize(10);
        //Formula.SetDefaultValue("0");
        fields.Add(ClrLnsL1);
        IUDOField ClrLnsL2 = new UDOTableField();
        ClrLnsL2.SetName("ClrLnsL2");
        ClrLnsL2.SetDescription("ClrLnsL2");
        ClrLnsL2.SetType(BoFieldTypes.db_Alpha);
        ClrLnsL2.SetEditSize(10);
        //Formula.SetDefaultValue("0");
        fields.Add(ClrLnsL2);
        return fields;
    }

    protected override void SetChildTables()
    {
        ChildTablesClasses.AddRange(new Type[]
            { typeof(DocumentLines), typeof(DocumentLinesTwoTables), typeof(DocumentSubLines1Table) });
    }
}
