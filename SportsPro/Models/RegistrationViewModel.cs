using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Models
{
    public class RegistrationViewModel
    {
        public RegistrationViewModel() { }
        public RegistrationViewModel(SportsProContext ctx, int id) {
            this.customer = ctx.Customers.Where(Q => Q.CustomerID == id).First();
        }

        public List<Product> UnregisteredProducts { get; set; }

        public Customer customer { get; set; }

        public List<Registration> registrations { get; set; }

        public int registerProductId { get; set; }

        public void getRegistrationsForCustomer(SportsProContext ctx)
        {
            registrations = ctx.Registrations
                .Include(Q => Q.Product)
                .Where(Q => Q.CustomerID == this.customer.CustomerID).ToList();
        }

        public void getUnregisteredProductsLists(SportsProContext ctx)
        {
            List<Product> allProducts = ctx.Products.ToList();
            foreach (Registration registration in this.registrations)
            {
                allProducts.Remove(registration.Product);
            }
            this.UnregisteredProducts = allProducts;

        }

    }
}
