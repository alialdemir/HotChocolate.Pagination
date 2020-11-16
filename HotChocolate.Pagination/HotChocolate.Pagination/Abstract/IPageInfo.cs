namespace HotChocolate.Pagination.Abstract
{
    public interface IPageInfo
    {
        long? TotalCount { get; set; }
        int? PageNumber { get; set; }
        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }
        int? Limit { get; set; }
    }
}