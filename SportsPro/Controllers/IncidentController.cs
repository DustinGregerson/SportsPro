﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public IActionResult Add()
        {
            IncidentViewModelAddEdit incidentViewModelAddEdit = new IncidentViewModelAddEdit();
            incidentViewModelAddEdit.addOrEdit = "Add";
            incidentViewModelAddEdit.incident = new Incident();
            incidentViewModelAddEdit.setLists(context);

            // Set default filter type, e.g., "All"
            incidentViewModelAddEdit.FilterType = "";


            return View("AddEdit", incidentViewModelAddEdit);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            IncidentViewModelAddEdit incidentViewModelAddEdit = new IncidentViewModelAddEdit();
            incidentViewModelAddEdit.setListsAndFindPTC(id, context);
            incidentViewModelAddEdit.addOrEdit = "Edit";
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
                    // here the New incident
                    if (incidentViewModelAddEdit.FilterType == "Unassigned")
                    {
                        incident.TechnicianID = null; // Unassigned incident
                    }
                    else if (incidentViewModelAddEdit.FilterType == "Open")
                    {
                        incident.DateClosed = null; // Open incident
                    }

                    context.Incidents.Add(incident);
                    TempData["SuccessMessage"] = "Incident added successfully!";
                }
                else
                {
                    context.Incidents.Update(incident);
                    TempData["SuccessMessage"] = "Incident updated successfully!";
                }

                context.SaveChanges();
                return RedirectToAction("List", new { filterType = incidentViewModelAddEdit.FilterType });

            }
            else
            {
                incidentViewModelAddEdit.setListsAndFindPTC(context);
                return View("AddEdit", incidentViewModelAddEdit);
            }
        }


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
            if (incident == null)
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
        [Route("incidents")]
        public IActionResult List(string filter)
        {
            IncidentViewModelList incidentViewModelList=new IncidentViewModelList(context,filter);
            if (filter == "")
            {
                filter = "All";
            }
            ViewBag.FilterType = filter;
            // here is the code that sets the ViewBag.FilterType to be used in the view

            

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(incidentViewModelList);
        }




    }
}
