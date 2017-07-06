using System.Collections.Generic;
using Core.Domain;

namespace Core.Repositories
{
    public interface IMovieRepository:IRepository
    {
        Movie Get(int id);
        IEnumerable<Movie> GetMovies(string query = null);
        void Add(Movie movie);
        void Update(Movie movie);
        void Remove(int id);
    }
}
