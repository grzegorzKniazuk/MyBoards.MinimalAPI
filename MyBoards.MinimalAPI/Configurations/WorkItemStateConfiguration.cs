using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI.Configurations;

public class WorkItemStateConfiguration: IEntityTypeConfiguration<WorkItemState> {
    public void Configure(EntityTypeBuilder<WorkItemState> builder) {
        builder.Property(x => x.Value).IsRequired().HasMaxLength(50);
        
        builder.HasData(
            new WorkItemState { Id = 1, Value = "To Do" },
            new WorkItemState { Id = 2, Value = "Doing" },
            new WorkItemState { Id = 3, Value = "Done" }
        );
    }
}