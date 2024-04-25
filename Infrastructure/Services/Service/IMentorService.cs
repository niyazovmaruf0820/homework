using Domain.DTOs.MentorDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.Service;

public interface IMentorService 
{
    Task<PagedResponse<List<GetMentorsDto>>> GetMentorsAsync(MentorFilter filter);
    Task<Response<GetMentorsDto>> GetMentorByIdAsync(int id);
    Task<Response<string>> CreateMentorAsync(AddMentorDto mentor);
    Task<Response<string>> UpdateMentorAsync(UpdateMentorsDto mentor);
    Task<Response<bool>> DeleteMentorAsync(int id);
}
