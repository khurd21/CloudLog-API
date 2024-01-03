using Asp.Versioning;
using CloudLogAPI.Exceptions;
using CloudLogAPI.Models.DynamoDB;
using CloudLogAPI.Models.Requests;
using CloudLogAPI.Models.Responses;
using CloudLogAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CloudLogAPI.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class LogbookController : ControllerBase, ILogbookAPI
{

    private ILogger<LogbookController> Logger { get; init; }

    private ILogbookService LogbookService { get; init; }

    public LogbookController(ILogger<LogbookController> logger, ILogbookService logbookService)
    {
        this.Logger = logger;
        this.LogbookService = logbookService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListJumpsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListJumps([FromQuery] ListJumpsRequest request)
    {
        // How to get email
        this.Logger.LogInformation(User.Claims.FirstOrDefault(c => c.Type == "email")?.Value);
        this.Logger.LogInformation($"{nameof(ListJumps)} called.");
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId.IsNullOrEmpty())
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found.",
                statusCode: StatusCodes.Status400BadRequest));
        }
        var loggedJumps = this.LogbookService.ListJumps(
            id: userId!,
            from: request.From,
            to: request.To);
        return await Task.FromResult(this.Ok(new ListJumpsResponse() { Jumps = loggedJumps }));
    }

    [HttpPost]
    [ProducesResponseType(typeof(LogJumpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status406NotAcceptable)]
    public async Task<IActionResult> LogJump([FromBody] LogJumpRequest request)
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId.IsNullOrEmpty() || request.Jump == null)
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found or request is empty.",
                statusCode: StatusCodes.Status400BadRequest));
        }
        request.Jump.Id = userId;
        try
        {
            this.LogbookService.LogJump(request.Jump);
        }
        catch (CloudLogException exception)
        {
            return await Task.FromResult(
                this.Problem(detail: exception.Message,
                statusCode: StatusCodes.Status406NotAcceptable));
        }
        return await Task.FromResult(
            this.Ok(new LogJumpResponse() { LoggedJump = request.Jump }));
    }

    [HttpPut]
    [ProducesResponseType(typeof(EditJumpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status406NotAcceptable)]
    public async Task<IActionResult> EditJump(EditJumpRequest request)
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId.IsNullOrEmpty() || request.Jump == null)
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found or request is empty.",
                statusCode: StatusCodes.Status400BadRequest));
        }
        request.Jump.Id = userId;
        try
        {
            this.LogbookService.EditJump(request.Jump);
        }
        catch (CloudLogException exception)
        {
            return await Task.FromResult(
                this.Problem(detail: exception.Message,
                statusCode: StatusCodes.Status406NotAcceptable));
        }
        return await Task.FromResult(this.Ok(
            new EditJumpResponse() { EditedJump = request.Jump }));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(DeleteJumpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status406NotAcceptable)]
    public async Task<IActionResult> DeleteJump(DeleteJumpRequest request)
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId.IsNullOrEmpty())
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found.",
                statusCode: StatusCodes.Status400BadRequest));
        }
        LoggedJump jump = new()
        {
            Id = userId,
            JumpNumber = request.JumpNumber,
        };
        try
        {
            this.LogbookService.DeleteJump(jump);
        }
        catch (CloudLogException exception)
        {
            return await Task.FromResult(
                this.Problem(detail: exception.Message,
                statusCode: StatusCodes.Status406NotAcceptable));
        }
        return await Task.FromResult(
            this.Ok(new DeleteJumpResponse() { DeletedJump = jump }));
    }
}