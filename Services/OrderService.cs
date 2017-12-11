using MK.BookStore.Models;
using Orchard.ContentManagement;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MK.BookStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<BookPartRecord> _productRepository;
        private readonly IContentManager _contentManager;
        private readonly IRepository<OrderRecord> _orderRepository;
        private readonly IRepository<OrderDetailRecord> _orderDetailRepository;

        public OrderService(IDateTimeService dateTimeService, IRepository<BookPartRecord> productRepository, IContentManager contentManager, IRepository<OrderRecord> orderRepository, IRepository<OrderDetailRecord> orderDetailRepository)
        {
            _dateTimeService = dateTimeService;
            _productRepository = productRepository;
            _contentManager = contentManager;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public OrderRecord CreateOrder(int customerId, IEnumerable<CartItem> items)
        {

            if (items == null)
                throw new ArgumentNullException("items");

            // Convert to an array to avoid re-running the enumerable
            var itemsArray = items.ToArray();

            if (!itemsArray.Any())
                throw new ArgumentException("Creating an order with 0 items is not supported", "items");

            var order = new OrderRecord
            {
                CreatedAt = _dateTimeService.Now,
                CustomerId = customerId,
                Status = OrderStatus.New
            };

            _orderRepository.Create(order);

            // Get all products in one shot, so we can add the product reference to each order detail
            var productIds = itemsArray.Select(x => x.BookId).ToArray();
            var products = _productRepository.Fetch(x => productIds.Contains(x.Id)).ToArray();

            // Create an order detail for each item
            foreach (var item in itemsArray)
            {
                var product = products.Single(x => x.Id == item.BookId);

                var detail = new OrderDetailRecord
                {
                    OrderRecord_Id = order.Id,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    VatRate = .19m
                };

                _orderDetailRepository.Create(detail);
                order.Details.Add(detail);
            }

//            order.UpdateTotals();

            return order;
        }

        /// <summary>
        /// Gets a list of ProductParts from the specified list of OrderDetails. Useful if you need to use the product as a ProductPart (instead of just having access to the ProductRecord instance).
        /// </summary>
        public IEnumerable<BookPart> GetProducts(IEnumerable<OrderDetailRecord> orderDetails)
        {
            var productIds = orderDetails.Select(x => x.ProductId).ToArray();
            return _contentManager.GetMany<BookPart>(productIds, VersionOptions.Latest, QueryHints.Empty);
        }
    }
}