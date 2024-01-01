using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CloudLogAPI.Records;

namespace CloudLogAPI.Repositories;

public class DynamoDBTableManager : IDynamoDBTableManager
{
    private AmazonDynamoDBClient Client;

    private ILogger<IDynamoDBTableManager> Logger;

    public DynamoDBTableManager(AmazonDynamoDBClient amazonDynamoDBClient, ILogger<IDynamoDBTableManager> logger)
    {
        this.Client = amazonDynamoDBClient;
        this.Logger = logger;
    }

    public IEnumerable<CreateTableResponse> CreateTables()
    {
        Task<CreateTableResponse>[] createTableTasks = new Task<CreateTableResponse>[]
        {
            this.CreateUserInfoTableAsync(),
            this.CreateLoggedJumpTableAsync(),
        };
        Task task = Task.WhenAll(createTableTasks);
        task.Wait();
        this.AssertAndLogTaskResult(nameof(this.CreateTables), task);
        return this.ConvertTaskToEnumerable(createTableTasks);
    }

    public IEnumerable<DeleteTableResponse> DeleteTables()
    {
        ListTablesResponse listTablesResponse = this.Client.ListTablesAsync().Result;
        IEnumerable<string> tableNames = listTablesResponse.TableNames;
        Task<DeleteTableResponse>[] deleteTableTasks = tableNames.Select(tableName => this.Client.DeleteTableAsync(tableName)).ToArray();
        Task task = Task.WhenAll(deleteTableTasks);
        task.Wait();
        this.AssertAndLogTaskResult(nameof(this.DeleteTables), task);
        return this.ConvertTaskToEnumerable(deleteTableTasks);
    }

    private async Task<CreateTableResponse> CreateUserInfoTableAsync()
    {
        List<AttributeDefinition> attributeDefinitions = new()
        {
            new()
            {
                AttributeName = nameof(UserInfo.Id),
                AttributeType = ScalarAttributeType.S,
            },
        };
        List<KeySchemaElement> keySchemaElements = new()
        {
            new()
            {
                AttributeName = nameof(UserInfo.Id),
                KeyType = KeyType.HASH,
            },
        };
        ProvisionedThroughput provisionedThroughput = new()
        {
            ReadCapacityUnits = 5,
            WriteCapacityUnits = 6,
        };

        CreateTableRequest request = new()
        {
            TableName = nameof(UserInfo),
            AttributeDefinitions = attributeDefinitions,
            KeySchema = keySchemaElements,
            ProvisionedThroughput = provisionedThroughput,
        };
        return await this.CreateAndLogTable(request);
    }

    private async Task<CreateTableResponse> CreateLoggedJumpTableAsync()
    {
        List<AttributeDefinition> attributeDefinitions = new()
        {
            new()
            {
                AttributeName = nameof(LoggedJump.Id),
                AttributeType = ScalarAttributeType.S,
            },
            new()
            {
                AttributeName = nameof(LoggedJump.JumpNumber),
                AttributeType = ScalarAttributeType.N,
            },
        };
        List<KeySchemaElement> keySchemaElements = new()
        {
            new()
            {
                AttributeName = nameof(LoggedJump.Id),
                KeyType = KeyType.HASH,
            },
            new()
            {
                AttributeName = nameof(LoggedJump.JumpNumber),
                KeyType = KeyType.RANGE,
            }
        };
        ProvisionedThroughput provisionedThroughput = new()
        {
            ReadCapacityUnits = 5,
            WriteCapacityUnits = 6,
        };

        CreateTableRequest request = new()
        {
            TableName = nameof(LoggedJump),
            AttributeDefinitions = attributeDefinitions,
            KeySchema = keySchemaElements,
            ProvisionedThroughput = provisionedThroughput,
        };
        return await this.CreateAndLogTable(request);
    }

    private async Task<CreateTableResponse> CreateAndLogTable(CreateTableRequest request)
    {
        CreateTableResponse response = await this.Client.CreateTableAsync(request);
        this.LogTableCreated(response.TableDescription);
        return response;
    }

    private IEnumerable<T> ConvertTaskToEnumerable<T>(Task<T>[] taskList) => taskList.Select(task => task.Result);

    private void LogTableCreated(TableDescription tableDescription)
    {
        this.Logger.LogInformation($"Table: {tableDescription.TableName}\t Status: {tableDescription.TableStatus}" +
                                    $" \t ReadCapacityUnits: {tableDescription.ProvisionedThroughput.ReadCapacityUnits}" +
                                    $" \t WriteCapacityUnits: {tableDescription.ProvisionedThroughput.WriteCapacityUnits}");
    }

    private void AssertAndLogTaskResult(string tableName, Task task)
    {
        string message = $"{tableName} - Status: {task.Status}";
        if (task.Status == TaskStatus.Canceled)
        {
            this.Logger.LogError(message);
            throw new TaskCanceledException(message);
        }
        else if (task.Status == TaskStatus.Faulted)
        {
            this.Logger.LogError(message);
            throw new TaskSchedulerException(message);
        }
        else
        {
            this.Logger.LogInformation(message);
        }
    }
}