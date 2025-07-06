using Ecom.API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers;

[Route("errors/{statusCode}")]
[ApiController]
public class ErrorController : ControllerBase
{
    // GET
    [HttpGet]
    public IActionResult Error(int statusCode)
    {
        return new ObjectResult(new ResponseAPI(statusCode));
    }
}