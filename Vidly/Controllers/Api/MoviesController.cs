using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        public ApplicationDbContext ApplicationDbContext { get; set; }

        public MoviesController()
        {
            ApplicationDbContext = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetMovies(string query = null)
        {
            var movieQuery = ApplicationDbContext.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailble > 0);

            if (!string.IsNullOrEmpty(query))
                movieQuery = movieQuery.Where(m => m.Name.Contains(query));

            var movies = movieQuery
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movies);
        }

        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = ApplicationDbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            ApplicationDbContext.Movies.Add(movie);
            ApplicationDbContext.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

            Movie movie = ApplicationDbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            Mapper.Map(movieDto, movie);
            ApplicationDbContext.SaveChanges();

            //return Ok(movieDto);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = ApplicationDbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            ApplicationDbContext.Movies.Remove(movie);
            ApplicationDbContext.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ApplicationDbContext.Dispose();
        }
    }
}
