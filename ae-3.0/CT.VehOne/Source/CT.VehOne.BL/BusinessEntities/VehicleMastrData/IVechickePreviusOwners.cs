using CompuTec.Core2.Attributes.AE;
using CompuTec.Core2.Beans;

namespace CT.VehOne.BL.BusinessEntities.VehicleMastrData;

[AppEngineUDOChildBean]
public interface IVechickePreviusOwners : IMasterDataChildBean<IVechickePreviusOwners>
{
    string U_OwnerName { get; set; }
    string U_OwnerAddress { get; set; }
    string U_OwnerPhone { get; set; }
    string U_OwnerEmail { get; set; }
    DateTime U_OwnershipStartDate { get; set; }
    DateTime U_OwnershipEndDate { get; set; }
}