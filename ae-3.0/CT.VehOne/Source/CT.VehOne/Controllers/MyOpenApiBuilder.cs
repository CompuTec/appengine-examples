using CompuTec.Core2.AE.Controllers.MinialAPI;
using CompuTec.Core2.Translations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CT.VehOne.Controllers;

public class MyOpenApiBuilder : AeSecureMinimalApiEndpointBuilder
{
    public override string Route => "MyApiRoute";

    public override void Configure(RouteGroupBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/Test", Test);
    }

    private IResult Test([FromServices] SecureScopeService<ITranslationService> translationService)
        =>Results.Ok(translationService.Value.GetTranslatedMessage("VehOne.VinIsMissing"));

}