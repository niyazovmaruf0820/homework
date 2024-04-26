using Domain.Enums;

namespace Domain.Entities;
 
public class TimeTable : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public TimeTableType TimeTableType { get; set; }
    public int GroupId { get; set; }
    public Group? Group { get; set; }
    public List<ProgressBook>? ProgressBooks { get; set; }
}

