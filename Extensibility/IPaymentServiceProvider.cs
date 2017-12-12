using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Events;

namespace MK.BookStore.Extensibility
{
    public interface IPaymentServiceProvider : IEventHandler
    {
        void RequestPayment(PaymentRequest e);
        void ProcessResponse(PaymentResponse e);
    }
}