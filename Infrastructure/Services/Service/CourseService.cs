using System.Data.Common;
using AutoMapper;
using Domain.DTOs.CourseDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.CourseService;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Entities;

namespace Infrastructure.Services.Service;

public class CourseService : ICourseService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CourseService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateCourseAsync(AddCourseDto course)
    {
        try
        {
            var existingCourse = await _context.Courses.FirstOrDefaultAsync(x => x.CourseName == course.CourseName);
            if (existingCourse != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Course already exists");
            var mapped = _mapper.Map<Course>(course);

            await _context.Courses.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new course");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCourseAsync(int id)
    {
        try
        {
            var courses = await _context.Courses.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (courses == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Courses not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCourseDto>> GetCourseByIdAsync(int id)
    {
        try
        {
            var courses = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (courses == null)
                return new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Course not found");
            var mapped = _mapper.Map<GetCourseDto>(courses);
            return new Response<GetCourseDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetCourseDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync(CourseFilter filter)
    {
        try
        {
            var courses = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.CourseName))
                courses = courses.Where(x => x.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Status.ToString()))
                courses = courses.Where(x => x.Status.ToString().ToLower().Contains(filter.Status.ToString().ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = _mapper.Map<List<GetCourseDto>>(response);
            return new PagedResponse<List<GetCourseDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateCourseAsync(UpdateCourseDto course)
    {
        try
        {
            var mapped = _mapper.Map<Course>(course);
            _context.Courses.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "Courses not found");
            return new Response<string>("Courses updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
