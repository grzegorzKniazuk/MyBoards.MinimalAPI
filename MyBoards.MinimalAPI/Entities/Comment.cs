namespace MyBoards.MinimalAPI.Entities;

public class Comment {
    
    public int Id { get; set; }
    
    public string Message { get; set; }
    
    public string Author { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime? UpdatedDate { get; set; }
    
    // Navigation property
    public WorkItem WorkItem { get; set; }
    
    // Foreign key
    public int WorkItemId { get; set; }
}