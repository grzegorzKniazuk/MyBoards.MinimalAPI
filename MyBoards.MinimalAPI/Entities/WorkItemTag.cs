namespace MyBoards.MinimalAPI.Entities;

// optional joining entity for M:N relationship with additional properties
public class WorkItemTag {
    
    public int WorkItemId { get; set; }
    public WorkItem WorkItem { get; set; }
    
    public int TagId { get; set; }
    public Tag Tag { get; set; }
    
    // Additional property for the relationship
    public DateTime PublicationDate { get; set; }
}