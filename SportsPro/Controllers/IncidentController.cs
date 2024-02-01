using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        [Route("incidents")]
        public IActionResult List()
        {
            IncidentViewModelList incidentViewModelList = new IncidentViewModelList(context,"");


            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(incidentViewModelList);
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            IncidentViewModelAddEdit incidentViewModelAddEdit = new IncidentViewModelAddEdit();
            incidentViewModelAddEdit.addOrEdit = "Add";
            incidentViewModelAddEdit.incident = new Incident();
            incidentViewModelAddEdit.setLists(context);
            return View("AddEdit", incidentViewModelAddEdit);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IncidentViewModelAddEdit incidentViewModelAddEdit = new IncidentViewModelAddEdit();
            incidentViewModelAddEdit.setListsAndFindPTC(id, context);
            return View("AddEdit", incidentViewModelAddEdit);
        }

        [HttpPost]
        public IActionResult AddEdit(IncidentViewModelAddEdit incidentViewModelAddEdit)
        {
            Incident incident = incidentViewModelAddEdit.incident;



            if (ModelState.IsValid)
            {
                if (incident.IncidentID == 0)
                {
                    context.Incidents.Add(incident);

                    //Here the code for tempData
                    TempData["SuccessMessage"] = "Incident added successfully!";
                }
               
                else
                {
                    context.Incidents.Update(incident);

                    //Here is the message code
                    TempData["SuccessMessage"] = "Incident updated successfully!";
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {

                incidentViewModelAddEdit.setListsAndFindPTC(context);

                return View("AddEdit", incidentViewModelAddEdit);
                
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
                //TempData Message
                TempData["SuccessMessage"] = "Incident deleted successfully!";
                return RedirectToAction("List");
            }
        }
    }
}
