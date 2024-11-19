namespace ConsoleApp.DataBase.Beans.VechicleMD;

[Install]
public class VehiclePreviousOwnersTable:UDOManager
{
    
    internal const string TableNaame = "CT_VO_VMD1";
    internal const string ArchiveTableNaame = "CT_VO_AMD1";
    internal const string TableDescription = "VehiclePreviousOwners";
    internal const string ROW_TABLE_BOM_ITEMS_NAME = "CT_VO_VMD1:PreOwners";
    internal const string ROW_TABLE_BOM_ITEMS_DEFAULT_CODE = "CT_VO_VMD1_Code";
    
    public VehiclePreviousOwnersTable(ICoreConnection coreConnection, IDatabaseSetupManager databaseSetupManager) : base(coreConnection, databaseSetupManager)
    {
    }
    protected override IUDOTable CreateUDOTable()
    {
        
        IUDOTable UdoTable = new UDOTable(CreateFields(), TableNaame, TableDescription, BoUTBTableType.bott_MasterDataLines);
        UdoTable.RegisteredUDOName = ROW_TABLE_BOM_ITEMS_NAME;
        UdoTable.RegisteredUDOCode = ROW_TABLE_BOM_ITEMS_DEFAULT_CODE;

        UdoTable.CanArchive = BoYesNoEnum.tYES;
        UdoTable.ArchiveTableName = ArchiveTableNaame;

        return UdoTable;
    }

    private List<IUDOField> CreateFields()
    {
        List<IUDOField> fields = new List<IUDOField>();
        fields.CreateAlpha("OwnerName", "OwnerName", 100);
        fields.CreateAlpha("OwnerAddress", "OwnerAddress", 100);
        fields.CreateAlpha("OwnerPhone", "OwnerPhone", 20);
        fields.CreateAlpha("OwnerEmail", "OwnerEmail", 100);
        fields.CreateDate("OwnershipStartDate", "OwnershipStartDate");
        fields.CreateDate("OwnershipEndDate", "OwnershipEndDate");
        return fields;
    }
}