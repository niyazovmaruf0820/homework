using AutoMapper;
using Domain.DTOs.ProgressBookDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrastructure.Services.Service;

public class ProgressBookService : IProgressBookService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ProgressBookService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateProgressBookAsync(AddProgressBookDto progressBook)
    {
        try
        {
            var existingProgressBook = await _context.ProgressBooks.FirstOrDefaultAsync(x => x.GroupId == progressBook.GroupId);
            if (existingProgressBook != null)
                return new Response<string>(HttpStatusCode.BadRequest, "ProgressBook already exists");
            var mapped = _mapper.Map<ProgressBook>(progressBook);

            await _context.ProgressBooks.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new ProgressBook");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteProgressBookAsync(int id)
    {
        try
        {
            var progressBook = await _context.ProgressBooks.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (progressBook == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "ProgressBook not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetProgressbookDto>> GetProgressBookByIdAsync(int id)
    {
        try
        {
            var ProgressBooks = await _context.ProgressBooks.FirstOrDefaultAsync(x => x.Id == id);
            if (ProgressBooks == null)
                return new Response<GetProgressbookDto>(HttpStatusCode.BadRequest, "ProgressBook not found");
            var mapped = _mapper.Map<GetProgressbookDto>(ProgressBooks);
            return new Response<GetProgressbookDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetProgressbookDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetProgressbookDto>>> GetProgressBooksAsync()
    {
        try
        {
            var progressBooks = _context.ProgressBooks.AsQueryable();
            var response = _context.ProgressBooks.Select(x => x);
            var totalRecord = progressBooks.Count();

            var mapped = _mapper.Map<List<GetProgressbookDto>>(response);
            return new PagedResponse<List<GetProgressbookDto>>(mapped, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetProgressbookDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetProgressbookDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateProgressBookAsync(UpdateProgressBookDto progressBook)
    {
        try
        {
            var mapped = _mapper.Map<ProgressBook>(progressBook);
            _context.ProgressBooks.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "ProgressBook not found");
            return new Response<string>("ProgressBook updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
