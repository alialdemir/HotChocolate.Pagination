using HotChocolate.Pagination.Abstract;
using HotChocolate.Pagination.Models;
using HotChocolate.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolate.Pagination
{
    public delegate IConnectionResolver ConnectionResolverFactory<in T>(
          IQueryable<T> source,
          PaginationDetails pagingDetails);

    public class QueryableConnectionMiddleware<T>
    {
        private readonly FieldDelegate _next;
        private readonly ConnectionResolverFactory<T> _createConnectionResolver;

        public QueryableConnectionMiddleware(
            FieldDelegate next,
            ConnectionResolverFactory<T> createConnectionResolver)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _createConnectionResolver = createConnectionResolver
            ?? CreateConnectionResolver;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            await _next(context).ConfigureAwait(false);

            var pagingDetails = new PaginationDetails
            {
                Limit = context.Argument<int?>("limit"),
                PageNumber = context.Argument<int?>("pageNumber"),
            };

            IQueryable<T> source = null;

            if (context.Result is PageableData<T> p)
            {
                source = p.Source;
                pagingDetails.Properties = p.Properties;
            }

            if (context.Result is IQueryable<T> q)
            {
                source = q;
            }
            else if (context.Result is IEnumerable<T> e)
            {
                source = e.AsQueryable();
            }

            if (source != null)
            {
                IConnectionResolver connectionResolver = _createConnectionResolver(
                    source, pagingDetails);

                context.Result = await connectionResolver
                    .ResolveAsync(context, source)
                    .ConfigureAwait(false);
            }
        }

        private static IConnectionResolver CreateConnectionResolver(
            IQueryable<T> source, PaginationDetails pagingDetails)
        {
            return new QueryableConnectionResolver<T>(source, pagingDetails);
        }
    }
}