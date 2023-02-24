using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProductsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await db.Products.FindAsync(id));
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product prod)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(prod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await db.Products.FindAsync(id));
        }

        [HttpPost]
        public IActionResult Delete(Product prod)
        {
            db.Products.Remove(prod);
            db.SaveChanges(true);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(product);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            if (db.Products == null)
            {
                return Problem("Entity set 'db.Products'  is null.");
            }

            var product = from m in db.Products
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.ProductName!.Contains(searchString));
            }

            return View(await product.ToListAsync());
        }

    }
}
    