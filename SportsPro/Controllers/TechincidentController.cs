using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SportsPro.Controllers
{
    public class TechincidentController : Controller
    {
        private SportsProContext context;

        public TechincidentController(SportsProContext ctx)
        {
            context = ctx;
        }
        public IActionResult get(string error)
            {
            
                List<Technician> technicians = context.Technicians.ToList();
                if (error != null)
                {
                    ViewBag.Error = error;
                }

                return View("get", technicians);
            }

        public IActionResult List(string id)
        {
            int? techId = null;

            string error = "";
            if (id != null)
            {
                try
                {
                    techId = int.Parse(id);
                }
                catch
                {
                    error = "DO NOT MANIPULATE THE HTML";

                    return RedirectToAction("get", new { error = error });
                }

            }
            else
            {
                error = "You must select a technician.";
                return RedirectToAction("get", new { error = error });
            }

            HttpContext.Session.SetString("TechId", id);

            List<Incident> incidents = context.Incidents
            .Include(Q => Q.Product)
            .Include(Q => Q.Technician)
            .Include(Q => Q.Customer).Where(q => q.TechnicianID == techId).ToList();

            Technician tech = context.Technicians.Find(techId);
            ViewBag.TechName = tech.Name;
            return View(incidents);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            IncidentViewModelAddEdit incidentViewModelAddEdits = new IncidentViewModelAddEdit();
            incidentViewModelAddEdits.setListsAndFindPTC(id, context);
            
           
            return View("Edit", incidentViewModelAddEdits);
        }

    }
}
