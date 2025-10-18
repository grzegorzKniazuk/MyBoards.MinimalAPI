namespace MyBoards.MinimalAPI.Entities;

public class Tag {
    
    public int Id { get; set; }
    public string Value { get; set; }
    
    // Navigation property
    public List<WorkItem> WorkItems { get; set; } = [];
}