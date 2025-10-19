using Microsoft.EntityFrameworkCore;
using MyBoards.MinimalAPI.Entities;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Fluent API configurations for WorkItem entity
        modelBuilder.Entity<WorkItem>(builder => {
            builder.Property(x => x.Area).HasColumnType("varchar(200)");
            builder.Property(x => x.IterationPath).HasColumnName("Iteration_Path");

            // Default value for Priority
            builder.Property(x => x.Priority).HasDefaultValue(1);

            // 1:N relationship between WorkItem and WorkItemComment
            builder.HasMany(w => w.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItemId);

            // 1:N relationship between WorkItem and User (AssignedTo)
            builder.HasOne(w => w.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(w => w.AuthorId);

            // 1:N relationship between WorkItem and WorkItemState
            builder.HasOne(w => w.State)
                .WithMany(s => s.WorkItems)
                .HasForeignKey(w => w.StateId);

            // M:N relationship between WorkItem and WorkItemTag
            builder.HasMany(w => w.Tags)
                .WithMany(t => t.WorkItems)
                .UsingEntity<WorkItemTag>(
                    w => w.HasOne(wt => wt.Tag).WithMany().HasForeignKey(wt => wt.TagId),
                    w => w.HasOne(wt => wt.WorkItem).WithMany().HasForeignKey(wt => wt.WorkItemId),
                    w => {
                        w.HasKey(wt => new { wt.TagId, wt.WorkItemId });
                        w.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    }); // Join entity for additional fields
        });

        modelBuilder.Entity<EpicWorkItem>(builder => {
            builder.Property(x => x.EndDate).HasPrecision(3);
            builder.ToTable("EpicWorkItems"); // Table-per-type (TPT) mapping instead of default TPH
        });

        modelBuilder.Entity<IssueWorkItem>(builder => {
            builder.Property(x => x.Effort).HasColumnType("decimal(5,2)");
            builder.ToTable("IssueWorkItems"); // Table-per-type (TPT) mapping instead of default TPH
        });
        
        modelBuilder.Entity<TaskWorkItem>(builder => {
            builder.Property(x => x.Activity).HasMaxLength(200);
            builder.Property(x => x.RemainingWork).HasPrecision(14, 2);
            builder.ToTable("TaskWorkItems"); // Table-per-type (TPT) mapping instead of default TPH
        });

        modelBuilder.Entity<Comment>(builder => {
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
        });

        modelBuilder.Entity<WorkItemState>(builder => {
            builder.Property(x => x.Value).IsRequired().HasMaxLength(50);
        });

        // 1:1 relationship between User and UserAddress
        modelBuilder.Entity<User>(builder => {
            builder.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);
            
            // 1:N relationship between User and Comments
            builder.HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId).OnDelete(DeleteBehavior.NoAction);
        });
    }
}