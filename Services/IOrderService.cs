using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MK.BookStore.Models;
using Orchard;

namespace MK.BookStore.Services
{
    public interface IOrderService : IDependency
    {
        /// <summary>
        /// Creates a new order based on the specified ShoppingCartItems
        /// </summary>
        OrderRecord CreateOrder(int customerId, IEnumerable<CartItem> items);

        /// <summary>
        /// Gets a list of ProductParts from the specified list of OrderDetails. Useful if you need to use the product as a ProductPart (instead of just having access to the ProductRecord instance).
        /// </summary>
        IEnumerable<BookPart> GetProducts(IEnumerable<OrderDetailRecord> orderDetails);
    }
}