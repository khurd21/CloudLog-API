using CloudLogAPI.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CloudLogAPI.Controllers;

public interface IDefaultInfoAPI
{
    Task<IActionResult> GetDefaultInfo();
    Task<IActionResult> SetDefaultInfo(DefaultInfoRequest defaultInfoRequest);
}