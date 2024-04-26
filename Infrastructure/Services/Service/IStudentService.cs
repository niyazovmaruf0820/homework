using Domain.DTOs.GroupDto;
using Domain.DTOs.StudentDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.StudentService;

public interface IStudentService
{
    Task<PagedResponse<List<GetStudentDto>>> GetStudentsAsync(StudentFilter filter);
    Task<Response<List<GroupWithCountOfStudentDto>>> GetStudentWithCountOfStudentDtoAsync();
    Task<Response<GetStudentDto>> GetStudentByIdAsync(int id);
    Task<Response<string>> CreateStudentAsync(AddStudentDto student);
    Task<Response<string>> UpdateStudentAsync(UpdateStudentDto student);
    Task<Response<bool>> DeleteStudentAsync(int id);
    Task<PagedResponse<List<GetStudentDto>>> GetStudentsbyGroup(string groupName,StudentFilter filter);
}