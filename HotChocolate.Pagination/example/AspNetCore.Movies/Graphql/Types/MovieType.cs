using AspNetCore.Movies.Infrastructure.Tables;
using HotChocolate.Types;

namespace AspNetCore.Movies.Graphql.Types
{
    public class MovieType : ObjectType<Movie>
    {
        protected override void Configure(IObjectTypeDescriptor<Movie> descriptor)
        {
            descriptor
                .Field(x => x.Title)
                .Type<StringType>();

            descriptor
                .Field(x => x.Poster)
                .Type<StringType>();

            descriptor
                .Field(x => x.Actors)
                .Type<StringType>();

            descriptor
                .Field(x => x.Year)
                .Type<StringType>();

            descriptor
                .Field(x => x.Writer)
                .Type<StringType>();
        }
    }
}