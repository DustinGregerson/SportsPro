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
        //Adding new Product to table
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            Product product = new Product();
            return View("AddEdit", product);
        }
        //Edit the product
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product = context.Products.Find(id);
            ViewBag.Action = "Edit";
            return View("AddEdit", product);
        }

        [HttpPost]
        public IActionResult AddEdit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    context.Products.Add(product);
                }
                // the product from database have to Added
                else
                {
                    context.Products.Update(product);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return View("AddEdit", product);
            }  
        }

        //here is the for Delete 
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product = context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("List", "Products");
            }
            else
            {

                return View(product);
            }
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (product == null)
            {
                return RedirectToAction("List");
            }
            else
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return RedirectToAction("List");
            }
        }
    }
}
