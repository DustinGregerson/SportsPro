using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class IncidentViewModelList
    {
        public IncidentViewModelList(SportsProContext context,string filter) {
            incidents = context.Incidents.ToList();
            incidents = context.Incidents
                .Include(Q => Q.Product)
                .Include(Q => Q.Technician)
                .Include(Q => Q.Customer).ToList();
            this.filter= filter;
        }

        public List<Incident> incidents { get; set; }
        public string filter { get; set; }
        public string FilterType { get; set; }

    }
}
