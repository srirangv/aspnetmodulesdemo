global using Helpers;
global using Data;
global using Modules;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

var dbName = builder.Configuration["DatabaseName"];
builder.Services.AddSingleton(new DatabaseConfig { Name = dbName });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddTransient(db => new SqliteConnection(dbName));

var app = builder.Build(); 

new Todos().AddRoutes(app);

using (var scope = app.Services.CreateScope())
{
    var bootstrap = scope.ServiceProvider.GetRequiredService<IDatabaseBootstrap>();
    bootstrap.Setup();
}

app.Run();



// app.MapGet("/", () => "Hello World!");
// app.MapGet("/users/{userId}/books/{bookId}", 
//     async (int userId, int bookId) => await Task.FromResult($"The user id is {userId} and book id is {bookId}"));

// curl -i -d '{"title":"buy milk","isComplete":false}' -H 'Content-Type: application/json'  http://localhost:5000/api/todos
// curl -i http://localhost:5000/api/todos
// curl -i http://localhost:5000/api/todos/1221
// curl -i -X PUT http://localhost:5000/api/todos/1221/mark-complete
// curl -i -X DELETE http://localhost:5000/api/todos/1221