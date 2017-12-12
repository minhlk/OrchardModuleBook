using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MK.BookStore.Models;

namespace MK.BookStore.Extensibility
{
    public class PaymentRequest
    {
        public OrderRecord Order { get; private set; }
        public bool WillHandlePayment { get; set; }
        public ActionResult ActionResult { get; set; }

        public PaymentRequest(OrderRecord order)
        {
            Order = order;
        }
    }
}