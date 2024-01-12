
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller

    {
        private SportsProContext context;
        public TechnicianController(SportsProContext ctx)
        {
            context = ctx;
        }

        public IActionResult List()
        {
            List<Technician> technicians;
            technicians = context.Technicians.ToList();
            return View(technicians);
        }
        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            Technician technician = context.Technicians.Find(id);
            if (technician == null)
            {
                ViewBag.action = "add";
                return View(technician);
            }
            else
            {
                ViewBag.action = "edit";
                return View(technician);
            }
           
        }
        [HttpPost]
        public IActionResult AddEdit(Technician technician)
        {
            if (ViewBag.action.equals("add"))
            {
                context.Technicians.Add(technician);
                return RedirectToAction("List", "Technician");
            }
            else if(ViewBag.action.equals("edit"))
            {
                context.Technicians.Update(technician);
                return RedirectToAction("List", "Technician");
            }
            else
            {
                return RedirectToAction("List", "Technician");
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Technician technician=context.Technicians.Find(id);
            
            if (technician == null)
            {
                return RedirectToAction("List", "Technician");
            }
            else
            {
                context.Technicians.Remove(technician);
                context.SaveChanges();

                return View(technician);
            }
        }
        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            if(technician == null)
            {
                return RedirectToAction("List", "Technician");
            }
            else
            {
                context.Technicians.Remove(technician);
                context.SaveChanges();
                return RedirectToAction("List", "Technician");
            }
        }
    }
}
