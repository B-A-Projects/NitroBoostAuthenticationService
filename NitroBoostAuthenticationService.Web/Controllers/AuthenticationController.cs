using System.Net.Security;
using Microsoft.AspNetCore.Mvc;

namespace NitroBoostAuthenticationService.Web.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ContentController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Index() => Ok("Hello world from the authentication service!");

    [HttpGet("hello")]
    public ActionResult<string> Hello([FromQuery] string name) =>
        Ok($"Hello {name}, your request has been received by the authentication service!");
}