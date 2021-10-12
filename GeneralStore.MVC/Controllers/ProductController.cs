﻿using GeneralStore.MVC.Models;
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
    public class ProductController : Controller
    {
        // Add the application DB Context (link to the database)
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Product
        public async Task<ActionResult> Index()
        {
            Product[] productArray = await _db.Products.ToArrayAsync();
            Product[] orderedArray = productArray.OrderBy(prod => prod.Name).ToArray();
            return View(orderedArray);
        }

        // GET: Create Product 
        // Product/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Create Product
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Details
        //Product/Details/{id}
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = await _db.Products.FindAsync(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // GET: Edit
        // Product/Edit/{id}
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = await _db.Products.FindAsync(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }
        // POST: Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(product).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Delete
        // Product/Delete/{id}
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = await _db.Products.FindAsync(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }
        // Post: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Product product = await _db.Products.FindAsync(id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}