using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace MK.BookStore.Models
{
    public class AddressPartRecord : ContentPartRecord
    {
        public virtual int CustomerId { get; set; }
        public virtual string Type { get; set; }
    }
}