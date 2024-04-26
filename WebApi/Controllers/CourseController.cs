using Domain.DTOs.CourseDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.CourseService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class CourseController(ICourseService _courseService) : ControllerBase
{
    [HttpGet("get-Courses")]
    public async Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync([FromQuery]CourseFilter filter)
    {
        return await _courseService.GetCoursesAsync(filter);
    }
    
    
    [HttpGet("{CourseId:int}")]
    public async Task<Response<GetCourseDto>> GetCourseByIdAsync([FromBody]int courseId)
    {
        return await _courseService.GetCourseByIdAsync(courseId);
    }
    
    [HttpPost("create-Course")]
    public async Task<Response<string>> AddCourseAsync([FromBody]AddCourseDto courseDto)
    {
        return await _courseService.CreateCourseAsync(courseDto);
    }
    
    [HttpPut("update-Course")]
    public async Task<Response<string>> UpdateCourseAsync([FromBody]UpdateCourseDto courseDto)
    {
        return await _courseService.UpdateCourseAsync(courseDto);
    }
    
    [HttpDelete("{CourseId:int}")]
    public async Task<Response<bool>> DeleteCourseAsync([FromBody]int courseId)
    {
        return await _courseService.DeleteCourseAsync(courseId);
    }
}
