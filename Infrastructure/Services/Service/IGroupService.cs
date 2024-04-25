using Domain.DTOs.GroupDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.Service;

public interface IGroupService 
{
    Task<PagedResponse<List<GetGroupDto>>> GetGroupsAsync(GroupFilter filter);
    Task<Response<GetGroupDto>> GetGroupByIdAsync(int id);
    Task<Response<string>> CreateGroupAsync(AddGroupDto group);
    Task<Response<string>> UpdateGroupAsync(UpdateGroupDto group);
    Task<Response<bool>> DeleteGroupAsync(int id);
}
