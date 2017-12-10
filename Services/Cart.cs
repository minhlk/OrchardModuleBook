using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MK.BookStore.Models;
using Orchard;
using Orchard.ContentManagement;

namespace MK.BookStore.Services
{
    public class Cart : ICart
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;

        public IEnumerable<CartItem> Items => ItemsInternal.AsReadOnly();

        private HttpContextBase HttpContext => _workContextAccessor.GetContext().HttpContext;

        private List<CartItem> ItemsInternal
        {
            get
            {
                var items = (List<CartItem>)HttpContext.Session["ShoppingCart"];

                if (items == null)
                {
                    items = new List<CartItem>();
                    HttpContext.Session["ShoppingCart"] = items;
                }

                return items;
            }
        }

        public Cart(IWorkContextAccessor workContextAccessor, IContentManager contentManager)
        {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
        }

        public void Add(int bookId, int quantity = 1)
        {
            var item = Items.SingleOrDefault(x => x.BookId == bookId);

            if (item == null)
            {
                item = new CartItem(bookId, quantity);
                ItemsInternal.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void Remove(int bookId)
        {
            var item = Items.SingleOrDefault(x => x.BookId == bookId);

            if (item == null)
                return;

            ItemsInternal.Remove(item);
        }

        public BookPart GetBook(int bookId)
        {
            return _contentManager.Get<BookPart>(bookId);
        }

        public void UpdateItems()
        {
            ItemsInternal.RemoveAll(x => x.Quantity == 0);
        }

        public IEnumerable<BookQuantity> GetBooks() {
            // Get a list of all product IDs from the shopping cart
            var ids = Items.Select(x => x.BookId).ToList();

            // Load all product parts by the list of IDs
            var bookParts = _contentManager.GetMany<BookPart>(ids, VersionOptions.Latest, QueryHints.Empty).ToArray();

            // Create a LINQ query that projects all items in the shoppingcart into shapes
            var query = from item in Items
                from bookPart in bookParts
                where bookPart.Id == item.BookId
                select new BookQuantity() { 
                    BookPart = bookPart,
                    Quantity = item.Quantity
                };

        return query;
        }

        public decimal Subtotal()
        {
            return Items.Select(x => GetBook(x.BookId).Price * x.Quantity).Sum();
        }

        public decimal Vat()
        {
            return Subtotal() * .19m;
        }

        public decimal Total()
        {
            return Subtotal() + Vat();
        }

        public int ItemCount()
        {
            return Items.Sum(x => x.Quantity);
        }

        private void Clear()
        {
            ItemsInternal.Clear();
            UpdateItems();
        }

    }
}
