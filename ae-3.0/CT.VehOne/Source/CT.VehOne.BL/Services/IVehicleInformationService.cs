namespace CT.VehOne.BL.Services;


//You now can use this service to get and change vehicle information in any part of the code base
public interface IVehicleInformationService
{
    string GetVehicleName(string code);
    bool ChangeVehicleNameForVehicle(string code, string name);
    bool AddOwner(string vechicleCode, string ownerName);
    bool CreateDefaultVehicle(string code, string name);
}