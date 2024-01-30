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
        [Route("products")]
        public IActionResult List()
        {
            List<Product> products;
            products = context.Products.ToList();

            // the is the code that Pass TempData message to the view
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

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
                    //Here the code for tempData
                    TempData["SuccessMessage"] = "Product added successfully!";
                }
                // the product from database have to Added
                else
                {
                    context.Products.Update(product);
                    //Here is the message code
                    TempData["SuccessMessage"] = "Product updated successfully!";
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
                // return RedirectToAction("List", "Products");
                return NotFound();
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
                //TempData Message
                TempData["SuccessMessage"] = "Product deleted successfully!";
                return RedirectToAction("List");
            }
        }
    }
}
