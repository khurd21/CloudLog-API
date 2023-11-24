using CloudLogAPI.Records;

namespace CloudLogAPI.Repositories;

public class LogbookService : ILogbookService
{
    private ILogger<ILogbookService> Logger;

    public LogbookService(ILogger<ILogbookService> logger)
    {
        this.Logger = logger;
    }

    public void DeleteJump(LoggedJump jump)
    {
        this.Logger.LogInformation($"{nameof(this.DeleteJump)} called.");
    }

    public void EditJump(LoggedJump jump)
    {
        this.Logger.LogInformation($"{nameof(this.EditJump)} called.");
    }

    public IEnumerable<LoggedJump> ListJumps(string id, int from, int to)
    {
        this.Logger.LogInformation($"{nameof(this.ListJumps)} called.");
        return new List<LoggedJump>() {};
    }

    public void LogJump(LoggedJump jump)
    {
        this.Logger.LogInformation($"{nameof(this.LogJump)} called.");
    }
}