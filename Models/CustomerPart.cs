using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Security;
using Orchard.Users.Models;

namespace MK.BookStore.Models
{
    public class CustomerPart : ContentPart<CustomerPartRecord>
    {

        public string FirstName
        {
            get => Record.FirstName;
            set => Record.FirstName = value;
        }

        public string LastName
        {
            get => Record.LastName;
            set => Record.LastName = value;
        }

        public string Title
        {
            get => Record.Title;
            set => Record.Title = value;
        }

        public DateTime CreatedUtc
        {
            get => Record.CreatedUtc;
            set => Record.CreatedUtc = value;
        }
        public IUser User => this.As<UserPart>();
    }
}