
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext ApplicationDbContext { get; set; }

        public MoviesController()
        {
            ApplicationDbContext = new ApplicationDbContext();
        }

        public ViewResult Index()
        {
            var movies = ApplicationDbContext.Movies.Include(m => m.Genre);
            return View("MovieIndex", movies);
        }

        public ActionResult Details(int id)
        {
            var movie = ApplicationDbContext.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View("MovieDetails", movie);
        }

        public ActionResult New()
        {
            var genres = ApplicationDbContext.Genres;

            var viewModel = new MovieFormViewModel
            {
                Genres = genres,
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = ApplicationDbContext.Movies.Single(m => m.Id == id);

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = ApplicationDbContext.Genres,
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = System.DateTime.Now;
                ApplicationDbContext.Movies.Add(movie);
            }
            else
            {
                var movieInDb = ApplicationDbContext.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            ApplicationDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" },
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        [Route("movies/released/{year:range(1900,2999)}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ApplicationDbContext.Dispose();
        }
    }
}