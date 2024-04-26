namespace Domain.DTOs.ProgressBookDto;

public class AddProgressBookDto
{
    public int Grade { get; set; }
    public int TimeTableId { get; set; }
    public int StudentId { get; set; }
    public bool IsAttended { get; set; }
    public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    public int GroupId { get; set; }
    public string? Notes { get; set; }
    public int LateMinutes { get; set; }
}
