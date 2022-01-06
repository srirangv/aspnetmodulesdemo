namespace Data;

using Dapper;
using Microsoft.Data.Sqlite;

public class DatabaseBootstrap : IDatabaseBootstrap
{
    private readonly DatabaseConfig databaseConfig;

    public DatabaseBootstrap(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public void Setup()
    {
        using var connection = new SqliteConnection(databaseConfig.Name);
        
        connection.Execute("Create Table If Not Exists Todos (" +
                           "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                           "Title VARCHAR(100) NULL," +
                           "IsComplete INTEGER NULL);");
    }
}

public interface IDatabaseBootstrap
{
    void Setup();
}

public class DatabaseConfig
{
    public string? Name { get; set; }
}

public static class ServiceExtensions
{
    public static void AddSqliteDbConnection(this IServiceCollection services, string? dbName)
    {
        services.AddScoped<Func<string, SqliteConnection>>(serviceProvider => tenant =>
        {
            if (null == dbName)
                throw new KeyNotFoundException("No instance found for the given tenant.");

            return new SqliteConnection(dbName);
        });
    }
}