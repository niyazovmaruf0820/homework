using Domain.Enums;

namespace Domain.Filter;

public class CourseFilter : PaginationFilter
{
    public string? CourseName { get; set; }
    public Status Status { get; set; }
}
