namespace MyBoards.MinimalAPI.Entities;

public class EpicWorkItem: WorkItem {

    public DateTime? StartDate { get; set; }
    
    // [Precision(3)] Example of precision for DateTime
    public DateTime? EndDate { get; set; }
}