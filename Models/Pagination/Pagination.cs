using System.ComponentModel.DataAnnotations;

namespace archolosDotNet.Models.Pagination;

public class PaginationPayload
{
    public const int maxSize = 20;

    private int _perPage = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Must be greater than 0")]
    public int page { get; set; } = 1;

    [Range(1, maxSize, ErrorMessage = "Must be between 1 and 20")]
    public int perPage
    {
        get => _perPage;
        set => _perPage = value > maxSize ? maxSize : value;
    }
}

public class PagedResult<T>
{
    public IEnumerable<T> data { get; set; } = [];

    public PaginationData pagination { get; set; } = new();
}

public class PaginationData
{
    public int currentPage { get; set; }

    public int count { get; set; }

    public int perPage { get; set; }
}
