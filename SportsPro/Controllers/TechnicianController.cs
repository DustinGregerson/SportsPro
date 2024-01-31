
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
        [Route("technicians")]
        public IActionResult List()
        {
            List<Technician> technicians;
            technicians = context.Technicians.ToList();
            // the is the code that Pass TempData message to the view
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View(technicians);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            Technician technician = new Technician();
            return View("AddEdit",technician);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Technician technician = context.Technicians.Find(id); 
            ViewBag.Action = "Edit";
            return View("AddEdit",technician);
           
        }
        [HttpPost]
        public IActionResult AddEdit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                //if a tech was not found the id will be zero. The submited technician needs to be added
                if (technician.TechnicianID == 0)
                {

                    context.Technicians.Add(technician);

                    //Here the code for tempData
                    TempData["SuccessMessage"] = "Product added successfully!";

                }
                //else the technician is in the database and needs to be added
                else
                {
                    context.Technicians.Update(technician);

                    //Here is the message code
                    TempData["SuccessMessage"] = "Product updated successfully!";


                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit",technician);
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

                return View(technician);
            }
        }
        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            if(technician == null)
            {
                return RedirectToAction("List");
            }
            else
            {
                context.Technicians.Remove(technician);
                context.SaveChanges();
                //TempData Message
                TempData["SuccessMessage"] = "Product deleted successfully!";

                return RedirectToAction("List");
            }
        }
    }
}
