namespace Domain.Entities;

public class ProgressBook : BaseEntity
{
    public int Grade { get; set; }
    public int TimeTableId { get; set; }
    public int StudentId { get; set; }
    public bool IsAttended { get; set; }
    public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    public int GroupId { get; set; }
    public string? Notes { get; set; }
    public int LateMinutes { get; set; }
    public TimeTable? TimeTable { get; set; }
    public Student? Student { get; set; }
    public Group? Group { get; set; }
}
