using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace MK.BookStore.Models
{
    public class AddressPart : ContentPart<AddressPartRecord>
    {

        public int CustomerId
        {
            get => Record.CustomerId;
            set => Record.CustomerId = value;
        }

        public string Type
        {
            get => Record.Type;
            set => Record.Type = value;
        }
    }
}