using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class RegistrationController : Controller
    {
        private SportsProContext context;
        public RegistrationController(SportsProContext ctx)
        {
            context = ctx;
        }

        public ViewResult GetCustomer()
        {
            List<Customer> customers = new List<Customer>();
            customers = context.Customers.ToList();
            ViewBag.Customers = customers;
            return View();
        }
        [Route("Registrations")]
        public IActionResult Registrations(Customer customer)
        {

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            RegistrationViewModel viewModel = new RegistrationViewModel(context, customer.CustomerID);
            viewModel.getRegistrationsForCustomer(context);
            viewModel.getUnregisteredProductsLists(context);
            HttpContext.Session.SetString("customerID", customer.CustomerID.ToString());
            return View(viewModel);
        }
        
        public IActionResult registerProduct(RegistrationViewModel viewModel)
        {
            Customer customer = new Customer();
            customer.CustomerID = int.Parse(HttpContext.Session.GetString("customerID"));

            Product product = new Product();
            product = context.Products.Find(viewModel.registerProductId);

            Registration registration = new Registration();
            registration.CustomerID = customer.CustomerID;
            registration.ProductID = product.ProductID;

            context.Add(registration);
            context.SaveChanges();
            TempData["SuccessMessage"] = "Registration added successfully!";
            return RedirectToAction("Registrations", customer);
        }
        public IActionResult Delete(int id)
        {
            Customer customer = new Customer();
            customer.CustomerID = int.Parse(HttpContext.Session.GetString("customerID"));
            Product product = new Product();
            product = context.Products.Find(id);
            Registration registration = context.Registrations
                           .Where(Q => Q.CustomerID == customer.CustomerID)
                           .Where(Q=>Q.ProductID==product.ProductID)
                           .FirstOrDefault();
            context.Remove(registration);
            context.SaveChanges();
            TempData["SuccessMessage"] = "Registration deleted successfully!";
            return RedirectToAction("Registrations", customer);

        }
    }
}
