using CloudLogAPI.Records;

namespace CloudLogAPI.Repositories;

public interface ILogbookService
{
    IEnumerable<LoggedJump> ListJumps(string id, int from, int to);
    LoggedJump GetJump(string id, int jumpNumber);
    void LogJump(LoggedJump jump);
    void EditJump(LoggedJump jump);
    void DeleteJump(LoggedJump jump);
}