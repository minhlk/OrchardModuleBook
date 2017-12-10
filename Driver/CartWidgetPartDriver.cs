using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MK.BookStore.Models;
using MK.BookStore.Services;
using Orchard.ContentManagement.Drivers;

namespace MK.BookStore.Driver
{
    public class CartWidgetPartDriver : ContentPartDriver<CartWidgetPart>
    {
        private readonly ICart _cart;

        public CartWidgetPartDriver(ICart cart)
        {
            _cart = cart;
        }

        protected override DriverResult Display(CartWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_CartWidget", () => shapeHelper.Parts_CartWidget(
                ItemCount: _cart.ItemCount(),
                TotalAmount: _cart.Total()
            ));
        }
    }
}