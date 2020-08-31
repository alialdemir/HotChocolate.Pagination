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

        public QueryableConnectionResolver(IQueryable<T> source,
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

        public Task<Connection<T>> ResolveAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => Create(), cancellationToken);
        }

        public ValueTask<IConnection> ResolveAsync(IMiddlewareContext context,
                                                   object source,
                                                   HotChocolate.Types.Relay.ConnectionArguments arguments,
                                                   bool withTotalCount,
                                                   CancellationToken cancellationToken)
        {
            return new ValueTask<IConnection>(Create());
        }

        /// <summary>
        /// Creates a connection object
        /// </summary>
        /// <returns>Connection</returns>
        private Connection<T> Create()
        {
            if (!_pageDetails.TotalCount.HasValue)
            {
                _pageDetails.TotalCount = _source.Count();
            }

            _properties[_totalCount] = _pageDetails.TotalCount.Value;

            IReadOnlyList<QueryableEdge<T>> selectedEdges = GetSelectedEdges();

            var pageInfo = new QueryablePagingDetails(_pageDetails.TotalCount,
                                                      _pageDetails.PageNumber,
                                                      _pageDetails.Limit);

            return new Connection<T>(pageInfo, selectedEdges);
        }

        /// <summary>
        /// Creates a IReadOnlyList QueryableEdge object
        /// </summary>
        /// <returns>List of QueryableEdge</returns>
        private IReadOnlyList<QueryableEdge<T>> GetSelectedEdges()
        {
            var list = new List<QueryableEdge<T>>();
            List<T> edges = GetEdgesToReturn(_source, _pageDetails);

            for (int i = 0; i < edges.Count; i++)
            {
                int index = i;
                _properties[_position] = index;
                string cursor = Base64Serializer.Serialize(_properties);
                list.Add(new QueryableEdge<T>(cursor, edges[i], index));
            }

            return list;
        }

        /// <summary>
        /// Get paging edgess
        /// </summary>
        /// <param name="allEdges">Edges</param>
        /// <param name="pagingDetails">Paging  details</param>
        /// <returns>List of edges</returns>
        private List<T> GetEdgesToReturn(IQueryable<T> allEdges,
                                         QueryablePagingDetails pagingDetails)
        {
            IQueryable<T> edges = ApplySkipTakeToEdges(allEdges,
                                                       pagingDetails.Limit,
                                                       pagingDetails.Offset);

            return edges.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="allEdges"></param>
        /// <param name="limit">Item limit</param>
        /// <param name="offset">Offset</param>
        /// <returns></returns>
        protected virtual IQueryable<T> ApplySkipTakeToEdges(IQueryable<T> allEdges,
                                                             int? limit,
                                                             int offset)
        {
            if (limit.HasValue)
            {
                allEdges = allEdges
                                .Skip(offset)
                                .Take(limit.Value);
            }

            return allEdges;
        }

        /// <summary>
        /// PaginationDetails to QueryablePagingDetails object
        /// </summary>
        /// <param name="pagination">Pagination</param>
        /// <returns>QueryablePagingDetails</returns>
        private QueryablePagingDetails DeserializePagingDetails(PaginationDetails pagination)
        {
            long? totalCount = GetTotalCountFromCursor(_properties);

            return new QueryablePagingDetails(totalCount,
                                              pagination.PageNumber,
                                              pagination.Limit);
        }

        /// <summary>
        /// Total item count
        /// </summary>
        /// <param name="properties">Properties</param>
        /// <returns>Total count</returns>
        private long? GetTotalCountFromCursor(IDictionary<string, object> properties)
        {
            if (properties == null)
            {
                return null;
            }

            return Convert.ToInt32(properties[_totalCount]);
        }
    }
}