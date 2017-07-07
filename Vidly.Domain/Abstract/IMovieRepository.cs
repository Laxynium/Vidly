using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidly.Domain.Entities;

namespace Vidly.Domain.Abstract
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        Movie GetMovie(int id);

        void AddMovie(Movie movie);

        void UpdateMovie(Movie movie);

        void RemoveMovie(int id);
    }
}
