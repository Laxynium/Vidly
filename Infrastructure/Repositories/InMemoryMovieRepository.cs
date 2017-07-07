using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.Repositories;
namespace Infrastructure.Repositories
{
    public class InMemoryMovieRepository:IMovieRepository
    {
        private readonly ISet<Movie> _movies = new HashSet<Movie>()
        {
            new Movie()
            {
                Id=1,
                Name = "Film1",
                Genre = new Genre {Id = 1,Name = "Akcji" },
                DateAdded = DateTime.Now,
                NumberInStock = 5,
                ReleaseDate = DateTime.Today,
                AvailableMovies = 5,
                GenreId = 1
            }
        };
        
        public Movie Get(int id)
        {
            return _movies.First(m => m.Id == id);
        }

        public IEnumerable<Movie> GetMovies(string query = null)
        {
            return _movies;
        }

        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }

        public void Update(Movie movie)
        {
            
        }

        public void Remove(int id)
        {
            
        }
    }
}
