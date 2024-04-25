using Domain.Enums;

namespace Domain.DTOs.CourseDto;

public class UpdateCourseDto
{
    public int Id { get; set;}
    public string CourseName { get; set; } = null!;
    public string? Description { get; set; }
    public Status Status { get; set; }
}
