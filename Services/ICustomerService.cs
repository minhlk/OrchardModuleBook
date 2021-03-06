﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.BookStore.Models;
using Orchard;
using Orchard.ContentManagement;

namespace MK.BookStore.Services
{
    public interface ICustomerService : IDependency
    {
        CustomerPart CreateCustomer(string email, string password);
        AddressPart GetAddress(int customerId, string addressType);
        AddressPart CreateAddress(int customerId, string addressType);
        IContentQuery<CustomerPart> GetCustomers();
        CustomerPart GetCustomer(int id);
        IEnumerable<AddressPart> GetAddresses(int customerId);

        AddressPart GetAddress(int id);
    }
}
