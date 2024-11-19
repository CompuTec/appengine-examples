using CT.VehOne.Services;

namespace CT.VehOne.Jobs;

[AdditionalJobConfiguration(Type = typeof(MyJobSettings))]
[EventBusJob(JobId =CreateVehicleOnMasterDataJobId,Description = CreateVehicleOnMasterDataJobName,ActionType = "*",ContentType = "CT_VO_OVMD")]
internal sealed class SendVehicleChangeNotification:EventBusSecureJob
{
    private readonly ILogger<SendVehicleChangeNotification> _logger;
    public const string CreateVehicleOnMasterDataJobId = "CT_VO_NotifyUserOnVehicleChange";
    public const string CreateVehicleOnMasterDataJobName = "Notify User On Vehicle Change";
    
    public SendVehicleChangeNotification(IAeJobInvoker aeJobInvoker ,ILogger<SendVehicleChangeNotification> logger) : base(aeJobInvoker)
    {
        _logger = logger;
    }

    public override Task Call()
    {
        var config = GetService<VehicleWebPluginConfig>();
        var jobConfig = Configuration as MyJobSettings;
        config.UsersToNotify=["manager"];
        var add = Message.ActionType == "A";
        var change = Message.ActionType == "U";
        if(config.NotifyUserOnVehicleCreated && add)
        {
            NotifyUserAdd(config);
        }
        if(config.NotifyUserOnVehicleChange && change)
        {
            NotifyUserChange(config);
        }
        return Task.CompletedTask;
    }

    private void NotifyUserChange(VehicleWebPluginConfig config)
    {
        GetService<ISendAlertService>().SendMessage($"Vehicle  Changed", $"Vehicle with code {Message.Key} has been changed", config.UsersToNotify?.ToArray());
    }

    private void NotifyUserAdd(VehicleWebPluginConfig config)
    {
        GetService<ISendAlertService>().SendMessage($"Vehicle  Added", $"Vehicle with code {Message.Key} has been added", config.UsersToNotify?.ToArray());
    }
}