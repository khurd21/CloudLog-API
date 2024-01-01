using Asp.Versioning;
using CloudLogAPI.Entities;
using CloudLogAPI.Exceptions;
using CloudLogAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CloudLogAPI.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class UserInfoController : ControllerBase, IDefaultInfoAPI, IUserInfoAPI
{
    private IUserInfoService UserInfoService { get; init; }

    public UserInfoController(IUserInfoService userInfoService)
    {
        this.UserInfoService = userInfoService;
    }

    [HttpGet("defaultInfo")]
    [ProducesResponseType(typeof(DefaultInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDefaultInfo()
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId == null || userId.IsNullOrEmpty())
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found.",
                statusCode: StatusCodes.Status404NotFound));
        }

        try
        {
            var defaultInfo = this.UserInfoService.GetDefaultInfo(userId);
            return await Task.FromResult(
                this.Ok(new DefaultInfoResponse() { DefaultInfo = defaultInfo }));
        }
        catch (CloudLogException)
        {
            return await Task.FromResult(
                this.Ok(new UserInfoResponse() { UserInfo = new() }));
        }
    }

    [HttpGet("userInfo")]
    [ProducesResponseType(typeof(UserInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserInfo()
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId == null || userId.IsNullOrEmpty())
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found.",
                statusCode: StatusCodes.Status404NotFound));
        }

        try
        {
            var userInfo = this.UserInfoService.GetUserInfo(userId);
            return await Task.FromResult(
                this.Ok(new UserInfoResponse() { UserInfo = userInfo }));
        }
        catch (CloudLogException)
        {
            return await Task.FromResult(
                this.Ok(new UserInfoResponse() { UserInfo = new() }));
        }
    }

    [HttpPost("defaultInfo")]
    [ProducesResponseType(typeof(DefaultInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetDefaultInfo(DefaultInfoRequest defaultInfoRequest)
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId == null || userId.IsNullOrEmpty())
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found.",
                statusCode: StatusCodes.Status404NotFound));
        }
        if (defaultInfoRequest.DefaultInfo == null)
        {
            return await Task.FromResult(
                this.Problem(detail: "No default info was provided.",
                statusCode: StatusCodes.Status404NotFound));
        }

        try
        {
            defaultInfoRequest.DefaultInfo.Id = userId;
            this.UserInfoService.SetDefaultInfo(defaultInfoRequest.DefaultInfo);
            return await Task.FromResult(
                this.Ok(new DefaultInfoResponse() { DefaultInfo = defaultInfoRequest.DefaultInfo }));
        }
        catch (CloudLogException exception)
        {
            return await Task.FromResult(
                this.Problem(detail: exception.Message,
                statusCode: StatusCodes.Status404NotFound));
        }
    }

    [HttpPost("userInfo")]
    [ProducesResponseType(typeof(UserInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetUserInfo(UserInfoRequest userInfoRequest)
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        if (userId == null || userId.IsNullOrEmpty())
        {
            return await Task.FromResult(
                this.Problem(detail: "User ID not found.",
                statusCode: StatusCodes.Status404NotFound));
        }
        if (userInfoRequest.UserInfo == null)
        {
            return await Task.FromResult(
                this.Problem(detail: "No user info was provided.",
                statusCode: StatusCodes.Status404NotFound));
        }

        try
        {
            userInfoRequest.UserInfo.Id = userId;
            this.UserInfoService.SetUserInfo(userInfoRequest.UserInfo);
            return await Task.FromResult(
                this.Ok(new UserInfoResponse() { UserInfo = userInfoRequest.UserInfo }));
        }
        catch (CloudLogException exception)
        {
            return await Task.FromResult(
                this.Problem(detail: exception.Message,
                statusCode: StatusCodes.Status404NotFound));
        }
    }
}