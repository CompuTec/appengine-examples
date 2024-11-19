using CT.VehOne.Controllers.Api.Models;
using CT.VehOne.Models.VehicleMastrData;
using Microsoft.OData.ModelBuilder;

namespace CT.VehOne;

public class Info : AEPlugin
{
    public Info()
    {
        PluginCode = "CT.VehOne";
        PluginName = "CT.VehOne Name";
        //Register the configuration to the plugin 
        //this configuration is managaed in adminstration Panel on the company level    
        SetConfiguration<VehicleWebPluginConfig>();
    }

    public override void BuildCustomEdmModel(ODataConventionModelBuilder builder)
    {
        //Register the entity action
        var action = builder.EntityType<VehicleMasterData>().Action("AddOwner");
        action.Parameter<Owner>("owner");
        action.ReturnsFromEntitySet<VehicleMasterData>("VehicleMasterData");
    }
}