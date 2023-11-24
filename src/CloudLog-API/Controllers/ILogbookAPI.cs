using CloudLogAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CloudLogAPI.Controllers;

public interface ILogbookAPI
{
    Task<IActionResult> ListJumps(ListJumpsRequest request);
    Task<IActionResult> LogJump(LogJumpRequest request);
    Task<IActionResult> EditJump(EditJumpRequest request);
    Task<IActionResult> DeleteJump(DeleteJumpRequest request);
}