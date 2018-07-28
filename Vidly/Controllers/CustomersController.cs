using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

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

        public ActionResult New()
        {
            var membershipTypes = ApplicationDbContext.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
                ApplicationDbContext.Customers.Add(customer);
            else
            {
                var customerInDb = ApplicationDbContext.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                //TryUpdateModel(customerInDb);
            }

            ApplicationDbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = ApplicationDbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = ApplicationDbContext.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ApplicationDbContext.Dispose();
        }
    }
}