namespace CT.VehOne;

public class VehicleWebPluginConfig
{
    public bool NotifyUserOnVehicleChange { get; set; }
    public bool NotifyUserOnVehicleCreated { get; set; }
    public List<string> UsersToNotify { get; set; }
}