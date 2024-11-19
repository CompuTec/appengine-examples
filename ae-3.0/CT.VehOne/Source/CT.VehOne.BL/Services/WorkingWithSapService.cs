using Microsoft.Extensions.Logging;

namespace CT.VehOne.BL.Services;

[Ioc(Scope= IocScope.Connection,ReferenceType = typeof(IWorkingWithSapService))]
internal sealed class WorkingWithSapService : IWorkingWithSapService
{
    private readonly ICoreConnection _connection;
    private readonly ILogger<WorkingWithSapService> _logger;

    public WorkingWithSapService( ICoreConnection connection,ILogger<WorkingWithSapService> logger)
    {
        _connection = connection;
        _logger = logger;
    }
    
    
    public bool CreateItemMasterData(string itemCode, string itemName, bool batchManagement)
    {
        using var measure = _logger.CreateMeasure();
        var company = _connection.Connection.GetCompany();
        var itm= company.GetBusinessObject(BoObjectTypes.oItems);
        itm.ItemCode = itemCode;
        itm.ItemName = itemName;
        itm.ManageBatchNumbers = batchManagement ? BoYesNoEnum.tYES : BoYesNoEnum.tNO;
        return itm.Add() == 0;
    }

    public Items? GetItem(string itemCode)
    {
        using var measure = _logger.CreateMeasure();
        var company = _connection.Connection.GetCompany();
        var itm= company.GetBusinessObject(BoObjectTypes.oItems);
        if (itm.GetByKey(itemCode))
        {
            return itm;
        }
        return null;
    }
}