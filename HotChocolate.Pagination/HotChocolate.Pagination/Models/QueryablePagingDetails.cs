using HotChocolate.Pagination.Abstract;
using Newtonsoft.Json;

namespace HotChocolate.Pagination.Models
{
    public class QueryablePagingDetails : IPageInfo
    {
        public QueryablePagingDetails(long? totalCount, int? pageNumber, int? limit)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;

            if (totalCount.HasValue && limit.HasValue)
            {
                TotalPages = totalCount.Value / limit.Value;

                if (TotalCount % limit > 0)
                    TotalPages++;
            }

            Limit = limit;
        }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage => PageNumber.HasValue && PageNumber > 1;

        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNextPage => PageNumber.HasValue && TotalPages.HasValue && PageNumber + 1 <= TotalPages;

        /// <summary>
        /// Total count
        /// </summary>
        public long? TotalCount { get; set; }

        /// <summary>
        /// Page number
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Total pages
        /// </summary>
        public long? TotalPages { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        [JsonIgnore]
        public int Offset
        {
            get
            {
                if (!PageNumber.HasValue || !Limit.HasValue)
                    return 0;

                return Limit.Value * (PageNumber.Value - 1);
            }
        }
    }
}