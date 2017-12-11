using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BookStore.Models;
using Orchard;

namespace MK.BookStore.Services
{
    public interface ICustomerService : IDependency
    {
        CustomerPart CreateCustomer(string email, string password);
        AddressPart GetAddress(int customerId, string addressType);
        AddressPart CreateAddress(int customerId, string addressType);
    }
}
