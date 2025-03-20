namespace IMS.Domain.Utilities;

public class Pagination
{
    public int? TotalPages { get; set; }
    public int? Total { get; set; }
    public int? PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
    public string? RefId { get; set; }
    public string? SearchQuery { get; set; }
}
