using HotChocolate.Pagination.Abstract;

namespace HotChocolate.Pagination.Models
{
    public class QueryablePagingDetails : IPageInfo
    {
        public QueryablePagingDetails(bool hasNextPage, long? totalCount, int? pageNumber, int? limit)
        {
            HasNextPage = hasNextPage;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            Limit = limit;
        }

        public bool HasNextPage { get; set; }

        public long? TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public int? Limit { get; set; }
    }
}