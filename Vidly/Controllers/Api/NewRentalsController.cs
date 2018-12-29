using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        public ApplicationDbContext ApplicationDbContext { get; set; }

        public NewRentalsController()
        {
            ApplicationDbContext = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult PostRental(NewRentalDto newRentalDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Customer customer = ApplicationDbContext.Customers.Single(c => c.Id == newRentalDto.CustomerId);

            var movies = ApplicationDbContext.Movies.Where(m => newRentalDto.MoviesIds.Contains(m.Id));

            foreach (Movie movie in movies)
            {
                if (movie.NumberAvailble == 0)
                    return BadRequest($"Movie with ID '{movie.Id}' not availble.");
                movie.NumberAvailble--;

                ApplicationDbContext.Rentals.Add(new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                    DateReturned = null,
                });
            }

            ApplicationDbContext.SaveChanges();
            return Ok();
        }
    }
}
