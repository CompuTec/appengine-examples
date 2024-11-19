namespace CT.VehOne.BL.DatabaseSetup;

[Install]
public class VehicleOneAuthorization : AuthorizationInfo
{


    public const string MAIN_PERMISSION_ID = "CT_VO_VehicleOne";
    public const string MAIN_PERMISSION_NAME = "VehicleOne";
    public const string VO_OBJECTS_ID = "CT_VO_Objects";
    public const string VO_OBJECS_NAme = "Objects";
    public const string VO_Other_ID = "CT_VO_Other";
    public const string VO_Other_Name = "Other";
    public const string AllowDeleteVehicleMasterData_ID = "CT_VO_AllowDelVMD";
    public const string AllowDeleteVehicleMasterData_NAME = "Allow Remove Vehicle";


    public VehicleOneAuthorization(ICoreConnection connection) : base(connection)
    {
        //
        // Authorisations for forms without UDO object example
        //
        this.PermissionID = MAIN_PERMISSION_ID;
        this.Name = MAIN_PERMISSION_NAME;
        this.Option = BoUPTOptions.bou_FullReadNone;
        this.Level = 1;
        this.DocumentAuthorizations = new List<CompuTec.Core2.DI.Setup.UDO.Model.DocumentAuthorizationInfo>();
        //Main Authorization
        this.DocumentAuthorizations.Add(new CompuTec.Core2.DI.Setup.UDO.Model.DocumentAuthorizationInfo(
            MAIN_PERMISSION_ID, MAIN_PERMISSION_NAME, "",
            BoUPTOptions.bou_FullReadNone, 1));
        this.DocumentAuthorizations.Add(new CompuTec.Core2.DI.Setup.UDO.Model.DocumentAuthorizationInfo(
            VO_OBJECTS_ID, VO_OBJECS_NAme, MAIN_PERMISSION_ID,
            BoUPTOptions.bou_FullReadNone, 2));
        this.DocumentAuthorizations.Add(new CompuTec.Core2.DI.Setup.UDO.Model.DocumentAuthorizationInfo(
            VO_Other_ID, VO_Other_Name, MAIN_PERMISSION_ID,
            BoUPTOptions.bou_FullReadNone, 2));
        //Add cusotm authorization that allow user to delete Vehicle Master Data
        this.DocumentAuthorizations.Add(new CompuTec.Core2.DI.Setup.UDO.Model.DocumentAuthorizationInfo(
            AllowDeleteVehicleMasterData_ID, AllowDeleteVehicleMasterData_NAME, VO_Other_ID,
            BoUPTOptions.bou_FullNone, 3));
    }
}

