 

namespace CT.VehOne.BL.DatabaseSetup.Beans.MasterData;

// [Install]
public class MasterDataHead : UDOManager
{
    public const String OBJECT_CODE = "CT_VO_MDCode";
    public const String HEADER_TABLE_NAME = "CT_VO_OMDA";
    public const String HEADER_TABLE_DESCRIPTION = "VO_TestMasterData";
    private readonly ITranslationService _translationService;

    public MasterDataHead(ICoreConnection connection, ITranslationService translationService,
        IDatabaseSetupManager manager) : base(connection, manager)
    {
        _translationService = translationService;
    }


    protected override IUDOTable CreateUDOTable()
    {
        List<DocumentAuthorizationInfo> authorizationList = new List<DocumentAuthorizationInfo>();

        var builder = UDOTable.CreateMasterDataBuilder()
            .WithTable(HEADER_TABLE_NAME, HEADER_TABLE_DESCRIPTION)
            .WithRegisteredUdo(OBJECT_CODE, HEADER_TABLE_NAME)
            .WithArchiveTable("CT_VO_AMDA")
            .WithPermissions(
                canArchive: BoYesNoEnum.tYES,
                canCancel: BoYesNoEnum.tNO,
                canClose: BoYesNoEnum.tYES,
                canCreateDefaultForm: BoYesNoEnum.tYES,
                canDelete: BoYesNoEnum.tYES,
                canFind: BoYesNoEnum.tYES,
                canLog: BoYesNoEnum.tYES,
                canYearTransfer: BoYesNoEnum.tYES)
            .WithAuthorizations(new AuthorizationUDO(VehicleOneAuthorization.VO_OBJECTS_ID, VehicleOneAuthorization.VO_OBJECS_NAme,
                BoUPTOptions.bou_FullReadNone, authorizationList))
            .AddField("ItemCode", "Item Code").AsAlpha(50).AddToFindAndForm().Add()
            .AddField("Browser", "Field to browse BOMs").AsAlpha(70).AddToFindAndForm().Add()
            .AddField("Description", "Item Description").AsAlpha(200).AddToFindAndForm().Add()
            .AddField("InventoryUoM", "Unit of Measure").AsAlpha(20).AddToFindAndForm().Add()
            .AddField("Revision", "Revision").AsAlpha(20).AddToFindAndForm().Add()
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
            .AddField("Yield", "Yield Percentage").AsPercentage().WithDefaultValue("100").Add()
            .AddField("YieldFormula", "Yield Formula").AsMemo().Add()
            .AddField("ItemFormula", "Item Formula").AsMemo().Add()
            .AddField("ScrapFormula", "Scrap Formula").AsMemo().Add()
            .AddField("CoproductFormula", "Coproduct Formula").AsMemo().Add()
            .AddField("Instructions", "Instructions").AsMemo().Add()
            .AddUniqueKey("Index","ItemCode", "Revision");

        return builder.Build();
    }

    protected override void SetChildTables()
    {
        ChildTablesClasses.AddRange(new Type[] { typeof(MasterDataLinesTable) });
    }
}
