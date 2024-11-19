
using CT.VehOne.Controllers.Api.Models;
using CT.VehOne.Models.VehicleMastrData;
using Microsoft.AspNetCore.Mvc;

namespace CT.VehOne.Controllers.Api;

 partial class VehicleMasterDataApiController
{
    [HttpPost]
    [Route("AddOwner/{key}")]

    public async Task<ActionResult<VehicleMasterData>> AddOwner(string key, [FromBody] Owner vehicleMasterData)
    {
        var udo = await GetEntityByKeyAsync(key);
        //set last line 
        udo.Owners.SetCurrentLine(udo.Owners.Count - 1);
        if (udo.Owners.IsRowFilled())
            udo.Owners.Add();
        udo.Owners.U_OwnerName = vehicleMasterData.Name;
        udo.Owners.U_OwnerAddress = vehicleMasterData.Address;
        var resutl = udo.Update();
        if (resutl != 0)
            return BadRequest("Error");
        else
            return Ok(Serializer.ToModel(udo));
    }
}