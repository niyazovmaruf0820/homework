using Domain.DTOs.ProgressBookDto;
using Domain.Responses;

namespace Infrastructure.Services.Service;

public interface IProgressBookService
{
    Task<PagedResponse<List<GetProgressbookDto>>> GetProgressBooksAsync();
    Task<Response<GetProgressbookDto>> GetProgressBookByIdAsync(int id);
    Task<Response<string>> CreateProgressBookAsync(AddProgressBookDto progressBook);
    Task<Response<string>> UpdateProgressBookAsync(UpdateProgressBookDto progressBook);
    Task<Response<bool>> DeleteProgressBookAsync(int id);
}
