
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Models;
using MvcMovie.Helpers;
using MvcMovie.Features.Movies.Models;
using MvcMovie.Features.Movies.Services;

namespace MvcMovie.Features.Movies.Controllers
{
    // /movies
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movies;

        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieService movies, ILogger<MoviesController> logger)
        {
            _movies = movies;
            _logger = logger;
        }

        // GET: Movies
        [HttpGet("")]
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IEnumerable<Movie> all = await _movies.GetAllAsync();
            IEnumerable<Movie> movies = all;
            IEnumerable<string?> genreQuery = all.Select(movie => movie.Genre).Distinct();
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString, StringComparison.OrdinalIgnoreCase));
                _logger.Info("User searched for movies with letters: {searchString}", searchString);
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(movie => movie.Genre == movieGenre);
                _logger.Info("User filtered movies by genre: {movieGenre}", movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(genreQuery),
                Movies = movies.ToList()
            };

            return View(movieGenreVM);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Movies/Details/5
        [HttpGet("details/{id:int}", Name = "MovieDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movies.GetByIdAsync(id);
            _logger.Info("Viewing details for movie with ID {id}.", id);
            return View(movie);
        }

        // GET: Movies/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            _logger.Info("Accessed the Create Movie page.");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (!ModelState.IsValid)
                {
                _logger.Warn("Invalid model state while creating a movie.");
                return View(movie);
            }
            else
            {

                _logger.Info("Creating a new movie with title: {Title}", movie.Title);
                await _movies.AddAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: Movies/Edit/5
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movies.GetByIdAsync(id);
            _logger.Info("Accessed the Edit page for movie with ID {id}.", id);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                _logger.Warn("Invalid model state while editing movie with ID: {id}", id);
                return View(movie);
            }
            await _movies.UpdateAsync(movie);
            _logger.Info("Updated movie with ID: {id}", id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Delete/5
        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movies.GetByIdAsync(id);
            _logger.Info("Accessed the Delete page for movie with ID {id}.", id);
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost("delete/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movies.DeleteAsync(id);
            _logger.Info("Deleted movie with ID: {id}", id);
            return RedirectToAction(nameof(Index));
        }

        //Get: /movies/bygenre/comedy
        [HttpGet("bygenre/{genre}")]
        public async Task<IActionResult> ByGenre(string genre)
        {
            IEnumerable<Movie> all = await _movies.GetAllAsync();
            IEnumerable<Movie> movies = all.Where(movie => movie.Genre != null && string.Equals(movie.Genre, genre, StringComparison.OrdinalIgnoreCase));
            MovieGenreViewModel viewModel = new MovieGenreViewModel
            {
                Genres = new SelectList(all.Select(m => m.Genre).Distinct()),
                Movies = movies.ToList(),
                MovieGenre = genre
            };
            _logger.Info("User filtered movies by genre: {genre}", genre);

            return View("Index", viewModel);
        }

        //GET: /movies/released/2010/5
        [HttpGet("released/{year:int:min(1900)}/{month:int:range(1,12)?}")]
        public async Task<IActionResult> Released(int year, int month)
        {
            IEnumerable<Movie> all = await _movies.GetAllAsync();
            IEnumerable<Movie> movies = all.Where(movie => movie.ReleaseDate.Year == year && (month == 0 ? true : movie.ReleaseDate.Month == month);
            MovieGenreViewModel viewModel = new MovieGenreViewModel
            {
                Genres = new SelectList(all.Select(m => m.Genre).Distinct()),
                Movies = movies.ToList()
            };
            _logger.Info("User filtered movies released in Year: {year}, Month: {month}", year, month);

            return View("Index", viewModel);
        }
    }
}
