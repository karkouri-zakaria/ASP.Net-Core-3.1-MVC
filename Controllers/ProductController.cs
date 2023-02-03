using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Details(int id)
        {
            return View(db.Products.Find(id));
        }

        [HttpGet]
        public IActionResult Create() {
            
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
        public IActionResult Delete(int id)
        {
            return View(db.Products.Find(id));
        }

        [HttpPost]
        public IActionResult Delete(Product prod)
        {
            db.Products.Remove(prod);
            db.SaveChanges(true);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(db.Products.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Product prod)
        {
            if (ModelState.IsValid)
            {
                db.Products.Update(prod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
