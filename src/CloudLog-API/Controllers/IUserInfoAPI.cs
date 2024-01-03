using CloudLogAPI.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CloudLogAPI.Controllers;

public interface IUserInfoAPI
{
    Task<IActionResult> GetUserInfo();
    Task<IActionResult> SetUserInfo(UserInfoRequest userInfoRequest);
}