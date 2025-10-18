namespace MyBoards.MinimalAPI.Entities;

public class IssueWorkItem: WorkItem {
    
    // [Column(TypeName = "decimal(5,2)")] Example of precision for decimal
    public decimal Effort { get; set; }
}