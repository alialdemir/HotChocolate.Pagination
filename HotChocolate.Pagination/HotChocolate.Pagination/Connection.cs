using HotChocolate.Types.Relay;
using System.Collections.Generic;

namespace HotChocolate.Pagination
{
    public class Connection<T> : Abstract.IConnection
    {
        public Connection(Abstract.IPageInfo pageInfo, IReadOnlyList<IEdge> edges)
        {
            PageInfo = pageInfo;
            Edges = edges;
        }

        public Abstract.IPageInfo PageInfo { get; }

        public IReadOnlyList<IEdge> Edges { get; }
    }
}