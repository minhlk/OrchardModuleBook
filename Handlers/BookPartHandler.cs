using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MK.BookStore.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace MK.BookStore.Handlers
{
    public class BookPartHandler : ContentHandler
    {
        public BookPartHandler(IRepository<BookPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}