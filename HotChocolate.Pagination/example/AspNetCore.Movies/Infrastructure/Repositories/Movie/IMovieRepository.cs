using System.Linq;

namespace AspNetCore.Movies.Infrastructure.Repositories.Movie
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Get movies
        /// </summary>
        /// <returns>List of movie</returns>
        IQueryable<Tables.Movie> Movies();
    }
}