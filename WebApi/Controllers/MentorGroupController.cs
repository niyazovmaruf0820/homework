using Domain.DTOs.MentorGroupDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MentorMentorGroupController(IMentorGroupService _mentorGroupService) : ControllerBase
{
    [HttpGet("get-MentorGroups")]
    public async Task<PagedResponse<List<GetMentorGroupDto>>> GetMentorGroupsAsync([FromQuery]MentorGroupFilter filter)
    {
        return await _mentorGroupService.GetMentorGroupsAsync(filter);
    }
    
    
    [HttpGet("{MentorGroupId:int}")]
    public async Task<Response<GetMentorGroupDto>> GetMentorGroupByIdAsync([FromBody]int mentorGroupId)
    {
        return await _mentorGroupService.GetMentorGroupByIdAsync(mentorGroupId);
    }
    
    [HttpPost("create-MentorGroup")]
    public async Task<Response<string>> AddMentorGroupAsync([FromBody]AddMentorGroupDto mentorGroupDto)
    {
        return await _mentorGroupService.CreateMentorGroupAsync(mentorGroupDto);
    }
    
    [HttpPut("update-MentorGroup")]
    public async Task<Response<string>> UpdateMentorGroupAsync([FromBody]UpdateMentorGroupDto mentorGroupDto)
    {
        return await _mentorGroupService.UpdateMentorGroupAsync(mentorGroupDto);
    }
    
    [HttpDelete("{MentorGroupId:int}")]
    public async Task<Response<bool>> DeleteMentorGroupAsync([FromBody]int mentorGroupId)
    {
        return await _mentorGroupService.DeleteMentorGroupAsync(mentorGroupId);
    }
}
