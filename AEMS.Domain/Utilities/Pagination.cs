namespace IMS.Domain.Utilities;

public class Pagination
{
    public int? TotalPages { get; set; } // Total number of pages
    public int? Total { get; set; } // Total number of items
    public int PageIndex { get; set; } = 1; // Current page index (1-based)
    public int PageSize { get; set; } = 10; // Number of items per page
    public string? RefId { get; set; } // Reference ID for additional context
    public string? SearchQuery { get; set; } // Search query for filtering
    public string? TotalCount { get; set; } // Total count as a string (optional)
    public int PageNumber => PageIndex; // Alias for PageIndex

    // Constructor to initialize pagination with total items and page size
    public Pagination(int? total = null, int pageIndex = 1, int pageSize = 10)
    {
        Total = total;
        PageIndex = pageIndex < 1 ? 1 : pageIndex; // Ensure PageIndex is at least 1
        PageSize = pageSize < 1 ? 10 : pageSize; // Ensure PageSize is at least 1
        CalculateTotalPages();
    }

    // Method to calculate the total number of pages
    private void CalculateTotalPages()
    {
        if (Total.HasValue && Total > 0 && PageSize > 0)
        {
            TotalPages = (int)Math.Ceiling((double)Total.Value / PageSize);
        }
        else
        {
            TotalPages = 0;
        }
    }

    // Method to validate and adjust the current page index
    public void ValidatePageIndex()
    {
        if (TotalPages.HasValue && PageIndex > TotalPages)
        {
            PageIndex = TotalPages.Value; // Adjust to the last page if out of range
        }
    }

    // Method to update pagination dynamically
    public void UpdatePagination(int? total, int pageIndex, int pageSize)
    {
        Total = total;
        PageIndex = pageIndex < 1 ? 1 : pageIndex;
        PageSize = pageSize < 1 ? 10 : pageSize;
        CalculateTotalPages();
        ValidatePageIndex();
    }
}