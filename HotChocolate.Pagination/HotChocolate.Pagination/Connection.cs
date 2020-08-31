using HotChocolate.Pagination.Abstract;
using System.Collections.Generic;

namespace HotChocolate.Pagination
{
    public class Connection<T> : IConnection
    {
        public Connection(IPageInfo pageInfo, IReadOnlyList<Types.Relay.Edge<T>> edges)
        {
            PageInfo = pageInfo;
            Edges = edges;
        }

        public IPageInfo PageInfo { get; }

        public IReadOnlyList<Types.Relay.IEdge> Edges { get; }
    }
}