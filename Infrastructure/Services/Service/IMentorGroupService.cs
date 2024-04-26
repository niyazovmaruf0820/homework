using Domain.DTOs.MentorGroupDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.Service;

public interface IMentorGroupService
{
    Task<PagedResponse<List<GetMentorGroupDto>>> GetMentorGroupsAsync(MentorGroupFilter filter);
    Task<Response<GetMentorGroupDto>> GetMentorGroupByIdAsync(int id);
    Task<Response<string>> CreateMentorGroupAsync(AddMentorGroupDto mentorGroup);
    Task<Response<string>> UpdateMentorGroupAsync(UpdateMentorGroupDto mentorGroup);
    Task<Response<bool>> DeleteMentorGroupAsync(int id);
}
