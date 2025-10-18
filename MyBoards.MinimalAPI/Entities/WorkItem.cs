namespace MyBoards.MinimalAPI.Entities;

public abstract class WorkItem {
    
    public int Id { get; set; }
    
    // [Column(TypeName = "varchar(200)")] Example of custom column type
    public string Area { get; set; }
    
    // [Column("Iteration_Path")] Example of custom column mapping
    public string IterationPath { get; set; }

    public int Priority { get; set; }
    
    // Navigation properties
    public List<Comment> Comments { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
    
    public WorkItemState State { get; set; }
    public int StateId { get; set; }
    
    public User Author { get; set; }
    public Guid AuthorId { get; set; }
}