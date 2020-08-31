using HotChocolate.Types.Relay;

namespace HotChocolate.Pagination.Models
{
    internal class QueryableEdge<T> : Edge<T>
    {
        public QueryableEdge(string cursor, T node, int index)
            : base(node, cursor)
        {
            Index = index;
        }

        public int Index { get; set; }
    }
}