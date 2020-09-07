using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.Movies.Infrastructure.Repositories.Movie
{
    public class MovieRepository : IMovieRepository
    {
        public IQueryable<Tables.Movie> Movies()
        {
            return new List<Tables.Movie>
            {
                new Tables.Movie
                {
                     Actors = "Michael Keaton, Jack Nicholson, Kim Basinger, Robert Wuhl",
                     Poster =  "https://m.media-amazon.com/images/M/MV5BMTYwNjAyODIyMF5BMl5BanBnXkFtZTYwNDMwMDk2._V1_SX300.jpg",
                     Writer ="Bob Kane (Batman characters), Sam Hamm (story), Sam Hamm (screenplay), Warren Skaaren (screenplay)",
                     Title= "Batman",
                     Year = "1989"
                },
                new Tables.Movie
                {
                    Actors =   "Amitabh Bachchan, Rani Mukerji, Shernaz Patel, Ayesha Kapoor" ,
                    Poster =    "https://m.media-amazon.com/images/M/MV5BNTI5MmE5M2UtZjIzYS00M2JjLWIwNDItYTY2ZWNiODBmYTBiXkEyXkFqcGdeQXVyNjQ2MjQ5NzM@._V1_SX300.jpg" ,
                    Writer =  "Sanjay Leela Bhansali (screenplay), Bhavani Iyer (English dialogue), Bhavani Iyer (screenplay), Prakash Kapadia (dialogue), Prakash Kapadia (screenplay)"  ,
                    Title =   "Black" ,
                    Year = "2005"    ,
                },
                new Tables.Movie
                {
                    Actors =  "Carl Sagan, Jaromír Hanzlík, Jonathan Fahn, Alan Belod"  ,
                    Poster =  "https://m.media-amazon.com/images/M/MV5BMTY4MGQyNjgtMzdmZS00MjQ5LWIyMzItYjYyZmQzNjVhYjMyXkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_SX300.jpg"  ,
                    Writer = "Cosmos",
                    Title =   "Ann Druyan, Carl Sagan, Steven Soter"  ,
                    Year =  "1980"   ,
                },
                new Tables.Movie
                {
                    Actors =   ,
                    Poster =    ,
                    Writer =    ,
                    Title =    ,
                    Year =     ,
                },
                new Tables.Movie
                {
                    Actors =   ,
                    Poster =    ,
                    Writer =    ,
                    Title =    ,
                    Year =     ,
                },
                new Tables.Movie
                {
                    Actors =   ,
                    Poster =    ,
                    Writer =    ,
                    Title =    ,
                    Year =     ,
                },
                new Tables.Movie
                {
                    Actors =   ,
                    Poster =    ,
                    Writer =    ,
                    Title =    ,
                    Year =     ,
                },
                new Tables.Movie
                {
                    Actors =   ,
                    Poster =    ,
                    Writer =    ,
                    Title =    ,
                    Year =     ,
                },
                new Tables.Movie
                {
                    Actors =   ,
                    Poster =    ,
                    Writer =    ,
                    Title =    ,
                    Year =     ,
                },
                new Tables.Movie
                {
                    Actors =   ,
                    Poster =    ,
                    Writer =    ,
                    Title =    ,
                    Year =     ,
                },
            }.AsQueryable();
        }
    }
}