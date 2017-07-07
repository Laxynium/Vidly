using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dtos;

namespace Infrastructure.Services
{
    public interface IMovieService:IService
    {
        MovieDto GetMovie(int Id);
        IEnumerable<MovieDto> GetMovies(string query=null);
        void AddMovie(MovieDto movie);
        void UpdateMovie(MovieDto movie);
        void Delete(int Id);
    }
}
