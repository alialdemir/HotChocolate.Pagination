using HotChocolate.Resolvers;
using HotChocolate.Types.Relay;
using System.Threading;
using System.Threading.Tasks;

namespace HotChocolate.Pagination.Abstract
{
    public interface IConnectionResolver
    {
        ValueTask<IConnection> ResolveAsync(IMiddlewareContext context, object source, ConnectionArguments arguments = default, bool withTotalCount = false, CancellationToken cancellationToken = default);
    }
}