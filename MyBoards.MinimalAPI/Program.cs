using Microsoft.EntityFrameworkCore;
using MyBoards.MinimalAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyBoardsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migrate database on startup
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<MyBoardsDbContext>();
var pendingMigrations = dbContext.Database.GetPendingMigrations();

if (pendingMigrations.Any()) {
    dbContext.Database.Migrate();
}

app.Run();