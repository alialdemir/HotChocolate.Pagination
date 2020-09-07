using AspNetCore.Movies.Graphql.Queries;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Movies
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
              .AddGraphQL(
                     SchemaBuilder
                         .New()
                         .AddQueryType(d => d.Name("Query"))
                         .AddMutationType(d => d.Name("Mutation"))

                         .AddType<MovieQuery>()

                         .Create()
                 );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePlayground();
            app.UseGraphQL("/GraphQl");
        }
    }
}