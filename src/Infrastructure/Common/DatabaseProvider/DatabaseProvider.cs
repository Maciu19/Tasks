using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common.DatabaseProvider;

public class DatabaseProvider : IDatabaseProvider
{
    private readonly IConfiguration _configuration;

    public DatabaseProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString(string connectionName)
    {
        var connectionString = _configuration.GetConnectionString(connectionName);

        return string.IsNullOrEmpty(connectionString)
            ? throw new ArgumentException($"Connection string for '{connectionName}' is null or empty.")
            : connectionString;
    }

    public string GetDefaultConnectionString()
    {
        return GetConnectionString("DefaultConnection");
    }
}