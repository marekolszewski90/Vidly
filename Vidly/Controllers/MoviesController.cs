using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
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