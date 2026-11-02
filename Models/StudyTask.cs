namespace project.Models;

public class StudyTask{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Course { get; set; }
    public DateOnly DueDate { get; set; }
    public bool IsCompleted { get; set; }
}