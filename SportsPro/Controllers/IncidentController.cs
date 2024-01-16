using Microsoft.AspNetCore.Mvc;
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
            incidents = context.Incidents.ToList();

            return View(incidents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            Incident incident = new Incident();
            return View("AddEdit", incident);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Incident incident = context.Incidents.Find(id);
            ViewBag.Action = "Edit";
            return View("AddEdit", incident);
        }

        [HttpPost]
        public IActionResult AddEdit(Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incident.ProductID == 0)
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
