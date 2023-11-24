using Asp.Versioning;
using CloudLogAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudLogAPI.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:ApiVersion}/logbook")]
public sealed class LogbookController : ControllerBase, ILogbookAPI
{

    private ILogger<LogbookController> Logger { get; init; }

    public LogbookController(ILogger<LogbookController> logger) {
        this.Logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListJumpsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListJumps([FromQuery] ListJumpsRequest request)
    {
        return await Task.FromResult(this.Ok(new ListJumpsResponse() {}));
    }

    [HttpPost]
    [ProducesResponseType(typeof(LogJumpResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> LogJump([FromBody] LogJumpRequest request)
    {
        return await Task.FromResult(this.Ok(new LogJumpResponse() {}));
    }

    [HttpPut]
    [ProducesResponseType(typeof(EditJumpResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> EditJump(EditJumpRequest request)
    {
        return await Task.FromResult(this.Ok(new EditJumpResponse() {}));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteJump(DeleteJumpRequest request)
    {
        return await Task.FromResult(this.Ok(new DeleteJumpResponse() {}));
    }
}