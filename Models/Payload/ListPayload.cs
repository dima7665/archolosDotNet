using archolosDotNet.Models.Pagination;

namespace archolosDotNet.Models.Payload;

public class ListPayload<F>
{
    public F? filter { get; set; }

    public PaginationPayload pagination { get; set; } = new();
}
