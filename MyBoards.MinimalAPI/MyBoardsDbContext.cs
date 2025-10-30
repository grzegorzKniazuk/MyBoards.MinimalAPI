using Microsoft.EntityFrameworkCore;
using MyBoards.MinimalAPI.Entities;
using MyBoards.MinimalAPI.Entities.ViewModels;

namespace MyBoards.MinimalAPI;

public class MyBoardsDbContext : DbContext {
    public MyBoardsDbContext(DbContextOptions<MyBoardsDbContext> options) : base(options) {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Address> UserAddresses { get; set; }

    public DbSet<WorkItem> WorkItems { get; set; }

    public DbSet<EpicWorkItem> EpicWorkItems { get; set; }

    public DbSet<IssueWorkItem> IssueWorkItems { get; set; }

    public DbSet<TaskWorkItem> TaskWorkItems { get; set; }

    public DbSet<Comment> WorkItemComments { get; set; }

    public DbSet<Tag> WorkItemTags { get; set; }

    public DbSet<WorkItemState> WorkItemStates { get; set; }

    public DbSet<WorkItemTag> WorkItemTag { get; set; }

    // ViewModel DbSet
    public DbSet<TopAuthor> TopAuthorsView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Apply configurations from separate configuration classes
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}