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

    return Results.Ok(tags);
});

// Update priority of an epic work item
app.MapPut("update", (MyBoardsDbContext db) => {)
    var epic = db.EpicWorkItems.First(epic => epic.Id == 1);
    epic.Priority = 10;
    db.SaveChanges();
    
    return Results.Ok(epic);
});

app.Run();