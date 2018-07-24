using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private List<Movie> Movies { get; set; }

        public MoviesController()
        {
            Movies = new List<Movie>
            {
                new Movie { Id = 1, Name = "Movie 1" },
                new Movie { Id = 2, Name = "Movie 2" },
                new Movie { Id = 3, Name = "Movie 3" },
            };
        }

        public ViewResult Index()
        {
            return View("MovieIndex", Movies);
        }

        public ActionResult Details(int id)
        {
            var movie = Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View("MovieDetails", movie);
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
    }
}