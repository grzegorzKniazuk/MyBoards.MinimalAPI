using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<Address>(a => a.UserId);

        // set index on Email
        builder.HasIndex(u => new { u.Email, u.FullName }).IsUnique();
        
        // 1:N relationship between User and Comments
        builder.HasMany(u => u.Comments)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.AuthorId).OnDelete(DeleteBehavior.ClientCascade); // use client cascade to avoid cycles
    }
}