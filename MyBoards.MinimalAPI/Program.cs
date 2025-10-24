using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MyBoards.MinimalAPI;
using MyBoards.MinimalAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options => {
    // Ignore possible reference loops
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

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

app.MapGet("data", (MyBoardsDbContext db) => {
    // get all tags
    var tags = db.WorkItems.ToList();

    // get top 5 newest comments
    var top5NewestComments = db.WorkItemComments.OrderByDescending(c => c.CreatedDate).Take(5).ToList();

    // get count of work items by state
    var statesCount = db.WorkItems.GroupBy(c => c.StateId).Select(g => new { stateId = g.Key, c = g.Count() }).ToList();

    // get epic work items with onhold state sorted by priority
    var epicOnHoldWorkItems = db.EpicWorkItems.Where(w => w.State.Id == 4).OrderBy(w => w.Priority).ToList();

    // user with most comments
    var userMostComments = db.WorkItemComments.GroupBy(c => c.AuthorId)
        .Select(g => new { authorId = g.Key, count = g.Count() })
        .OrderByDescending(g => g.count)
        .FirstOrDefault();

    if (userMostComments != null) {
        var userMostCommentsDetails = db.Users.Find(userMostComments.authorId);
    }
    
    // user with relations by include (join in sql)
    var user = db.Users
        .Include(u => u.Comments).ThenInclude(c => c.WorkItem)
        .Include(u => u.Address)
        .First(u => u.Id == Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"));

    return Results.Ok(tags);
});

// Update priority of an epic work item
app.MapPut("update", (MyBoardsDbContext db) => {)
    var epic = db.EpicWorkItems.First(epic => epic.Id == 1);
    epic.Priority = 10;
    db.SaveChanges();

    return Results.Ok(epic);
});

// add a new tag
app.MapPost("addTag", (MyBoardsDbContext db) => {
    var newIssue = new Tag {
        Value = "EFCore"
    };

    db.WorkItemTags.Add(newIssue);
    db.SaveChanges();

    return Results.Ok(newIssue);
});

// add a new user with address
app.MapPost("addUser", (MyBoardsDbContext db) => {
    var newUser = new User {
        // Id will be generated automatically
        FullName = "New User",
        Email = "example@gmail.com",
        Address = new Address {
            // id will be generated automatically
            Country = "USA",
            City = "New York",
            Street = "5th Avenue",
            PostalCode = "10001"
        }
    };

    db.Users.Add(newUser);
    db.SaveChanges();
    
    return Results.Ok(newUser);
});

app.Run();