global using Helpers;
global using Data;
global using Modules;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var dbName = builder.Configuration["DatabaseName"];
services.AddSingleton(new DatabaseConfig { Name = dbName });
services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
services.AddTransient(db => new SqliteConnection(dbName));

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build(); 

new Todos().AddRoutes(app);

app
    .UseSwagger()
    .UseSwaggerUI();

app.BootstrapDb();

app.Run();



// app.MapGet("/", () => "Hello World!");
// app.MapGet("/users/{userId}/books/{bookId}", 
//     async (int userId, int bookId) => await Task.FromResult($"The user id is {userId} and book id is {bookId}"));

// curl -i -d '{"title":"buy milk","isComplete":false}' -H 'Content-Type: application/json'  http://localhost:5000/api/todos
// curl -i http://localhost:5000/api/todos
// curl -i http://localhost:5000/api/todos/1221
// curl -i -X PUT http://localhost:5000/api/todos/1221/mark-complete
// curl -i -X DELETE http://localhost:5000/api/todos/1221