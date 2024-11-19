using CompuTec.Core2.Beans;
using CompuTec.Core2.Beans.Structures;
using ConsoleApp.DataBase.Beans.VechicleMD;
using Microsoft.Extensions.Logging;

namespace CT.VehOne.BL.BusinessEntities.VehicleMastrData;

[GenerateUdoBeanProperties(typeof(IVehicleMasterData))]
[Bean(TableName = VechicleMasterDataTable.TableName, UdoCode = VechicleMasterDataTable.UdoCode)]
internal sealed partial class VechicleMasterDataBean : MasterDataBean, IVehicleMasterData
{
    private readonly ITranslationService _translationService;
    public const string __ownerLines = VehiclePreviousOwnersTable.TableNaame;
    public VechicleMasterDataBean(ICoreConnection coreConnection, ILogger<VechicleMasterDataBean> logger,ITranslationService translationService) : base(coreConnection, logger)
    {
        _translationService = translationService;
        UDOCode = VechicleMasterDataTable.UdoCode;
        TableName = VechicleMasterDataTable.TableName;
        AutoCodeGeneration = false;
        AddChildDefinition<VechickePreviusOwners>(__ownerLines);
        this.RowAdded += rowAdded;
        this.RowDeleted += rowDeeted;
        this.ValueChanging += changing;
        this.ValueChanged += changed;
    }

    private void rowDeeted(UdoEventArguments e)
    {
        _logger.LogDebug("Row deleted {row}", e.ObjectName);
    }

    private void rowAdded(UdoEventArguments e)
    {
        _logger.LogDebug("Row added {row}", e.ObjectName);
    }

    private void changed(object sender, PropertyChangedEventArgs eventargs)
    {
        _logger.LogDebug("Value changed {property}", eventargs.PropertyName);
    }

    private void changing(object sender, PropertyChangingEventArgs eventargs, out bool canel)
    {
        canel = false;
        _logger.LogDebug("Value changing {property}", eventargs.PropertyName);
    }

    public override bool CanDelete()
    {
        throw new CanootRemoveVehicleMasterDaataException();
        return false;
    }

    public IVechickePreviusOwners Owners
    {
        get=>GetChild<IVechickePreviusOwners>(__ownerLines);
    }


    protected override bool BeforeAdd()
    {
        ValidateCode();
        Validate();
        return true;
    }

    private void ValidateCode()
    {
        if(string.IsNullOrEmpty(Code))
            throw new Exception(_translationService.GetTranslatedMessage("VehOne.CodeIsMissing"));
    }

    protected override bool BeforeUpdate()
    {
        Validate();
        return true;
    }

    private void Validate()
    {
        
        ValidateViN();
        ValidateRegistrationNumber();
        ValidateModelAndBrand();
        ValiddateDuplicatedOwnerNames();
    }

    private void ValiddateDuplicatedOwnerNames()
    {
        var invalid=Owners.Where(p=>p.IsRowFilled()).GroupBy(p=>p.U_OwnerName.ToUpper()).Any(P=>P.Count()>1);
        if(invalid)
            throw new Exception(_translationService.GetTranslatedMessage("VehOne.DuplicatedPrevOwners"));
    }
    private void ValidateViN()
    {
        if(string.IsNullOrEmpty(U_VIN))
            throw new Exception(_translationService.GetTranslatedMessage("VehOne.VinIsMissing"));
    }
    private void ValidateRegistrationNumber()
    {
        if(string.IsNullOrEmpty(U_RegistrationNumber))
            throw new Exception("Registration number is required");
    }
    private void ValidateModelAndBrand()
    {
        if(string.IsNullOrEmpty(U_Model))
            throw new Exception(_translationService.GetTranslatedMessage("VehOne.ModelIsMissing"));
    }

    
}