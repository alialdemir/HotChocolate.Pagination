using AspNetCore.Movies.Graphql.Types;
using AspNetCore.Movies.Infrastructure.Repositories.Movie;
using AspNetCore.Movies.Infrastructure.Tables;
using HotChocolate.Pagination;
using System.Linq;

namespace AspNetCore.Movies.Graphql.Queries
{
    public class MovieQuery
    {
        private readonly IMovieRepository _movieRepository;

        public MovieQuery(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Get movies
        /// </summary>
        /// <returns>List of movie</returns>
        [UsePagination(SchemaType = typeof(MovieType))]
        public IQueryable<Movie> GetMovies()
        {
            return _movieRepository.Movies();
        }
    }
}