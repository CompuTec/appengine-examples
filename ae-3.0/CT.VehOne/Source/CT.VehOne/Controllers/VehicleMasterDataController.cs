using CT.VehOne.Controllers.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace CT.VehOne.Controllers.OData;

public partial class VehicleMasterDataController
{
    [HttpPost]
    public async Task<IActionResult> AddOwner([FromODataUri] string key, ODataActionParameters  parameters)
    {
        if (!parameters.TryGetValue("owner", out var cowner) || !(cowner is COwner owner))
        {
            return BadRequest("Owner is required");
        }

        var udo = await GetEntityByKey(key);
        if (udo == null)
        {
            return NotFound();
        }

        // Set the last line and add a new row if necessary
        udo.Owners.SetCurrentLine(udo.Owners.Count - 1);
        if (udo.Owners.IsRowFilled())
        {
            udo.Owners.Add();
        }

        // Assign owner details
        udo.Owners.U_OwnerName = owner.Name;
        udo.Owners.U_OwnerAddress = owner.Address;

        // Update entity and handle result
        var result = udo.Update();
        if (result != 0)
        {
            return BadRequest("Error updating the entity");
        }

        return Ok(Serializer.ToModel(udo));
    }
}