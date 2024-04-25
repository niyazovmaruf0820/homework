using Domain.Responses;
using Domain.DTOs.CourseDto;
using Domain.Filter;
namespace Infrastructure.Services.CourseService;

public interface ICourseService // Course
{
    Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync(CourseFilter filter);
    Task<Response<GetCourseDto>> GetCourseByIdAsync(int id);
    Task<Response<string>> CreateCourseAsync(AddCourseDto course);
    Task<Response<string>> UpdateCourseAsync(UpdateCourseDto course);
    Task<Response<bool>> DeleteCourseAsync(int id);
}
