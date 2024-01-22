﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {

        private SportsProContext context;
        public IncidentController(SportsProContext ctx)
        {
            context = ctx;
        }
       

        public IActionResult List()
        {
            List<Incident> incidents;
            incidents = context.Incidents
                .Include(Q => Q.Product)
                .Include(Q => Q.Technician)
                .Include(Q => Q.Customer).ToList();
            
            return View(incidents);
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            Incident incident = new Incident();
            ViewBag.Customers = context.Customers.ToList();
            ViewBag.Technicians=context.Technicians.ToList();
            ViewBag.Products = context.Products.ToList();
            return View("AddEdit", incident);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Incident incident = context.Incidents.Find(id);
            ViewBag.Action = "Edit";
            ViewBag.Customers = context.Customers.ToList();
           ViewBag.Technicians=context.Technicians.ToList();
            ViewBag.Products = context.Products.ToList();
            return View("AddEdit", incident);
        }

        [HttpPost]
        public IActionResult AddEdit(Incident incident)
        {
            incident.Customer=context.Customers.Find(incident.CustomerID);
            incident.Technician = context.Technicians.Find(incident.TechnicianID);
            incident.Product = context.Products.Find(incident.ProductID);

            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                {
                    context.Incidents.Add(incident);
                }
               
                else
                {
                    context.Incidents.Update(incident);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Customers = context.Customers.ToList();
                ViewBag.Technicians = context.Technicians.ToList();
                ViewBag.Products = context.Products.ToList();
                return View("AddEdit", incident);
            }
        }

        //here is the for Delete 
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Incident incident = context.Incidents.Find(id);


            if (incident == null)
            {
                return RedirectToAction("List", "Incidents");
            }
            else
            {

                return View(incident);
            }
        }
        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            if (incident== null)
            {
                return RedirectToAction("List");
            }
            else
            {
                context.Incidents.Remove(incident);
                context.SaveChanges();
                return RedirectToAction("List");
            }
        }
    }
}
