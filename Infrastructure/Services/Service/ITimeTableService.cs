using Domain.DTOs.TimeTableDto;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.Service;

public interface ITimeTableService  
{
    Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTablesAsync(TimeTableFilter filter);
    Task<Response<GetTimeTableDto>> GetTimeTableByIdAsync(int id);
    Task<Response<string>> CreateTimeTableAsync(AddTimetableDto timeTable);
    Task<Response<string>> UpdateTimeTableAsync(UpdateTimeTableDto timeTable);
    Task<Response<bool>> DeleteTimeTableAsync(int id);
}
