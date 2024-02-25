using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class IncidentViewModelList
    {
        public IncidentViewModelList(SportsProContext context,string filter) {
            this.filter= filter;
            if(filter == null||filter=="All")
            {
                incidents = context.Incidents
                .Include(Q => Q.Product)
                .Include(Q => Q.Technician)
                .Include(Q => Q.Customer)
                .ToList();
            }
            else if(filter == "Unassigned")
            {
                incidents = context.Incidents
                .Include(Q => Q.Product)
                .Include(Q => Q.Customer)
                .Where(Q=>Q.TechnicianID==null)
                .ToList();
            }
            else if(filter == "Open")
            {
                incidents = context.Incidents
                .Include(Q => Q.Product)
                .Include(Q => Q.Technician)
                .Include(Q => Q.Customer)
                .Where(Q=>Q.DateClosed==null)
                .ToList();
            }
        }

        public List<Incident> incidents { get; set; }
        public string filter { get; set; }
        public string FilterType { get; set; }

    }
}
