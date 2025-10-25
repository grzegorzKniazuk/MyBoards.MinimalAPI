using Microsoft.EntityFrameworkCore;

namespace MyBoards.MinimalAPI.Entities;

// owned type example
// [Owned] attribute means AddressCoordinates is owned by Address entity so table will not be created for it
// [Owned]
public class AddressCoordinates {
    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }
}