using Asp.Versioning;
using CloudLogAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudLogAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:ApiVersion}/logbook")]
public sealed class LogbookController : ControllerBase
{

    private ILogger<LogbookController> Logger { get; init; }

    public LogbookController(ILogger<LogbookController> logger) {
        this.Logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> ListJumps([FromQuery] ListJumpsRequest request)
    {
        return await Task.FromResult(this.Ok(new ListJumpsResponse()));
    }

    [HttpPost]
    [ProducesResponseType(typeof(LogJumpRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> LogJump([FromBody] LogJumpRequest request)
    {
        return await Task.FromResult(this.Ok(new LogJumpResponse() {}));
    }

}