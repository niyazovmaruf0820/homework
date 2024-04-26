using System.Net;
using Domain.DTOs.ProgressBookDto;

namespace Domain.Responses;

public class PagedResponse<T>:Response<T>
{
    private List<GetProgressbookDto> mapped;


    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecord { get; set; }
    public int TotalPage { get; set; }
    public PagedResponse(T data ,int pageNumber, int pageSize, int totalRecord ) : base(data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecord = totalRecord;
        TotalPage = (int)Math.Ceiling(totalRecord / (float)pageSize);
    }

    public PagedResponse(HttpStatusCode statusCode, string error) : base(statusCode, error)
    {
    }

    public PagedResponse(HttpStatusCode statusCode, List<string> error) : base(statusCode, error)
    {
    }

    public PagedResponse(T data, int totalRecord) : base(data)
    {
        this.Data = data;
        TotalRecord = totalRecord;
    }
}