using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace IMS.Domain.Utilities;

public static class Helper
{

    private static readonly SequentialGuidValueGenerator _generator = new();
    public static Guid NewGuid() => _generator.Next(null);

    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int? page, int? perPage, ref int total,
        ref int pages)
    {
        if (pages < 0) throw new ArgumentOutOfRangeException(nameof(pages));

        total = source.Count();
        pages = (int)Math.Ceiling((double)(total / (double)perPage));
        return source.Skip((int)((page - 1) * perPage)).Take((int)perPage);
    }

    public static Pagination Combine(this Pagination pagination, int total, int totalPages)
    {
        pagination.Total = total;
        pagination.TotalPages = totalPages;
        return pagination;
    }
}
