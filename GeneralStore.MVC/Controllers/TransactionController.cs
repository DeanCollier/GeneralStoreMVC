using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction { CustomerId = viewModel.CustomerId, ProductId = viewModel.ProductId };
                _db.Transactions.Add(transaction);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Details
        // Transaction/Details/{id}
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Transaction transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null)
                return HttpNotFound();

            return View(transaction);
        }

        // GET: Edit
        // Transaction/Edit/{id}
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Transaction transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null)
                return HttpNotFound();

            ViewData["Customers"] = _db.Customers.Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });

            ViewData["Products"] = _db.Products.Select(prod => new SelectListItem
            {
                Text = prod.Name,
                Value = prod.ProductId.ToString()
            });

            return View(transaction);
        }
        // POST: Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(transaction).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Delete
        // Transaction/Delete/{id}
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Transaction transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null)
                return HttpNotFound();

            return View(transaction);
        }
        // Post: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Transaction transaction = await _db.Transactions.FindAsync(id);
            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}
