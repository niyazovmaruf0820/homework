using System.Data.Common;
using AutoMapper;
using Domain.DTOs.MentorGroupDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Entities;

namespace Infrastructure.Services.Service;

public class MentorGroupService : IMentorMentorGroupService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MentorGroupService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<string>> CreateMentorGroupAsync(AddMentorGroupDto mentorGroup)
    {
        try
        {
            var existingMGroup = await _context.MentorGroups.FirstOrDefaultAsync(x => x.MentorId == mentorGroup.MentorId);
            if (existingMGroup != null)
                return new Response<string>(HttpStatusCode.BadRequest, "MentorGroup already exists");
            var mapped = _mapper.Map<Group>(mentorGroup);

            await _context.Groups.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new MentorGroup");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMentorGroupAsync(int id)
    {
        try
        {
            var mgroups = await _context.MentorGroups.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (mgroups == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "MentorGroups not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMentorGroupDto>> GetMentorGroupByIdAsync(int id)
    {
        try
        {
            var mentorgroups = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (mentorgroups == null)
                return new Response<GetMentorGroupDto>(HttpStatusCode.BadRequest, "Mentorgroups not found");
            var mapped = _mapper.Map<GetMentorGroupDto>(mentorgroups);
            return new Response<GetMentorGroupDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetMentorGroupDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetMentorGroupDto>>> GetMentorGroupsAsync(MentorGroupFilter filter)
    {
        try
        {
            var mentorGroup = _context.MentorGroups.AsQueryable();

            if (filter?.MentorId != null)
                mentorGroup = mentorGroup.Where(x => x.MentorId == filter.MentorId);
            if (filter?.GroupId != null) 
                mentorGroup = mentorGroup.Where(x => x.GroupId == filter.GroupId);

            var response = await mentorGroup
                .Skip((filter!.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = mentorGroup.Count();

            var mapped = _mapper.Map<List<GetMentorGroupDto>>(response);
            return new PagedResponse<List<GetMentorGroupDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetMentorGroupDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetMentorGroupDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateMentorGroupAsync(UpdateMentorGroupDto mentorGroup)
    {
        try
        {
            var mapped = _mapper.Map<MentorGroup>(mentorGroup);
            _context.MentorGroups.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "MentorGroups not found");
            return new Response<string>("MentorGroups updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
