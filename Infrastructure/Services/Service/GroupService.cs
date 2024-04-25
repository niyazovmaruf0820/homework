using AutoMapper;
using Domain.DTOs.GroupDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;
using Domain.Entities;

namespace Infrastructure.Services.Service;

public class GroupService : IGroupService // Group
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GroupService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateGroupAsync(AddGroupDto group)
    {
        try
        {
            var existingGroup = await _context.Groups.FirstOrDefaultAsync(x => x.GroupName == group.GroupName);
            if (existingGroup != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Group already exists");
            var mapped = _mapper.Map<Group>(group);

            await _context.Groups.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Group");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteGroupAsync(int id)
    {
        try
        {
            var groups = await _context.Groups.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (groups == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Groups not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetGroupDto>> GetGroupByIdAsync(int id)
    {
        try
        {
            var groups = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (groups == null)
                return new Response<GetGroupDto>(HttpStatusCode.BadRequest, "Group not found");
            var mapped = _mapper.Map<GetGroupDto>(groups);
            return new Response<GetGroupDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetGroupDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetGroupDto>>> GetGroupsAsync(GroupFilter filter)
    {
        try
        {
            var groups = _context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(filter.GroupName))
                groups = groups.Where(x => x.GroupName.ToLower().Contains(filter.GroupName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Status.ToString()))
                groups = groups.Where(x => x.Status.ToString().ToLower().Contains(filter.Status.ToString().ToLower()));

            var response = await groups
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = groups.Count();

            var mapped = _mapper.Map<List<GetGroupDto>>(response);
            return new PagedResponse<List<GetGroupDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetGroupDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetGroupDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<Response<string>> UpdateGroupAsync(UpdateGroupDto group)
    {
        try
        {
            var mapped = _mapper.Map<Group>(group);
            _context.Groups.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "Groups not found");
            return new Response<string>("Groups updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
