using CompuTec.Core2.Beans;
using CompuTec.Core2.DI.Database;
using CT.VehOne.BL.BusinessEntities.VehicleMastrData;
using CT.VehOne.BL.Enumerators;
using Microsoft.Extensions.Logging;

namespace CT.VehOne.BL.Services;

[Ioc(Scope= IocScope.Connection,ReferenceType = typeof(IVehicleInformationService))]
internal sealed class VehicleInformationService : IVehicleInformationService
{
    private readonly ICoreConnection _coreConnection;
    private readonly ILogger<VehicleInformationService> _logger;

    public VehicleInformationService(ICoreConnection coreConnection,ILogger<VehicleInformationService> logger)
    {
        _coreConnection = coreConnection;
        _logger = logger;
    }
    
    public string GetVehicleName(string code)
    {
        ///This part will create OpenTelemetry Measurment 
        using var measure = _logger.CreateMeasure();
        ///Log the calling 
        _logger.LogInformation("Getting vehicle name for code {code}",code);
        return QueryManager.ExecuteSimpleQuery<string>(_coreConnection,"@CT_VO_OVMDA","Name","Code",code);
    }

    public bool ChangeVehicleNameForVehicle(string code, string name)
    {
        using var measure = _logger.CreateMeasure();
        var udo= _coreConnection.GetService<IVehicleMasterData>();
        var sucess = udo.GetByKey(code);
        if (!sucess)
        {
            _logger.LogWarning("Vehicle with code {code} not found",code);
            return false;
        }
        udo.Name = name;
        return udo.Update()==0;
    }

    public bool AddOwner(string vechicleCode, string ownerName)
    {
        using var measure = _logger.CreateMeasure();
        var udo= _coreConnection.GetService<IVehicleMasterData>();
        var sucess = udo.GetByKey(vechicleCode);
        if (!sucess)
        {
            _logger.LogWarning("Vehicle with code {code} not found",vechicleCode);
            return false;
        }
        udo.Owners.SetCurrentLine(udo.Owners.Count-1);
        if(!udo.Owners.IsRowFilled())
            udo.Owners.Add();
        udo.Owners.U_OwnerName = ownerName;
        return udo.Update()==0;
    }

    public bool CreateDefaultVehicle(string code, string name)
    {
        using var measure = _logger.CreateMeasure();
        var udo= _coreConnection.GetService<IVehicleMasterData>();
        udo.Code = code;
        udo.Name = name;
        udo.U_VIN = Guid.NewGuid().ToString();
        udo.U_Model="Default";
        udo.U_RegistrationNumber="Default";
        udo.U_Type = VechicleType.Car;
        return udo.Add()==0;
    }
}