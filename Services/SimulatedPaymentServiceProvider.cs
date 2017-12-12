using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MK.BookStore.Extensibility;
using Orchard.Environment.Extensions;

namespace MK.BookStore.Services
{
    [OrchardFeature("MK.BookStore.SimulatedPSP")]
    public class SimulatedPaymentServiceProvider : IPaymentServiceProvider
    {
        public void RequestPayment(PaymentRequest e)
        {

            e.ActionResult = new RedirectToRouteResult(new RouteValueDictionary {
                {"action", "Index"},
                {"controller", "SimulatedPaymentServiceProvider"},
                {"area", "MK.BookStore"},
                {"orderReference", (e.Order.Id + 1000).ToString(CultureInfo.InvariantCulture)},
                {"amount", (int)((e.Order.SubTotal + e.Order.Vat) * 100)}
            });

            e.WillHandlePayment = true;
        }

        public void ProcessResponse(PaymentResponse e)
        {
            var result = e.HttpContext.Request.QueryString["result"];

            e.OrderReference = e.HttpContext.Request.QueryString["orderReference"];
            e.PaymentReference = e.HttpContext.Request.QueryString["paymentId"];
            e.ResponseText = e.HttpContext.Request.QueryString.ToString();

            switch (result)
            {
                case "Success":
                    e.Status = PaymentResponseStatus.Success;
                    break;
                case "Failure":
                    e.Status = PaymentResponseStatus.Failed;
                    break;
                case "Cancelled":
                    e.Status = PaymentResponseStatus.Cancelled;
                    break;
                default:
                    e.Status = PaymentResponseStatus.Exception;
                    break;
            }

            e.WillHandleResponse = true;
        }
    }
}