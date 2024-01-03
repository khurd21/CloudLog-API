using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Services;

public interface ILogbookService
{
    IEnumerable<LoggedJump> ListJumps(string id, int from, int to);
    void LogJump(LoggedJump jump);
    void EditJump(LoggedJump jump);
    void DeleteJump(LoggedJump jump);
}