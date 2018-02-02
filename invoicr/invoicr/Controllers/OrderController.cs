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
    public class OrderController : Controller
    {
        private InvoicrContext ic = new InvoicrContext();

        // GET: /<controller>/
        public IActionResult Add(int id)
        {
            ViewData["InvoiceID"] = id;
            IEnumerable<Product> _viewModel = ic.Products.AsEnumerable(); 
            return View(_viewModel);
        }

        [HttpPost]
        public IActionResult Add(int invoice, int product, int quantity)
        {
            Order newOrder = new Order
            {
                Invoice = ic.Invoices.SingleOrDefault(i => i.ID == invoice),
                Product = ic.Products.SingleOrDefault(p => p.ID == product),
                Quantity = quantity,
            };

            ic.Orders.Add(newOrder);
            ic.SaveChanges();

            return RedirectToAction("View", "Invoice", new { id = invoice });
        }
    }
}
