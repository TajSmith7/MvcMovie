using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Services
{
    public class MovieService : IMovieService
    
    {

        private readonly MvcMovieContext _db;

        public MovieService(MvcMovieContext db) => _db = db;
        
        public async Task<IEnumerable<Movie>> GetAllAsync() => await _db.Movie.ToListAsync();


        public async Task<Movie?> GetByIdAsync(int id) => await _db.Movie.FirstOrDefaultAsync(movie => movie.Id == id);

        public async Task AddAsync(Movie movie)
        {
            _db.Movie.Add(movie);
            await _db.SaveChangesAsync();

        }

        public async Task UpdateAsync(Movie movie)
        {
            _db.Movie.Update(movie);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Movie.FindAsync(id);
            if (entity != null)
            {
                _db.Movie.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
