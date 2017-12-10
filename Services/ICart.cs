using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BookStore.Models;
using Orchard;

namespace MK.BookStore.Services
{
    public interface ICart : IDependency
    {
        IEnumerable<CartItem> Items { get; }
        void Add(int productId, int quantity = 1);
        void Remove(int bookId);
        BookPart GetBook(int bookId);
        decimal Subtotal();
        decimal Vat();
        decimal Total();
        int ItemCount();
        void UpdateItems();
        IEnumerable<BookQuantity> GetBooks();

    }
}
