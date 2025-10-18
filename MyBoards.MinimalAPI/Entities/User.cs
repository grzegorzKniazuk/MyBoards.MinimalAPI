namespace MyBoards.MinimalAPI.Entities;

public class User {
    
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    // Navigation property for the related UserAddress
    public Address Address { get; set; }
    
    // Navigation property for the related WorkItems
    public List<WorkItem> WorkItems { get; set; } = [];
}