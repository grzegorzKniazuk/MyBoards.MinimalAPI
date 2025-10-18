namespace MyBoards.MinimalAPI.Entities;

public class TaskWorkItem: WorkItem {
    
    // [MaxLength(200)] Example of max length constraint
    public string Activity { get; set; }
    
    // [Precision(14,2)] Example of precision for decimal
    public decimal RemainingWork { get; set; }
}