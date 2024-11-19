using System.ComponentModel;
using CompuTec.Core2.Attributes.AE;
using CompuTec.Core2.Beans;
using ConsoleApp.DataBase.Beans.VechicleMD;
using CT.VehOne.BL.Enumerators;

namespace CT.VehOne.BL.BusinessEntities.VehicleMastrData;

[AppEngineUDOBean(TableName = VechicleMasterDataTable.TableName,ObjectCode = VechicleMasterDataTable.UdoCode)]
public interface IVehicleMasterData : IMasterDataBean
{
    [DefaultValue(VechicleType.Car)]
    VechicleType U_Type { get; set; }
    string U_Model { get; set; }
    string U_Color { get; set; }
    int U_EnginePower { get; set; }
    double U_EngineCapacity { get; set; }
    DateTime U_ManufacturingDate { get; set; }
    DateTime U_RegistrationDate { get; set; }
    string U_VIN { get; set; }
    string U_RegistrationNumber { get; set; }
    public IVechickePreviusOwners Owners { get; }
    public string U_BuyBy { get; set; }
    public int U_InvNr { get; set; }
}