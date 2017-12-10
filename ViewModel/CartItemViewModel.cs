using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MK.BookStore.ViewModel
{
    public class CartItemViewModel
    {
        public decimal BookId { get; set; }
        public bool IsRemoved { get; set; }
        public int Quantity { get; set; }
    }
}