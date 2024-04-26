using Domain.DTOs.MentorDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class MentorController(IMentorService _mentorService) : ControllerBase
{
    [HttpGet("get-Mentors")]
    public async Task<PagedResponse<List<GetMentorsDto>>> GetMentorsAsync([FromQuery]MentorFilter filter)
    {
        return await _mentorService.GetMentorsAsync(filter);
    }
    
    
    [HttpGet("{MentorId:int}")]
    public async Task<Response<GetMentorsDto>> GetMentorByIdAsync([FromBody]int mentorId)
    {
        return await _mentorService.GetMentorByIdAsync(mentorId);
    }
    
    [HttpPost("create-Mentor")]
    public async Task<Response<string>> AddMentorAsync([FromBody]AddMentorDto mentorDto)
    {
        return await _mentorService.CreateMentorAsync(mentorDto);
    }
    
    [HttpPut("update-Mentor")]
    public async Task<Response<string>> UpdateMentorAsync([FromBody]UpdateMentorsDto mentorDto)
    {
        return await _mentorService.UpdateMentorAsync(mentorDto);
    }
    
    [HttpDelete("{MentorId:int}")]
    public async Task<Response<bool>> DeleteMentorAsync([FromBody]int mentorId)
    {
        return await _mentorService.DeleteMentorAsync(mentorId);
    }
}
