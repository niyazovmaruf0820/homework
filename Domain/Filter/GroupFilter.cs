using Domain.Enums;

namespace Domain.Filter;

public class GroupFilter : PaginationFilter
{
    public string? GroupName { get; set; }
    public Status Status { get; set; }
}
