using System.Data.Common;
using AutoMapper;
using Domain.DTOs.MentorDto;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Entities;

namespace Infrastructure.Services.Service;

public class MentorService : IMentorService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MentorService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<string>> CreateMentorAsync(AddMentorDto mentor)
    {
        try
        {
            var existingMentor = await _context.Mentors.FirstOrDefaultAsync(x => x.Email == mentor.Email);
            if (existingMentor != null)
                return new Response<string>(HttpStatusCode.BadRequest, "mentor already exists");
            var mapped = _mapper.Map<Mentor>(mentor);

            await _context.Mentors.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new mentor");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMentorAsync(int id)
    {
        try
        {
            var mentor = await _context.Mentors.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (mentor == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Mentor not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMentorsDto>> GetMentorByIdAsync(int id)
    {
        try
        {
            var mentors = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (mentors == null)
                return new Response<GetMentorsDto>(HttpStatusCode.BadRequest, "Mentor not found");
            var mapped = _mapper.Map<GetMentorsDto>(mentors);
            return new Response<GetMentorsDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetMentorsDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetMentorsDto>>> GetMentorsAsync(MentorFilter filter)
    {
        try
        {
            var mentors = _context.Mentors.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Address))
                mentors = mentors.Where(x => x.Address.ToLower().Contains(filter.Address.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                mentors = mentors.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var response = await mentors
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = mentors.Count();

            var mapped = _mapper.Map<List<GetMentorsDto>>(response);
            return new PagedResponse<List<GetMentorsDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetMentorsDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetMentorsDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateMentorAsync(UpdateMentorsDto mentor)
    {
        try
        {
            var mapped = _mapper.Map<Mentor>(mentor);
            _context.Mentors.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "Mntor not found");
            return new Response<string>("Mentor updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
