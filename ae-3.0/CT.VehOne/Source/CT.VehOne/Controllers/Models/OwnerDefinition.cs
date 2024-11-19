namespace CT.VehOne.Controllers.Api.Models;

public record Owner(string Name, string Address);
public class COwner
{
    public string Name { get; set; }
    public string Address { get; set; }
}