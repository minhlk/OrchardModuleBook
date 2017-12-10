using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MK.BookStore.Models
{
    public sealed class BookQuantity
    {
        public BookPart BookPart { get; set; }
        public int Quantity { get; set; }
    }
}