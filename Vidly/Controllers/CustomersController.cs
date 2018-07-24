using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private List<Customer> _customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Customer 1" },
            new Customer { Id = 2, Name = "Customer 2" },
            new Customer { Id = 3, Name = "Customer 3" },
            new Customer { Id = 4, Name = "Customer 4" },
        };

        // GET: Customers
        public ActionResult Index()
        {
            return View("CustomerIndex", _customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View("CustomerDetails", customer);
        }
    }
}