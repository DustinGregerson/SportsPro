

using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private SportsProContext context;
        public CustomerController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("customers")]
        public IActionResult List()
        {
            List<Customer> customers;
            customers = context.Customers.ToList();

            // the is the code that Pass TempData message to the view
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View(customers);
        }
        //Adding new Customer to table
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            Customer customer = new Customer();
            ViewBag.Contries=context.Countries.ToList();
            return View("AddEdit", customer);
        }
        //Edit the Customer
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer customer = context.Customers.Find(id);
            ViewBag.Action = "Edit";
            ViewBag.Contries = context.Countries.ToList();
            return View("AddEdit", customer);
        }

        [HttpPost]
        public IActionResult AddEdit(Customer customer)
        {
            customer.Country=context.Countries.Find(customer.CountryID);

            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    context.Customers.Add(customer);

                    //Here the code for tempData
                    TempData["SuccessMessage"] = "Customer added successfully!";
                }
                // the customer from database have to Added
                else
                {
                    context.Customers.Update(customer);

                    //Here the code for tempData
                    TempData["SuccessMessage"] = "Customer Updated successfully!";
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Contries = context.Countries.ToList();
                return View("AddEdit", customer);
            }
        }

        //here is the code  for Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Customer customer = context.Customers.Find(id);

            if (customer == null)
            {
                return RedirectToAction("List", "Customers");
            }
            else
            {

                return View(customer);
            }
        }
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            if (customer == null)
            {
                return RedirectToAction("List");
            }
            else
            {
                context.Customers.Remove(customer);
                context.SaveChanges();

                //TempData Message
                TempData["SuccessMessage"] = "Customer deleted successfully!";

                return RedirectToAction("List");
            }
        }
    }
}
