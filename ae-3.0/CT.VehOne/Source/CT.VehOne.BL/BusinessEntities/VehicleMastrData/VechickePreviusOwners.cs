using CompuTec.Core2.Beans;

namespace CT.VehOne.BL.BusinessEntities.VehicleMastrData;

[GenerateUdoBeanProperties(typeof(IVechickePreviusOwners))]
internal sealed  partial class VechickePreviusOwners : MasterDataChildBean<IVechickePreviusOwners>,
    IVechickePreviusOwners
{
    public VechickePreviusOwners(IUDOBean BaseUdo, bool master = false) : base(BaseUdo, master)
    {
        
    }
 
    public override bool IsRowFilled()
    {
        //this method is used to check if the row is filled if is filled will be saved in the database
        //in this Example we check if the OwnerName is filled
        return !string.IsNullOrEmpty(U_OwnerName);
    }
}