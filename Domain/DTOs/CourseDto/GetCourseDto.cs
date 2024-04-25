using Domain.Enums;

namespace Domain.DTOs.CourseDto;

public class GetCourseDto
{
    public int Id { get; set;}
    public string CourseName { get; set; } = null!;
    public string? Description { get; set; }
    public Status Status { get; set; }
}
