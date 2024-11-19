using CT.VehOne.BL.DatabaseSetup;

namespace ConsoleApp.DataBase.Beans.VechicleMD;

[Install]
public class VechicleMasterDataTable : UDOManager
{
    // Constants for table and UDO details
    internal const string TableName = "CT_VO_OVMD"; // Table name for vehicle master data
    internal const string ArchiveTableName = "CT_VO_AVMD"; // Archive table name for vehicle master data
    internal const string TableDescription = "VehicleMasterData"; // Description of the table
    internal const string UdoCode = "CT_VO_OVMD"; // Unique identifier for the User Defined Object (UDO)
    internal const string UdoName = "CT_VO_VehicleMD"; // Name of the User Defined Object (UDO)
    
    // Constructor to initialize the VehicleMasterDataTable with core connection and database setup manager
    public VechicleMasterDataTable(ICoreConnection coreConnection, IDatabaseSetupManager databaseSetupManager) : base(coreConnection, databaseSetupManager)
    {
    }

    // Method to create the UDO table with required settings and properties
    protected override IUDOTable CreateUDOTable()
    {
        var builder = new UDOTable(GetFieldList(), GetFindColumns(), TableName, TableDescription,
            BoUTBTableType.bott_MasterData, GetKeys());
        builder.RegisteredUDOName = UdoName;
        builder.RegisteredUDOCode = UdoCode;
        builder.ArchiveTableName = ArchiveTableName;
        builder.CanArchive = BoYesNoEnum.tYES; // Enable archiving for the table
        builder.CanCancel = BoYesNoEnum.tNO; // Disallow cancel operation
        builder.CanClose = BoYesNoEnum.tYES; // Allow close operation
        builder.CanCreateDefaultForm = BoYesNoEnum.tYES; // Allow creating a default form
        builder.CanDelete = BoYesNoEnum.tYES; // Allow deletion of records
        builder.CanFind = BoYesNoEnum.tYES; // Allow finding records
        builder.CanLog = BoYesNoEnum.tYES; // Enable logging
        builder.CanYearTransfer = BoYesNoEnum.tYES; // Enable year transfer
        builder .Authorizations = new AuthorizationUDO(VehicleOneAuthorization.VO_OBJECTS_ID, "", BoUPTOptions.bou_FullReadNone, new List<DocumentAuthorizationInfo>
        {
            new(TableName, TableDescription, VehicleOneAuthorization.VO_OBJECTS_ID, BoUPTOptions.bou_FullReadNone, 3, UdoCode)
        });
        return builder;
    }

    // Method to get a list of keys for the UDO table
    private List<IUDOTableKey> GetKeys()
    {
        List<IUDOTableKey> keys = new List<IUDOTableKey>();
        keys.CreateUniqueKey("VIN_Index", TableName, "VIN"); // Create a unique key on the VIN field
        return keys;
    }

    // Method to get the columns that can be used to find records in the UDO table
    private List<IUDOFindColumn> GetFindColumns()
    {
        List<IUDOFindColumn> findColumns = new List<IUDOFindColumn>();
        findColumns.CreateFindColumn("Code", "Code"); // Add Code as a find column
        findColumns.CreateFindColumn("Name", "Name"); // Add Name as a find column
        findColumns.CreateFindColumn("U_Type", "Type"); // Add Type as a find column
        findColumns.CreateFindColumn("U_Model", "Model"); // Add Model as a find column
        findColumns.CreateFindColumn("U_Brand", "Brand"); // Add Brand as a find column
        findColumns.CreateFindColumn("U_RegistrationNumber", "RegistrationNumber"); // Add RegistrationNumber as a find column
        return findColumns;
    }

    // Method to get the list of fields for the UDO table
    private List<IUDOField> GetFieldList()
    {
        List<IUDOField> fields = new List<IUDOField>();
        // Create Type field with predefined valid values
        var type = fields.CreateAlpha("Type", "Type", 3);
        type.ValidValuesMD.Add("C", "Car");
        type.ValidValuesMD.Add("B", "Bus");
        type.ValidValuesMD.Add("T", "Truck");
        type.ValidValuesMD.Add("M", "Motorcycle");
        type.ValidValuesMD.Add("O", "Other");
        type.DefaultValue="C"; // Set default value to Car
        
        // Create other fields for the vehicle master data
        fields.CreateAlpha("Model", "Model", 30); // Model field
        fields.CreateAlpha("Brand", "Brand", 30); // Brand field
        fields.CreateAlpha("Color", "Color", 30); // Color field
        fields.CreateNumeric("EnginePower", "EnginePower"); // Engine power field
        fields.CreateQuantity("EngineCapacity", "EngineCapacity"); // Engine capacity field
        fields.CreateDate("ManufacturingDate", "ManufacturingDate"); // Manufacturing date field
        fields.CreateDate("RegistrationDate", "RegistrationDate"); // Registration date field
        fields.CreateAlpha("RegistrationNumber", "RegistrationNumber", 10); // Registration number field
        fields.CreateAlpha("VIN", "VIN", 56); // Vehicle Identification Number (VIN) field
        fields.CreateAlpha("BuyBy", "BuyFromCode", 30); // Owner field
        var remarks= fields.CreateField("Remarks", "Remarks", 30); // Owner field
        remarks.SetType(BoFieldTypes.db_Memo);
        fields.CreateNumeric("InvNr", "InvoideNr"); // Owner field
        return fields;
    }

    /// <summary>
    /// Sets the child tables related to the vehicle master data
    /// </summary>
    protected override void SetChildTables()
    {
        ChildTablesClasses.AddRange(new Type[] { typeof(VehiclePreviousOwnersTable) }); // Add VehiclePreviousOwnersTable as a child table
    }
}