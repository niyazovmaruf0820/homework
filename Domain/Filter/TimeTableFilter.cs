namespace Domain.Filter;

public class TimeTableFilter : PaginationFilter
{
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
}
