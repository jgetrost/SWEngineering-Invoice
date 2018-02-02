using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using invoicr.Data;
using Microsoft.AspNetCore.Mvc;
using static invoicr.Models.InvoicrModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invoicr.Controllers
{
    public class ProductController : Controller
    {
        private InvoicrContext ic = new InvoicrContext();

        /// <summary>
        /// List of all products.
        /// </summary>
        /// <returns>The /Product/Index page.</returns>
        public IActionResult Index()
        {
            IEnumerable<Product> _viewModel = ic.Products.AsEnumerable();
            return View(_viewModel);
        }

        /// <summary>
        /// Add product page.
        /// </summary>
        /// <returns>The /Product/Add view.</returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Adds the new product to the database that was submitted by the form on /Product/Add
        /// </summary>
        /// <param name="name">The name of the new product.</param>
        /// <param name="description">A description of the new product.</param>
        /// <param name="price">The price of the new product.</param>
        /// <returns>A redirection to the /Product/Index</returns>
        [HttpPost]
        public IActionResult Add(string name, string description, string price)
        {
            Product newProduct = new Product
            {
                Name = name,
                Descrption = description,
                Price = System.Convert.ToDecimal(price)
            };

            ic.Products.Add(newProduct);
            ic.SaveChanges();

            return RedirectToAction("Index", "Product");
        }
    }
}
