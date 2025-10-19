namespace MyBoards.MinimalAPI.Entities;

public class User {
    
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    
    // Navigation property for the related UserAddress
    public Address Address { get; set; }
    
    // Navigation property for the related WorkItems
    public List<WorkItem> WorkItems { get; set; } = [];
    
    // Navigation property for the related Comments
    public List<Comment> Comments { get; set; } = [];
}