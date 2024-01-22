using Microsoft.EntityFrameworkCore;
using ToDoAngularGraphQL.Server.Models;
using ToDoAngularGraphQL.Server.Schema;
using ToDoAngularGraphQL.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToDoContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("default")));

builder.Services.AddGraphQLServer()
    .RegisterDbContext<ToDoContext>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin().AllowAnyHeader());
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToDoContext>();
    db.Database.Migrate();
    if (db.ToDoItems.Count() == 0)
    {
        db.ToDoItems.Add(new ToDoItem
        {
            Title = "first",
            Item = "this is my first to do",
            IsDone = false
        });
        db.SaveChanges();
    }
}


app.MapGraphQL();

app.MapFallbackToFile("/index.html");

app.UseCors("CorsPolicy");

app.Run();
