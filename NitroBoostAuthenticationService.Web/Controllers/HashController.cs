using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NitroBoostAuthenticationService.Shared.Interfaces.Services;
using NitroBoostAuthenticationService.Web.Requests;

namespace NitroBoostAuthenticationService.Web.Controllers;

[ApiController]
[AutoValidateAntiforgeryToken]
[Route("api/authentication/hash")]
public class HashController : ControllerBase
{
    private ISaltService _service;

    public HashController(ISaltService service) => _service = service;
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<List<byte[]>>> Hash([FromBody] HashRequest request)
    {
        var result = await _service.Hash(request.UserId, request.Values);
        if (result == null)
            return Unauthorized();
        return Ok(result!);
    }

    [Authorize]
    [HttpPost("validate")]
    public async Task<ActionResult<List<bool>>> Validate([FromBody] ValidateRequest request)
    {
        var result = await _service.Validate(request.UserId, request.Values, request.Hashes);
        if (result == null)
            return Unauthorized();
        return Ok(result!);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult> Create([FromQuery] long userId)
    {
        await _service.AddSalt(userId);
        return Ok();
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] long userId)
    {
        await _service.DeleteSalt(userId);
        return Ok();
    }
}