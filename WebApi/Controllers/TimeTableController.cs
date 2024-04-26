using Domain.DTOs.TimeTableDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TimeTableController(ITimeTableService _timeTableService)
{
    [HttpGet("get-TimeTables")]
    public async Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTablesAsync([FromQuery]TimeTableFilter filter)
    {
        return await _timeTableService.GetTimeTablesAsync(filter);
    }
    
    
    [HttpGet("{TimeTableId:int}")]
    public async Task<Response<GetTimeTableDto>> GetTimeTableByIdAsync([FromBody]int timeTableId)
    {
        return await _timeTableService.GetTimeTableByIdAsync(timeTableId);
    }
    
    [HttpPost("create-TimeTable")]
    public async Task<Response<string>> AddTimeTableAsync([FromBody]AddTimetableDto timeTableDto)
    {
        return await _timeTableService.CreateTimeTableAsync(timeTableDto);
    }
    
    [HttpPut("update-TimeTable")]
    public async Task<Response<string>> UpdateTimeTableAsync([FromBody]UpdateTimeTableDto timeTableDto)
    {
        return await _timeTableService.UpdateTimeTableAsync(timeTableDto);
    }
    
    [HttpDelete("{TimeTableId:int}")]
    public async Task<Response<bool>> DeleteTimeTableAsync([FromBody]int timeTableId)
    {
        return await _timeTableService.DeleteTimeTableAsync(timeTableId);
    }
}
