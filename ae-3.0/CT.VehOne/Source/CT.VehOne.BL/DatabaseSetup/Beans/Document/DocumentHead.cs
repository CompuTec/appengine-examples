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
        var builder = UDOTable.CreateDocumentBuilder()
            .WithSeriesManagement(BoYesNoEnum.tYES)
            .WithRegisteredUdo(OBJECT_CODE, HEADER_TABLE_NAME)
            .WithTable(HEADER_TABLE_NAME, HEADER_TABLE_DESCRIPTION)
            .WithArchiveTable("CT_VO_ADOC")
            .WithPermissions(BoYesNoEnum.tYES, BoYesNoEnum.tYES, BoYesNoEnum.tYES,
                canDelete: BoYesNoEnum.tYES, canFind: BoYesNoEnum.tYES, canLog: BoYesNoEnum.tYES)
            .AddField("ItemCode", "Item Code").AsAlpha(50).AsPrimaryKey().AddToFindAndForm().Add()
            .AddField("Browser", "Field to browse BOMs").AsAlpha(70).AddToFindAndForm().Add()
            .AddField("Description", "Item Description").AsAlpha(200).AddToFindAndForm().Add()
            .AddField("InventoryUoM", "Unit of Measure").AsAlpha(20).AddToFindAndForm().Add()
            .AddField("Revision", "Revision").AsAlpha(20).AsPrimaryKey().AddToFindAndForm().Add()
            .AddField("Quantity", "Quantity").AsQuantity().AddToFindAndForm().Add()
            .AddField("Factor", "Factor").AsQuantity().AddToFindAndForm().Add()
            .AddField("WhsCode", "Warehouse Code").AsAlpha(8).AddToFindAndForm().Add()
            .AddField("OccrCode", "Distribution Rule").AsAlpha(8).AddToFindAndForm().Add()
            .AddField("Price", "Price").AsPrice().AddToFindAndForm().Add()
            .AddField("Currency", "Currency").AsMemo().AddToFindAndForm().Add()
            .AddField("Remarks", "Remarks").AsMemo().AddToFindAndForm().Add()
            .AddField("OcrCode", "Costing Code 1").AsAlpha(8).AddToFindAndForm().Add()
            .AddField("OcrCode2", "Costing Code 2").AsAlpha(8).AddToFindAndForm().Add()
            .AddField("OcrCode3", "Costing Code 3").AsAlpha(8).AddToFindAndForm().Add()
            .AddField("OcrCode4", "Costing Code 4").AsAlpha(8).AddToFindAndForm().Add()
            .AddField("OcrCode5", "Costing Code 5").AsAlpha(8).AddToFindAndForm().Add()
            .AddField("Project", "Project").AsAlpha(20).AddToFindAndForm().Add()
            .AddField("PriceList", "Price List").AsNumeric().AddToFindAndForm().Add()
            .AddField("BatchSize", "Batch Size").AsQuantity().WithDefaultValue("1").Add()
            .AddField("ProdType", "Production Type").AsAlpha(1).WithDefaultValue("I")
                .WithValidValues(new Dictionary<string, string>
                {
                    ["I"] = _translationService.GetTranslatedMessage("ProdTypeFieldI", "internal"),
                    ["E"] = _translationService.GetTranslatedMessage("ProdTypeFieldE", "external")
                }).Add()
            .AddField("Yield", "Yield Percentage").AsFloat().WithSubType(BoFldSubTypes.st_Percentage).WithDefaultValue("100").Add()
            .AddField("YieldFormula", "Yield Formula").AsMemo().Add()
            .AddField("ItemFormula", "Item Formula").AsMemo().Add()
            .AddField("ScrapFormula", "Scrap Formula").AsMemo().Add()
            .AddField("CoproductFormula", "Coproduct Formula").AsMemo().Add()
            .AddField("Instructions", "Instructions").AsMemo().Add()
            .AddField("ClrLns", "ClrLns").AsAlpha(10).Add()
            .AddField("ClrLnsL1", "ClrLnsL1").AsAlpha(10).Add()
            .AddField("ClrLnsL2", "ClrLnsL2").AsAlpha(10).Add();

        return builder.Build();
    }

    protected override void SetChildTables()
    {
        ChildTablesClasses.AddRange(new Type[]
            { typeof(DocumentLines), typeof(DocumentLinesTwoTables), typeof(DocumentSubLines1Table) });
    }
}
