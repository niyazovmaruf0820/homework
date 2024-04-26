using Domain.Enums;

namespace Domain.DTOs.TimeTableDto;

public class AddTimetableDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public TimeTableType TimeTableType { get; set; }
    public int GroupId { get; set;}
}
