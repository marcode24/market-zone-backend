
namespace Domain.Shared;

public class PagedResults<TResponse>
{
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
  public int TotalPages { get; set; }
  public int TotalRecords { get; set; }
  public List<TResponse> Results { get; set; } = [];
}
