using System.Collections.Generic;
using System.Threading.Tasks;
using MvcMovie.Features.Movies.Models;

namespace MvcMovie.Features.Movies.Services
{
    public interface IMovieService
    {
       Task<IEnumerable<Movie>> GetAllAsync();
       Task<Movie?> GetByIdAsync(int id);
       Task AddAsync(Movie movie);
       Task UpdateAsync(Movie movie);
       Task DeleteAsync(int id);
    }
}
