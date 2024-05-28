using System.Data;
using System.Net.Security;
using Microsoft.AspNetCore.Mvc;
using NitroBoostAuthenticationService.Shared;
using NitroBoostAuthenticationService.Shared.Configurations;
using NitroBoostAuthenticationService.Shared.Interfaces.Services;

namespace NitroBoostAuthenticationService.Web.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AuthenticationController : ControllerBase
{
    private IAuthenticationService _service;

    public AuthenticationController(IAuthenticationService service) => _service = service;
    
    [HttpGet]
    public ActionResult<string> Index() => Ok("Hello world from the authentication service!");

    [HttpGet("hello")]
    public ActionResult<string> Hello([FromQuery] string name) =>
        Ok($"Hello {name}, your request has been received by the authentication service!");

    [HttpPost("login")]
    public async Task<ActionResult<string?>> Login([FromBody] CredentialsDto credentials)
    {
        try
        {
            string? token = await _service.Authenticate(credentials.Username, credentials.Password);
            if (token == null)
                return Unauthorized();
            
            HttpContext.Response.Headers.Append("access_token", token);
            HttpContext.Response.Headers.Append("token_type", "bearer");
            return Ok();
        }
        catch (Exception e)
        {
            return Unauthorized();
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<AccountDto?>> CreateAccount([FromBody] CredentialsDto credentials)
    {
        try
        {
            AccountDto? result = await _service.CreateAccount(credentials.Username, credentials.Password);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}