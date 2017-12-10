using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace MK.BookStore.Models
{ 
    public class BookPartRecord : ContentPartRecord {
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }
        public virtual string Genre { get; set; }
        public virtual decimal Price { get; set; }
    }
}