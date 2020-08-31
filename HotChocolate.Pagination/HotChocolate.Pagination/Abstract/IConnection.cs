using HotChocolate.Types.Relay;
using System.Collections.Generic;

namespace HotChocolate.Pagination.Abstract
{
    public interface IConnection
    {
        IPageInfo PageInfo { get; }
        IReadOnlyList<IEdge> Edges { get; }
    }
}