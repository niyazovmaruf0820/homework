using Domain.DTOs.GroupDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class GroupController(IGroupService _groupService) : ControllerBase
{
    [HttpGet("get-Groups")]
    public async Task<PagedResponse<List<GetGroupDto>>> GetGroupsAsync([FromQuery]GroupFilter filter)
    {
        return await _groupService.GetGroupsAsync(filter);
    }
    
    
    [HttpGet("{GroupId:int}")]
    public async Task<Response<GetGroupDto>> GetGroupByIdAsync([FromBody]int groupId)
    {
        return await _groupService.GetGroupByIdAsync(groupId);
    }
    
    [HttpPost("create-Group")]
    public async Task<Response<string>> AddGroupAsync([FromBody]AddGroupDto groupDto)
    {
        return await _groupService.CreateGroupAsync(groupDto);
    }
    
    [HttpPut("update-Group")]
    public async Task<Response<string>> UpdateGroupAsync([FromBody]UpdateGroupDto groupDto)
    {
        return await _groupService.UpdateGroupAsync(groupDto);
    }
    
    [HttpDelete("{GroupId:int}")]
    public async Task<Response<bool>> DeleteGroupAsync([FromBody]int groupId)
    {
        return await _groupService.DeleteGroupAsync(groupId);
    }
}
