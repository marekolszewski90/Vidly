
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext ApplicationDbContext { get; set; }

        public CustomersController()
        {
            ApplicationDbContext = new ApplicationDbContext();
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            return ApplicationDbContext.Customers
                .Include(c => c.MembershipType)
                .Select(Mapper.Map<Customer, CustomerDto>);
        }

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = ApplicationDbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            ApplicationDbContext.Customers.Add(customer);
            ApplicationDbContext.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = ApplicationDbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            ApplicationDbContext.SaveChanges();
        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = ApplicationDbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            ApplicationDbContext.Customers.Remove(customerInDb);
            ApplicationDbContext.SaveChanges();
        }
    }
}
