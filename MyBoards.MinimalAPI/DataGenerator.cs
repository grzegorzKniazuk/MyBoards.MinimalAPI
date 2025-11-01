using Bogus;
using MyBoards.MinimalAPI.Entities;

namespace MyBoards.MinimalAPI;

public class DataGenerator {
    public void Seed(MyBoardsDbContext db) {
        var addressGenerator = new Faker<Address>()
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());
        
        var userGenerator = new Faker<User>()
            .RuleFor(u => u.FullName, f => f.Person.FullName)
            .RuleFor(u => u.Email, (f, u) => f.Person.Email)
            .RuleFor(u => u.Address, addressGenerator.Generate());
        
        var users = userGenerator.Generate(10);
        db.Users.AddRange(users);

        db.SaveChanges();
    }
}