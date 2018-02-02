using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using invoicr.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static invoicr.Models.InvoicrModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invoicr.Controllers
{
    public class InvoiceController : Controller
    {
        private InvoicrContext ic = new InvoicrContext();

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Invoice> _viewModel = ic.Invoices.Include(i => i.Orders).ThenInclude(o => o.Product).AsEnumerable();
            foreach(Invoice i in _viewModel)
            {
                if (i.Orders == null)
                {
                    i.Orders = new List<Order>();
                }
            }
            return View(_viewModel);
        }

        public IActionResult Add()
        {
            Invoice newInvoice = new Invoice
            {
                GUID = Guid.NewGuid().ToString(),
            };

            ic.Invoices.Add(newInvoice);
            ic.SaveChanges();
            return RedirectToAction("View", "Invoice", new { id = newInvoice.ID });
        }

        public IActionResult View(int id)
        {
            Invoice _viewModel = ic.Invoices.Include(i => i.Orders).ThenInclude(o => o.Product).SingleOrDefault(i => i.ID == id);
            if(_viewModel.Orders == null)
            {
                _viewModel.Orders = new List<Order>();
            }
            return View(_viewModel);
        }
    }
}
