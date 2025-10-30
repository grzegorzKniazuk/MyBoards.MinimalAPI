using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI.Configurations;

public class AddressConfiguration: IEntityTypeConfiguration<Address> {
    
    public void Configure(EntityTypeBuilder<Address> builder) {
        builder.OwnsOne(a => a.Coordinates, coord => {
            coord.Property(c => c.Latitude).HasPrecision(18, 7);
            coord.Property(c => c.Longitude).HasPrecision(18, 7);
        });
    }
}