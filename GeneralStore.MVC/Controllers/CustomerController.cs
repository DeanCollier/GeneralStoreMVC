using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        // Add the application DB Context (link to the database) 
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            Customer[] customerArray = await _db.Customers.ToArrayAsync();
            Customer[] orderedArray = customerArray.OrderBy(cust => cust.LastName).ToArray();
            return View(orderedArray);
        }

        // GET: Customer
        // Customer/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Create Customer
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Details
        // Customer/Details/{id}
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Customer customer = await _db.Customers.FindAsync(id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // GET: Edit
        // Customer/Edit/{id}
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Customer customer = await _db.Customers.FindAsync(id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        // POST: Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(customer).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Delete
        // Customer/Delete/{id}
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Customer customer = await _db.Customers.FindAsync(id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Customer customer = await _db.Customers.FindAsync(id);
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}