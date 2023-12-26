using CloudLogAPI.Records;

namespace CloudLogAPI.Repositories;

public interface ILogbookService
{
    IEnumerable<LoggedJump> ListJumps(string id, int from, int to);
    void LogJump(LoggedJump jump);
    void EditJump(LoggedJump jump);
    void DeleteJump(LoggedJump jump);
}