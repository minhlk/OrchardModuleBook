using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MK.BookStore.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace MK.BookStore.Handlers
{
    public class AddressPartHandler : ContentHandler
    {
        public AddressPartHandler(IRepository<AddressPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}