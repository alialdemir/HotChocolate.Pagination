using AspNetCore.Movies.Graphql.Queries;
using AspNetCore.Movies.Graphql.Types;
using AspNetCore.Movies.Infrastructure.DbContext;
using AspNetCore.Movies.Infrastructure.Repositories.Movie;
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
                .AddScoped<MovieDbContext>()
                .AddScoped<IMovieRepository, MovieRepository>()
                .AddGraphQL(s => SchemaBuilder
                                .New()
                                .AddServices(s)
                                .AddType<MovieType>()
                                .AddQueryType<MovieQuery>()
                                .Create());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePlayground()
               .UseGraphQL();
        }
    }
}