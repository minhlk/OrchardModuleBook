using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MK.BookStore.Models
{
    [Serializable]
    public sealed class CartItem
    {
        public int BookId { get; private set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException();

                _quantity = value;
            }
        }

        public CartItem()
        {
        }

        public CartItem(int bookId, int quantity = 1)
        {
            BookId = bookId;
            Quantity = quantity;
        }
    }
}