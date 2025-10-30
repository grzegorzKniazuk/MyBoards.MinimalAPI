using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI.Configurations;

public class TaskWorkItemConfiguration: IEntityTypeConfiguration<TaskWorkItem> {
    public void Configure(EntityTypeBuilder<TaskWorkItem> builder) {
        builder.Property(x => x.Activity).HasMaxLength(200);
        builder.Property(x => x.RemainingWork).HasPrecision(14, 2);
        builder.ToTable("TaskWorkItems"); // Table-per-type (TPT) mapping instead of default TPH
    }
}