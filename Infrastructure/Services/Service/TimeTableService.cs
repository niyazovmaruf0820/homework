using AutoMapper;
using Domain.DTOs.TimeTableDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;
namespace Infrastructure.Services.Service;

public class TimeTableService 
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TimeTableService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateTimeTableAsync(AddTimetableDto TimeTable)
    {
        try
        {
            var existingTimeTable = await _context.TimeTables.FirstOrDefaultAsync(x => x.GroupId == TimeTable.GroupId);
            if (existingTimeTable != null)
                return new Response<string>(HttpStatusCode.BadRequest, "TimeTable already exists");
            var mapped = _mapper.Map<TimeTable>(TimeTable);

            await _context.TimeTables.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new TimeTable");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteTimeTableAsync(int id)
    {
        try
        {
            var timeTables = await _context.TimeTables.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (timeTables == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "TimeTables not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetTimeTableDto>> GetTimeTableByIdAsync(int id)
    {
        try
        {
            var TimeTables = await _context.TimeTables.FirstOrDefaultAsync(x => x.Id == id);
            if (TimeTables == null)
                return new Response<GetTimeTableDto>(HttpStatusCode.BadRequest, "TimeTable not found");
            var mapped = _mapper.Map<GetTimeTableDto>(TimeTables);
            return new Response<GetTimeTableDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetTimeTableDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTablesAsync(TimeTableFilter filter)
    {
        try
        {
            var timeTables = _context.TimeTables.AsQueryable();

            if (filter?.FromTime != null)
                timeTables = timeTables.Where(x => x.FromTime == filter.FromTime);
            if (filter?.ToTime != null)
                timeTables = timeTables.Where(x => x.ToTime == filter.ToTime);

            var response = await timeTables
                .Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = timeTables.Count();

            var mapped = _mapper.Map<List<GetTimeTableDto>>(response);
            return new PagedResponse<List<GetTimeTableDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetTimeTableDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetTimeTableDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<Response<string>> UpdateTimeTableAsync(UpdateTimeTableDto TimeTable)
    {
        try
        {
            var mapped = _mapper.Map<TimeTable>(TimeTable);
            _context.TimeTables.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "TimeTables not found");
            return new Response<string>("TimeTables updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
