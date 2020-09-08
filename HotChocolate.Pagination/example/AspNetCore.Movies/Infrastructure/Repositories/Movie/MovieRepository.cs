using AspNetCore.Movies.Infrastructure.DbContext;
using System.Linq;

namespace AspNetCore.Movies.Infrastructure.Repositories.Movie
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _movieDbContext;

        public MovieRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        /// <summary>
        /// Get movies
        /// </summary>
        /// <returns>List of movie</returns>
        public IQueryable<Tables.Movie> Movies()
        {
            return _movieDbContext.Movies;
        }
    }
}