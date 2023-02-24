using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext db;
        private static Cart Cart;
        public CartController(ApplicationDbContext db)
        {
            this.db = db;
            

        }

        public IActionResult Index()
        {
            return View(db.CartRows.ToList());
        }


        public async void cart()
        {

            Cart = db.Carts.FirstOrDefault();
            if (Cart == null)
            {
                Cart = new Cart()
                {
                    Total = 0,
                    NumberOfArticles = 0
                };
                db.Carts.Add(Cart);
                db.SaveChanges();
            }

        }
        public async Task<IActionResult> AddtoCart(int id)
        {
            var product = db.Products.FindAsync(id);
            cart();
            var CartRow = db.CartRows.FirstOrDefault(n => n.Product.Id == product.Result.Id );
            if (CartRow == null)
            {
                CartRow Row = new CartRow()
                {

                    Id = product.Result.Id,
                    CartId = Cart.CartId,
                    Quantity = 1

                };
                if (ModelState.IsValid) db.CartRows.Add(Row);
            }
            else CartRow.Quantity++;
            await db.SaveChangesAsync();


            return View(await db.CartRows.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int Cartid)
        {
            return View(await db.CartRows.FindAsync(id, Cartid));
        }

        public IActionResult Delete_Confirmed(int id, int Cartid)
        {
            CartRow CartRow = db.CartRows.FindAsync(id, Cartid).Result;
            db.CartRows.Remove(CartRow);
            db.SaveChanges(true);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, int Cartid)
        {
            CartRow CartRow = db.CartRows.FindAsync(id, Cartid).Result;
            if (CartRow.CartId == null)
            {
                return NotFound();
            }
            return View(CartRow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int Cartid, CartRow cartRow)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.CartRows.Update(cartRow);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index)) ;
        }



    }
}
