using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI.Configurations;

public class EpicWorkItemConfiguration: IEntityTypeConfiguration<EpicWorkItem> {
    public void Configure(EntityTypeBuilder<EpicWorkItem> builder) {
        builder.Property(x => x.EndDate).HasPrecision(3);
        builder.ToTable("EpicWorkItems"); // Table-per-type (TPT) mapping instead of default TPH
    }
}