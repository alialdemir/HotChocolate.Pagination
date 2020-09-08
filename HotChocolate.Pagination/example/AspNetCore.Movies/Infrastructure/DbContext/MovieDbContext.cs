using AspNetCore.Movies.Infrastructure.Tables;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.Movies.Infrastructure.DbContext
{
    public class MovieDbContext
    {
        public MovieDbContext()
        {
            Movies = new List<Tables.Movie>
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
                    Actors =   "Amitabh Bachchan, Rani Mukerji, Shernaz Patel, Ayesha Kapoor",
                    Poster =    "https://m.media-amazon.com/images/M/MV5BNTI5MmE5M2UtZjIzYS00M2JjLWIwNDItYTY2ZWNiODBmYTBiXkEyXkFqcGdeQXVyNjQ2MjQ5NzM@._V1_SX300.jpg",
                    Writer =  "Sanjay Leela Bhansali (screenplay), Bhavani Iyer (English dialogue), Bhavani Iyer (screenplay), Prakash Kapadia (dialogue), Prakash Kapadia (screenplay)",
                    Title =   "Black",
                    Year = "2005",
                },
                new Tables.Movie
                {
                    Actors =  "Carl Sagan, Jaromír Hanzlík, Jonathan Fahn, Alan Belod",
                    Poster =  "https://m.media-amazon.com/images/M/MV5BMTY4MGQyNjgtMzdmZS00MjQ5LWIyMzItYjYyZmQzNjVhYjMyXkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_SX300.jpg",
                    Writer = "Cosmos",
                    Title =   "Ann Druyan, Carl Sagan, Steven Soter",
                    Year =  "1980",
                },
                new Tables.Movie
                {
                    Actors =  "Katie Holmes, Sarah Polley, Suzanne Krull, Desmond Askew",
                    Poster =  "https://m.media-amazon.com/images/M/MV5BNzJhNGExMDYtMWEyOS00MzMxLWJiYTYtMDYxNTRmNjdjM2VlXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg",
                    Writer =  "John August",
                    Title =    "Go",
                    Year =   "1999",
                },
                new Tables.Movie
                {
                    Actors =  "Leonardo DiCaprio, Kate Winslet, Billy Zane, Kathy Bates",
                    Poster = "https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E5My00ZTliLWIzOTUtMTY4ZGI1YjdiNjk3XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_SX300.jpg",
                    Writer = "James Cameron",
                    Title =  "Titanic",
                    Year =  "1997",
                },
                new Tables.Movie
                {
                    Actors =  "Russell Crowe, Ed Harris, Jennifer Connelly, Christopher Plummer",
                    Poster = "https://m.media-amazon.com/images/M/MV5BMzcwYWFkYzktZjAzNC00OGY1LWI4YTgtNzc5MzVjMDVmNjY0XkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg",
                    Writer =  "Akiva Goldsman, Sylvia Nasar (book)",
                    Title =   "A Beautiful Mind",
                    Year =  "2001",
                },
                new Tables.Movie
                {
                    Actors =  "Ingrid Bolsø Berdal, Rolf Kristian Larsen, Tomas Alf Larsen, Endre Martin Midtstigen",
                    Poster = "https://m.media-amazon.com/images/M/MV5BMjA1NDcwOTA1MV5BMl5BanBnXkFtZTcwMzE4NjkzMQ@@._V1_SX300.jpg",
                    Writer =  "Thomas Moldestad (story), Martin Sundland (story), Roar Uthaug (story), Jan Eirik Langoen (idea), Magne Lyngner (idea)",
                    Title =  "Cold Prey",
                    Year = "2006",
                },
                new Tables.Movie
                {
                    Actors =  "Nick Mancuso, Phillip Jarrett, Carrie-Anne Moss, John Vernon",
                    Poster =  "https://m.media-amazon.com/images/M/MV5BYzUzOTA5ZTMtMTdlZS00MmQ5LWFmNjEtMjE5MTczN2RjNjE3XkEyXkFqcGdeQXVyNTc2ODIyMzY@._V1_SX300.jpg",
                    Writer = "Grenville Case",
                    Title =  "Matrix",
                    Year =   "1993",
                },
                new Tables.Movie
                {
                    Actors =  "Kevin Bacon, Billy Crudup, Robert De Niro, Ron Eldard",
                    Poster =  "https://m.media-amazon.com/images/M/MV5BMTc4OTUzNzc0MV5BMl5BanBnXkFtZTgwMjE4ODYxMTE@._V1_SX300.jpg",
                    Writer = "Lorenzo Carcaterra (book), Barry Levinson (screenplay)",
                    Title =  "Sleepers",
                    Year =   "1996",
                },
                new Tables.Movie
                {
                    Actors = "Nina Hoss, Ronald Zehrfeld, Nina Kunzendorf, Trystan Pütter",
                    Poster =  "https://m.media-amazon.com/images/M/MV5BNTc3ODA4MTIxOV5BMl5BanBnXkFtZTgwNTAzOTAxNjE@._V1_SX300.jpg",
                    Writer = "Christian Petzold (screenplay), Harun Farocki, Hubert Monteilhet (novel)",
                    Title =   "Phoenix",
                    Year =  "2014",
                },
            }.AsQueryable();
        }

        public IQueryable<Movie> Movies { get; private set; }
    }
}