using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI.Configurations;

public class IssueWorkItemConfiguration: IEntityTypeConfiguration<IssueWorkItem> {
    public void Configure(EntityTypeBuilder<IssueWorkItem> builder) {
        builder.Property(x => x.Effort).HasColumnType("decimal(5,2)");
        builder.ToTable("IssueWorkItems"); // Table-per-type (TPT) mapping instead of default TPH
    }
}