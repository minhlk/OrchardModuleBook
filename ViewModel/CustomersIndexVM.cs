using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MK.BookStore.ViewModel
{
    public class CustomersIndexVM
    {
        public IList<dynamic> Customers { get; set; }
        public dynamic Pager { get; set; }
        public CustomersSearchVM Search { get; set; }

        public CustomersIndexVM()
        {
            Search = new CustomersSearchVM();
        }

        public CustomersIndexVM(IEnumerable<dynamic> customers, CustomersSearchVM search, dynamic pager)
        {
            Customers = customers.ToArray();
            Search = search;
            Pager = pager;
        }
    }
}