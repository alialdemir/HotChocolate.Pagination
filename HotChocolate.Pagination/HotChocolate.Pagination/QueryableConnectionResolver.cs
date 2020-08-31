using HotChocolate.Language;
using HotChocolate.Pagination.Abstract;
using HotChocolate.Pagination.Models;
using HotChocolate.Resolvers;
using HotChocolate.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotChocolate.Pagination
{
    public class QueryableConnectionResolver<T>
        : IConnectionResolver
    {
        private const string _totalCount = "__totalCount";
        private const string _position = "__position";

        private readonly IQueryable<T> _source;
        private readonly IDictionary<string, object> _properties;
        private readonly QueryablePagingDetails _pageDetails;

        public QueryableConnectionResolver(
            IQueryable<T> source,
            PaginationDetails paginationDetails)
        {
            if (paginationDetails == null)
            {
                throw new ArgumentNullException(nameof(paginationDetails));
            }

            _source = source ?? throw new ArgumentNullException(nameof(source));
            _pageDetails = DeserializePagingDetails(paginationDetails);

            _properties = paginationDetails.Properties
                ?? new Dictionary<string, object>();
        }

        public Task<Connection<T>> ResolveAsync(
            CancellationToken cancellationToken)
        {
            return Task.Run(() => Create(), cancellationToken);
        }

        private Connection<T> Create()
        {
            if (!_pageDetails.TotalCount.HasValue)
            {
                _pageDetails.TotalCount = _source.Count();
            }

            _properties[_totalCount] = _pageDetails.TotalCount.Value;

            IReadOnlyList<QueryableEdge<T>> selectedEdges = GetSelectedEdges();

            var pageInfo = new QueryablePagingDetails(HasNextPage(_pageDetails.TotalCount,
                                                                  _pageDetails.Limit,
                                                                  _pageDetails.PageNumber), _pageDetails.TotalCount, _pageDetails.PageNumber, _pageDetails.Limit);

            return new Connection<T>(pageInfo, selectedEdges);
        }

        private bool HasNextPage(long? count, int? limit, int? pageNumber)
        {
            if (!limit.HasValue || !pageNumber.HasValue || !count.HasValue)
                return false;

            if (pageNumber.Value <= 0 || limit.Value <= 0) return false;
            return !((pageNumber.Value - 1) > count.Value / limit.Value);
        }

        private IReadOnlyList<QueryableEdge<T>> GetSelectedEdges()
        {
            var list = new List<QueryableEdge<T>>();
            List<T> edges = GetEdgesToReturn(
                _source, _pageDetails);

            for (int i = 0; i < edges.Count; i++)
            {
                int index = i;
                _properties[_position] = index;
                string cursor = Base64Serializer.Serialize(_properties);
                list.Add(new QueryableEdge<T>(cursor, edges[i], index));
            }

            return list;
        }

        private List<T> GetEdgesToReturn(
            IQueryable<T> allEdges,
            QueryablePagingDetails pagingDetails)
        {
            IQueryable<T> edges = ApplyCursorToEdges(allEdges,
                                                     pagingDetails.Limit,
                                                     pagingDetails.PageNumber);

            return edges.ToList();
        }

        protected virtual IQueryable<T> ApplyCursorToEdges(
            IQueryable<T> allEdges, int? limit, int? pageNumber)
        {
            IQueryable<T> edges = allEdges;

            if (pageNumber.HasValue && limit.HasValue)
            {
                edges = edges
                    .Skip(limit.Value * (pageNumber.Value - 1))
                    .Take(limit.Value);
            }

            return edges;
        }

        private QueryablePagingDetails DeserializePagingDetails(
            PaginationDetails pagination)
        {
            long? totalCount = GetTotalCountFromCursor(_properties);

            return new QueryablePagingDetails(HasNextPage(totalCount,
                                                          pagination.Limit,
                                                          pagination.PageNumber), totalCount, pagination.PageNumber, pagination.Limit);
        }

        private long? GetTotalCountFromCursor(IDictionary<string, object> properties)
        {
            if (properties == null)
            {
                return null;
            }

            return Convert.ToInt32(properties[_totalCount]);
        }

        public ValueTask<IConnection> ResolveAsync(IMiddlewareContext context,
                                                   object source,
                                                   Types.Relay.ConnectionArguments arguments,
                                                   bool withTotalCount,
                                                   CancellationToken cancellationToken)
        {
            return new ValueTask<IConnection>(Create());
        }
    }
}