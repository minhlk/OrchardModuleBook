using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Orchard.Environment;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace MK.BookStore
{
    public class AdminMenu : INavigationProvider
    {
        private readonly Work<RequestContext> _requestContextAccessor;

        public string MenuName => "admin";

        public AdminMenu(Work<RequestContext> requestContextAccessor)
        {
            _requestContextAccessor = requestContextAccessor;
            T = NullLocalizer.Instance;
        }

        private Localizer T { get; set; }
        public void GetNavigation(NavigationBuilder builder)
        {
            var requestContext = _requestContextAccessor.Value;
            var idValue = (string)requestContext.RouteData.Values["id"];
            var id = 0;

            if (!string.IsNullOrEmpty(idValue))
            {
                int.TryParse(idValue, out id);
            }
            builder

                // Image set
                .AddImageSet("webshop")

                // "Webshop"
                .Add(item => item

                    .Caption(T("Book Store"))
                    .Position("2")
                    .LinkToFirstChild(true)
                    .Add(subItem => subItem
                        .Caption(T("Customersz"))
                        .Action("Index", "CustomerAdmin", new { area = "MK.BookStore" })
                    )
                    // "Orders"
                    .Add(subItem => subItem
                        .Caption(T("Orders"))
                        .Position("2.1")
                        .Action("Index", "OrderAdmin", new { area = "MK.BookStore" })
                    )
                    //Customers
                    .Add(subItem => subItem
                        .Caption(T("Customers"))
                        .Position("2.2")
                        .Action("Index", "CustomerAdmin", new { area = "MK.BookStore" })
                        .Add(T("Details"), i => i.Action("Index", "CustomerAdmin", new { area = "MK.BookStore" }))
                        .Add(T("Details"), i => i.Action("Edit", "CustomerAdmin", new { id, area = "MK.BookStore" }).LocalNav())
                        .Add(T("Addresses"), i => i.Action("ListAddresses", "CustomerAdmin", new { id, area = "MK.BookStore" }).LocalNav())
                        .Add(T("Orders"), i => i.Action("ListOrders", "CustomerAdmin", new { id , area = "MK.BookStore" }).LocalNav())
                    )
                );
        }
    }
}