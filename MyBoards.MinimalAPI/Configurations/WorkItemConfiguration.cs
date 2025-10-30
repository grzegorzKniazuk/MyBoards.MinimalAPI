using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI.Configurations;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem> {
    public void Configure(EntityTypeBuilder<WorkItem> builder) {
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
    }
}