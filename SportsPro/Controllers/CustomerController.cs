﻿

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

        public IActionResult List()
        {
            List<Customer>
    customers;
            customers = context.Customers.ToList();

            return View(customers);
        }
        //Adding new Customer to table
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            Customer customer = new Customer();
            return View("AddEdit", customer);
        }
        //Edit the Customer
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer customer = context.Customers.Find(id);
            ViewBag.Action = "Edit";
            return View("AddEdit", customer);
        }

        [HttpPost]
        public IActionResult AddEdit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    context.Customers.Add(customer);
                }
                // the customer from database have to Added
                else
                {
                    context.Customers.Update(customer);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
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
                return RedirectToAction("List");
            }
        }
    }
}