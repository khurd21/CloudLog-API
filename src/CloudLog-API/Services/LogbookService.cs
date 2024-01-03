using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using CloudLogAPI.Exceptions;
using CloudLogAPI.Models.DynamoDB;

namespace CloudLogAPI.Services;

public class LogbookService : ILogbookService
{
    private ILogger<ILogbookService> Logger { get; init; }

    private IDynamoDBContext DynamoDBContext { get; init; }

    public LogbookService(ILogger<ILogbookService> logger, IDynamoDBContext dynamoDBContext)
    {
        this.Logger = logger;
        this.DynamoDBContext = dynamoDBContext;
    }

    public void DeleteJump(LoggedJump jump)
    {
        if (jump.Id == null)
        {
            throw new CloudLogException("Identifier for jumper is required.");
        }
        if (jump.JumpNumber.HasValue && !this.VerifyJumpExists(jump.Id, jump.JumpNumber.Value))
        {
            throw new CloudLogException($"Jump {jump.JumpNumber} does not already exists.");
        }
        this.DynamoDBContext
            .DeleteAsync(jump)
            .Wait();
    }

    public void EditJump(LoggedJump jump)
    {
        if (jump.Id == null)
        {
            throw new CloudLogException("Identifier for jumper is required.");
        }
        if (jump.JumpNumber.HasValue && !this.VerifyJumpExists(jump.Id, jump.JumpNumber.Value))
        {
            throw new CloudLogException($"Jump {jump.JumpNumber} does not already exists.");
        }

        this.DynamoDBContext
            .SaveAsync(jump)
            .Wait();
    }

    public IEnumerable<LoggedJump> ListJumps(string id, int from, int to)
    {
        return this.DynamoDBContext
            .QueryAsync<LoggedJump>(
                id,
                QueryOperator.Between,
                new object[] { from, to })
            .GetRemainingAsync().Result
            .OrderBy(jump => jump.JumpNumber);
    }

    public void LogJump(LoggedJump jump)
    {
        if (jump.Id == null)
        {
            throw new CloudLogException("Identifier for jumper is required.");
        }
        if (jump.JumpNumber.HasValue && this.VerifyJumpExists(jump.Id, jump.JumpNumber.Value))
        {
            throw new CloudLogException($"Jump {jump.JumpNumber} already exists.");
        }
        this.DynamoDBContext
            .SaveAsync<LoggedJump>(jump)
            .Wait();
    }

    private bool VerifyJumpExists(string id, int jumpNumber)
    {
        return this.DynamoDBContext.LoadAsync<LoggedJump>(id, jumpNumber).Result != null;
    }
}