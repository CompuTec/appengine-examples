using CompuTec.Core2.AE.Controllers.API;
using Microsoft.AspNetCore.Mvc;

namespace CT.VehOne.Controllers;

public class MyOwnControler:AppEngineSecureController
{
    [HttpGet]
    public string Get()
    {
        return "Hello World!";
    }
}