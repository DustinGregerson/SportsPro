
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SportsPro.Models
{
    public class IncidentViewModelAddEdit
    {
        public List<Technician> technicians { get; set; }
        public List<Product> products { get; set; }
        public List<Customer> customers { get; set; }

        public Incident incident { get; set; }
        
        public Technician technician { get; set; }
        public Product product { get; set; }
        public Customer customer { get; set; }

        public string addOrEdit { get; set; }

        //This method only sets the lists
        public void setLists(SportsProContext context)
        {
            this.customers = context.Customers.ToList();
            this.products = context.Products.ToList();
            this.technicians = context.Technicians.ToList();
        }
        //This method get the incident from the id, sets the lists and get the product,technician and customer
        public void setListsAndFindPTC(int id, SportsProContext context)
        {
            this.incident = context.Incidents.Find(id);
            this.customers = context.Customers.ToList();
            this.products = context.Products.ToList();
            this.technicians = context.Technicians.ToList();
            this.customer = context.Customers.Find(this.incident.CustomerID);
            this.product = context.Products.Find(this.incident.ProductID);
            this.technician = context.Technicians.Find(this.incident.TechnicianID);
        }
        //This method sets the lists and finds the product,technician,and customer.
        //however , it expects that the incident has been created.
        public void setListsAndFindPTC(SportsProContext context)
        {
            this.customers = context.Customers.ToList();
            this.products = context.Products.ToList();
            this.technicians = context.Technicians.ToList();
            this.customer = context.Customers.Find(this.incident.CustomerID);
            this.product = context.Products.Find(this.incident.ProductID);
            this.technician = context.Technicians.Find(this.incident.TechnicianID);
        }
    }
}
