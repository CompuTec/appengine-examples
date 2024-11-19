using CompuTec.BaseLayer;
using CompuTec.Core2.DI.Database;

namespace CT.VehOne.UI.Services;

internal interface IVehicleMasterDataUiTools
{
    string GetSellerInvoiceNr(int docEntry);
    string GetSellerName(string cardCode);
}

[Ioc(Scope = IocScope.Connection,ReferenceType = typeof(IVehicleMasterDataUiTools))]
internal sealed class VehicleMasterDataTool:IVehicleMasterDataUiTools
{
    private readonly ILogger<VehicleMasterDataTool> _logger;
    private readonly ICoreConnection _connection;

    public VehicleMasterDataTool(ILogger<VehicleMasterDataTool> logger,ICoreConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }
    
    public string GetSellerInvoiceNr(int docEntry)
    {
        _logger.LogInformation("GetSellerInvoiceNr");
        using var measure=_logger.CreateMeasure();
        var query = _connection.GetQuery();
        query.SimpleTableName= "OINV";
        query.SetSimpleResultFields("DocNum");
        query.SetSimpleWhereFields("DocEntry");
        using var result=query.ExecuteSimpleParameters(docEntry);
        if (result.RecordCount == 0)
            return "";
        return result.GetValue<int>(0).ToString();
    }

    public string GetSellerName(string cardCode)
    {
        _logger.LogInformation("GetSellerName");
        using var measure=_logger.CreateMeasure();
        var query = _connection.GetQuery();
        query.SimpleTableName= "OCRD";
        query.SetSimpleResultFields("CardName");
        query.SetSimpleWhereFields("CardCode");
        using var result=query.ExecuteSimpleParameters(cardCode);
        if (result.RecordCount == 0)
            return "";
        return result.GetValue<string>(0);
    }
}