using MusicStore.Data;
using MusicStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace MusicStore.Controllers
{
    public class ShoppingCart : Controller
    {
        private readonly MusicStoreContext _context;

        public ShoppingCart(MusicStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(CartManager.cart);
        }

        public IActionResult AddToCart(int id)
        {
            var music = _context.Music.FirstOrDefault(m => m.id == id);
            if (music == null)
            {
                return NotFound();
            }

            var existingItem = CartManager.cart.Items.FirstOrDefault(i => i.MusicId == id);

            if(existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                //use the constructor directly
                var carItem = new CartItem(music.id, music.title, music.price, 1);
                CartManager.cart.Items.Add(carItem);
            }

            return RedirectToAction("Index", CartManager.cart);
        }

        public IActionResult RemoveFromCart(int id)
        {
            var itemToRemove = CartManager.cart.Items.FirstOrDefault(i => i.MusicId == id);

            if(itemToRemove != null)
            {
                if(itemToRemove.Quantity > 1) 
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    CartManager.cart.Items.Remove(itemToRemove);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Purchased()
        {
            return View(CartManager.cart);
        }

       
       
    }
}
