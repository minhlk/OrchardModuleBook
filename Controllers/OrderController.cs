using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MK.BookStore.Models;
using MK.BookStore.Services;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Security;
using Orchard.Themes;

namespace MK.BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly dynamic _shapeFactory;
        private readonly IOrderService _orderService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICart _shoppingCart;
        private readonly ICustomerService _customerService;
        private readonly Localizer _t;

        public OrderController(IShapeFactory shapeFactory
            , IOrderService orderService
            , IAuthenticationService authenticationService
            , ICart shoppingCart
            , ICustomerService customerService)
        {
            _shapeFactory = shapeFactory;
            _orderService = orderService;
            _authenticationService = authenticationService;
            _shoppingCart = shoppingCart;
            _customerService = customerService;
            _t = NullLocalizer.Instance;
        }

        [Themed, HttpPost]
        public ActionResult Create()
        {

            var user = _authenticationService.GetAuthenticatedUser();

            if (user == null)
                throw new OrchardSecurityException(_t("Login required"));

            var customer = user.ContentItem.As<CustomerPart>();

            if (customer == null)
                throw new InvalidOperationException("The current user is not a customer");

            var order = _orderService.CreateOrder(customer.Id, _shoppingCart.Items);

            // Todo: Give payment service providers a chance to process payment by sending a event. If no PSP handled the event, we'll just continue by displaying the created order.
            // Raise an OrderCreated event

            // If we got here, no PSP handled the OrderCreated event, so we'll just display the order.
            var shape = _shapeFactory.Create(
                Order: order,
                Products: _orderService.GetProducts(order.Details).ToArray(),
                Customer: customer,
                InvoiceAddress: (dynamic)_customerService.GetAddress(user.Id, "InvoiceAddress"),
                ShippingAddress: (dynamic)_customerService.GetAddress(user.Id, "ShippingAddress")
            );
            return new ShapeResult(this, shape);
        }
    }
}