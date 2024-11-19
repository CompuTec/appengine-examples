using CT.VehOne.BL.Services;
using CT.VehOne.Resources;

namespace CT.VehOne.Jobs;

/// <summary>
/// This job will be scheduled to run every 1 h and will restore the vehicle master data from the items
/// </summary>
[RecurringJob(JobId = RestoreJobId, JobName = RestoreJobName, CronExpression = "0 0 0 ? * * *")]
internal sealed class RestoreVehicleMasterDataFromItems:SecureJob
{
    public const string RestoreJobId = "CT_VehOne_RestoreVehicleMasterDataFromItems";
    public const string RestoreJobName = "Restore Vehicle Master Data From Items";
        
    private readonly ILogger<RestoreVehicleMasterDataFromItems> _logger;

    public RestoreVehicleMasterDataFromItems(IAeJobInvoker aeJobInvoker,ILogger<RestoreVehicleMasterDataFromItems> logger) : base(aeJobInvoker)
    {
        _logger = logger;
    }

    public override Task Call()
    {
        var itemsCOdesToRestore= GetService<IVehicleQueries>().GetItemsToResore();
        if (!itemsCOdesToRestore.Any())
            return Task.CompletedTask;
        var service= GetService<IVehicleInformationService>();
        foreach (var (itemCode, itemName) in itemsCOdesToRestore)
        {
            try
            {
                var sucess=service.CreateDefaultVehicle(itemCode, itemName);
                if (sucess)
                {
                    _logger.LogInformation("Vehicle master data restored for item {itemCode} -{itemName}",itemCode,itemName);
                }
                else
                {
                    _logger.LogWarning("Vehicle master data not restored for item {itemCode} -{itemName}",itemCode,itemName);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while restoring vehicle master data for item {itemCode} -{itemName}",itemCode,itemName);
            }
        }
        return Task.CompletedTask;
    }
}