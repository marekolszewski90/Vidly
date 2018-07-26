using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext ApplicationDbContext { get; set; }

        public CustomersController()
        {
            ApplicationDbContext = new ApplicationDbContext();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = ApplicationDbContext.Customers.Include(c => c.MembershipType);
            return View("CustomerIndex", customers);
        }

        public ActionResult Details(int id)
        {
            var customer = ApplicationDbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View("CustomerDetails", customer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ApplicationDbContext.Dispose();
        }
    }
}