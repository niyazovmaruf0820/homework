namespace Domain.Filter;

public class MentorGroupFilter : PaginationFilter
{
    public int MentorId { get; set; }
    public int GroupId { get; set; }
}
