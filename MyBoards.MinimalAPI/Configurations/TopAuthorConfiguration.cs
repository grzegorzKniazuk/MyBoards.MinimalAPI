using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities.ViewModels;

namespace MyBoards.MinimalAPI.Configurations;

public class TopAuthorConfiguration: IEntityTypeConfiguration<TopAuthor> {
    public void Configure(EntityTypeBuilder<TopAuthor> builder) {
        builder.ToView("TopAuthorsView"); // Map to the database view

        // No primary key defined for the view model
        builder.HasNoKey();
    }
}