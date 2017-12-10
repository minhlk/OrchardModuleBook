using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MK.BookStore.Models;
using MK.BookStore.Services;
using MK.BookStore.ViewModel;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Mvc;
using Orchard.Themes;

namespace MK.BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrchardServices _services;
        private readonly ICart _cart;

        public CartController(ICart cart, IOrchardServices services)
        {
            _cart = cart;
            _services = services;
        }
        [Themed]
        public ActionResult Index() {
            // Create a new shape using the "New" property of IOrchardServices.
            var shape = _services.New.Cart(
                Books: _cart.GetBooks().Select(p => _services.New.CartItem(
                    BookPart: p.BookPart,
                    Quantity: p.Quantity,
                    Title: _services.ContentManager.GetItemMetadata(p.BookPart).DisplayText)
                ).ToList(),
                Total: _cart.Total(),
                Subtotal: _cart.Subtotal(),
                Vat: _cart.Vat()
            );

            // Return a ShapeResult
            return new ShapeResult(this, shape);
        }

        [HttpPost]
        public ActionResult Add(int id)
        {
            _cart.Add(id);
            return RedirectToAction("Index");
        }
        public ActionResult Update(string command, CartItemViewModel[] items)
        {

            // Loop through each posted item
            foreach (var item in items)
            {

                // Select the shopping cart item by posted product ID
                var shoppingCartItem = _cart.Items.SingleOrDefault(x => x.BookId == item.BookId);

                if (shoppingCartItem != null)
                {
                    // Update the quantity of the shoppingcart item. If IsRemoved == true, set the quantity to 0
                    shoppingCartItem.Quantity = item.IsRemoved ? 0 : item.Quantity < 0 ? 0 : item.Quantity;
                }
            }

            // Update the shopping cart so that items with 0 quantity will be removed
            _cart.UpdateItems();

            // Return an action result based on the specified command
            switch (command)
            {
                case "Checkout":
                    break;
                case "ContinueShopping":
                    break;
                case "Update":
                    break;
            }

            // Return to Index if no command was specified
            return RedirectToAction("Index");
        }



        public ActionResult GetItems()
        {
            var products = _cart.GetBooks();

            var json = new
            {
                items = (from item in products
                    select new
                    {
                        id = item.BookPart.Id,
                        title = _services.ContentManager.GetItemMetadata(item.BookPart).DisplayText ?? "(No TitlePart attached)",
                        unitPrice = item.BookPart.Price,
                        quantity = item.Quantity
                    }).ToArray()
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        private void UpdateShoppingCart(IEnumerable<CartItemViewModel> items)
        {

            _cart.Clear();

            if (items == null)
                return;

            _cart.AddRange(items
                .Where(item => !item.IsRemoved)
                .Select(item => new CartItem(item.BookId, item.Quantity < 0 ? 0 : item.Quantity))
            );

            _cart.UpdateItems();
        }
    }
}