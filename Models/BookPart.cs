using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace MK.BookStore.Models
{
    public class BookPart : ContentPart<BookPartRecord>
    {
        public string Name
        {
            get => Record.Name;
            set => Record.Name = value;
        }
        public string Author
        {
            get => Record.Author;
            set => Record.Author = value;
        }
        public string Genre
        {
            get => Record.Genre;
            set => Record.Genre = value;
        }
        public decimal Price
        {
            get => Record.Price;
            set => Record.Price = value;
        }

    }
}