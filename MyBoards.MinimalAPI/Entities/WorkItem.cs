namespace MyBoards.MinimalAPI.Entities;

public class WorkItem {
    
    public int Id { get; set; }
    
    // [Column(TypeName = "varchar(200)")] Example of custom column type
    public string Area { get; set; }
    
    // [Column("Iteration_Path")] Example of custom column mapping
    public string IterationPath { get; set; }

    public int Priority { get; set; }
    
    // Epic Fields
    public DateTime? StartDate { get; set; }
    
    // [Precision(3)] Example of precision for DateTime
    public DateTime? EndDate { get; set; }
    
    // Issue Fields
    // [Column(TypeName = "decimal(5,2)")] Example of precision for decimal
    public decimal Effort { get; set; }
    
    // Task Fields
    // [MaxLength(200)] Example of max length constraint
    public string Activity { get; set; }
    
    // [Precision(14,2)] Example of precision for decimal
    public decimal RemainingWork { get; set; }
    
    public string Type { get; set; }
    
    // Navigation properties
    public List<Comment> Comments { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
    
    public WorkItemState State { get; set; }
    public int StateId { get; set; }
    
    public User Author { get; set; }
    public Guid AuthorId { get; set; }
}