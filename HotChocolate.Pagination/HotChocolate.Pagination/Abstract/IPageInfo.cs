namespace HotChocolate.Pagination.Abstract
{
    public interface IPageInfo
    {
        long? TotalCount { get; set; }
        int? PageNumber { get; set; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        int? Limit { get; set; }
    }
}