using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private SportsProContext context;
        public ProductController(SportsProContext ctx)
        {
           context=ctx;
        }

        public IActionResult List()
        {
            List<Product> products;
            products = context.Products.ToList();

            return View(products);
        }
    }
}
