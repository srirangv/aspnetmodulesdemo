namespace Data;

using Dapper;
using Microsoft.Data.Sqlite;

public class DatabaseBootstrap : IDatabaseBootstrap
{
    private readonly SqliteConnection _connection;
    
    public DatabaseBootstrap(SqliteConnection connection)
    {
        this._connection = connection;
    }

    public void Setup()
    {
        _connection.Execute("Create Table If Not Exists Todos (" +
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
    public static void BootstrapDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var bootstrap = scope.ServiceProvider.GetRequiredService<IDatabaseBootstrap>();
        bootstrap.Setup();
    }
}