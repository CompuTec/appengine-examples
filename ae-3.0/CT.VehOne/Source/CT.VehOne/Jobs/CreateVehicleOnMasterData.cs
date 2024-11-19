using CT.VehOne.BL.Services;

namespace CT.VehOne.Jobs;

/// <summary>
/// The logic of this job is when the item master data is created in system And created Item :
/// * ItemCode start with V
/// * ItemProperty 1 is checked
/// We create a vehicle master data with the same code and name as the item master data
/// </summary>
[EventBusJob(JobId =CreateVehicleOnMasterDataJobId,Description = CreateVehicleOnMasterDataJobName,ActionType = "A",ContentType = "4")]
internal sealed class CreateVehicleOnMasterData:EventBusSecureJob
{
    private readonly ILogger<CreateVehicleOnMasterData> _logger;
    public const string CreateVehicleOnMasterDataJobId = "CT_VEHCreateVehicleOnMasterData";
    public const string CreateVehicleOnMasterDataJobName = "Create Vehicle On Master Data";
    
    public CreateVehicleOnMasterData(IAeJobInvoker aeJobInvoker,ILogger<CreateVehicleOnMasterData> logger) : base(aeJobInvoker)
    {
        _logger = logger;
    }

    public override bool BeforeCall(QueryManager qm)
    {
        var itemCode = this.Message.Key;
        if(!itemCode.StartsWith("V"))
            return false;
        
        //now chekc the Item Property
        qm.SimpleTableName= "OITM";
        qm.SetSimpleResultFields("QryGroup1");
        qm.SetSimpleWhereFields("ItemCode");
        using var res= qm.ExecuteSimpleParameters(itemCode);
        if(res.RecordCount==0)
            return false;
        //check if the Item Property 1 is checked 
        return res.GetValue<bool>("QryGroup1");
    }

    public override async Task Call()
    {
        using var measure= _logger.CreateMeasure();
        try
        {
            var name = await GetItemName();
            var servuce = GetService<IVehicleInformationService>();
            var added=servuce.CreateDefaultVehicle(Message.Key,name);
            if(added)
                _logger.LogInformation("Vehicle Master Data for Item {ItemCode} created",this.Message.Key);
        }catch(Exception ex)
        {
            _logger.LogError(ex,"Error while creating Vehicle Master Data for Item {ItemCode}",this.Message.Key);
        }
    }

    private async Task<string> GetItemName()
    {
        var qm = CoreConnection.GetQuery();
        qm.SimpleTableName= "OITM";
        qm.SetSimpleResultFields("ItemName");
        qm.SetSimpleWhereFields("ItemCode");
        using var res=await qm.ExecuteSimpleParametersAsync(Message.Key);
        var name = res.GetValue<string>("ItemName");
        return name;
    }
}