using MK.BookStore.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Users.Models;

namespace MK.BookStore.Handlers
{
    public class CustomerPartHandler : ContentHandler
    {
        public CustomerPartHandler(IRepository<CustomerPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<UserPart>("Customer"));
        }
    }
}