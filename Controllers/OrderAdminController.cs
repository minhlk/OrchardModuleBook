using System.Linq;
using System.Web.Mvc;
using MK.BookStore.Models;
using MK.BookStore.Services;
using MK.BookStore.ViewModel;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Settings;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;

namespace MK.BookStore.Controllers
{
    [Admin]
    public class OrderAdminController : Controller
    {
        private dynamic Shape { get; set; }
        private Localizer T { get; set; }
        private readonly IOrderService _orderService;
        private readonly IRepository<BookPartRecord> _productRepository;
        private readonly INotifier _notifier;
        private readonly ISiteService _siteService;

        public OrderAdminController(INotifier notifier, IOrderService orderService, IShapeFactory shapeFactory, IRepository<BookPartRecord> productRepository, ISiteService siteService)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
            _notifier = notifier;
            _orderService = orderService;
            _productRepository = productRepository;
            _siteService = siteService;
        }

        public ActionResult Index(PagerParameters pagerParameters) {
            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters.Page, pagerParameters.PageSize);
            var ordersQuery = _orderService.GetOrders();
            var orders = ordersQuery.Skip(pager.GetStartIndex()).Take(pager.PageSize);
            var pagerShape = Shape.Pager(pager).TotalItemCount(ordersQuery.Count());
            var model = Shape.Orders(Orders: orders.ToArray(), Pager: pagerShape);
            return View((object)model);
//            var orders = _orderService.GetOrders().ToArray();
//            return View(orders);
        }

        public ActionResult Edit(int id) {
            var order = _orderService.GetOrder(id);
            var model = BuildModel(order, new EditOrderVM(order));
            return View((object)model);
            
        }

        [HttpPost]
        public ActionResult Edit(EditOrderVM model)
        {
            var order = _orderService.GetOrder(model.Id);

            if (!ModelState.IsValid)
                return BuildModel(order, model);

            order.Status = model.Status;

            _notifier.Add(NotifyType.Information, T("The order has been saved"));
            return RedirectToAction("ListOrders", "CustomerAdmin", new { id = order.CustomerId });
        }

        private dynamic BuildModel(OrderRecord order, EditOrderVM editModel) {
            return Shape.Order(
                Order: order,
                Details: order.Details.Join(_productRepository.Table, x => x.ProductId, x => x.Id, (record, productRecord) => 
                    Shape.Detail
                    (             
                        Price: productRecord.Price,
                        Name: productRecord.Name,
                        Author: productRecord.Author,
                        Genre: productRecord.Genre
                    )).ToArray(),
                EditModel: editModel
            );
        }
    }
}