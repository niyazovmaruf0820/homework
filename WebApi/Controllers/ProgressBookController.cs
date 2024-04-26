using Domain.DTOs.ProgressBookDto;
using Domain.Responses;
using Infrastructure.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class ProgressBookController(IProgressBookService _progressBookService)
{
    [HttpGet("get-ProgressBooks")]
    public async Task<PagedResponse<List<GetProgressbookDto>>> GetProgressBooksAsync()
    {
        return await _progressBookService.GetProgressBooksAsync();
    }
    
    
    [HttpGet("{ProgressBookId:int}")]
    public async Task<Response<GetProgressbookDto>> GetProgressBookByIdAsync([FromBody]int progressBookId)
    {
        return await _progressBookService.GetProgressBookByIdAsync(progressBookId);
    }
    
    [HttpPost("create-ProgressBook")]
    public async Task<Response<string>> AddProgressBookAsync([FromBody]AddProgressBookDto progressBookDto)
    {
        return await _progressBookService.CreateProgressBookAsync(progressBookDto);
    }
    
    [HttpPut("update-ProgressBook")]
    public async Task<Response<string>> UpdateProgressBookAsync([FromBody]UpdateProgressBookDto progressBookDto)
    {
        return await _progressBookService.UpdateProgressBookAsync(progressBookDto);
    }
    
    [HttpDelete("{ProgressBookId:int}")]
    public async Task<Response<bool>> DeleteProgressBookAsync([FromBody]int progressBookId)
    {
        return await _progressBookService.DeleteProgressBookAsync(progressBookId);
    }
}
