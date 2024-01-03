using Amazon.DynamoDBv2.Model;

namespace CloudLogAPI.Services;

public interface IDynamoDBTableManager
{
    IEnumerable<CreateTableResponse> CreateTables();
    IEnumerable<DeleteTableResponse> DeleteTables();
}