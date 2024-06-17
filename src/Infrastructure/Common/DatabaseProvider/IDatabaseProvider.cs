namespace Infrastructure.Common.DatabaseProvider;

public interface IDatabaseProvider
{
    string GetConnectionString(string connectionName);
    string GetDefaultConnectionString();
}