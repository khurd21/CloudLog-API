using Amazon.DynamoDBv2.Model;

namespace CloudLogAPI.Repositories;

public interface IDynamoDBTableManager
{
    IEnumerable<CreateTableResponse> CreateTables();
    IEnumerable<DeleteTableResponse> DeleteTables();
}