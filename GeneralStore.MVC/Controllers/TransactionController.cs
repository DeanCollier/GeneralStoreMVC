using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        // Add the application DB Context (link to the database) 
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Transaction
        public async Task<ActionResult> Index()
        {
            Transaction[] transactionArray = await _db.Transactions.ToArrayAsync();
            Transaction[] orderedArray = transactionArray.OrderBy(trans => trans.Customer.LastName).ToArray();
            return View(orderedArray);
        }

        // GET: Create Transaction
        // Transaction/Create
        public ActionResult Create()
        {
            var viewModel = new CreateTransactionViewModel();
            viewModel.Customers = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });

            viewModel.Products = _db.Products.Select(prod => new SelectListItem
            {
                Text = prod.Name,
                Value = prod.ProductId.ToString()
            });

            return View(viewModel);
        }
        // POST: Create Transaction
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTransactionViewModel viewModel)
        {
            /*if (ModelState.IsValid)
            {
                _db.Transactions.Add(transaction);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }*/
            return View(viewModel);
        }

    }
}