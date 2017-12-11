﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MK.BookStore.Models
{
    public class OrderRecord
    {
        public virtual int Id { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal Vat { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual IList<OrderDetailRecord> Details { get; private set; }
        public virtual string PaymentServiceProviderResponse { get; set; }
        public virtual string PaymentReference { get; set; }
        public virtual DateTime? PaidAt { get; set; }
        public virtual DateTime? CompletedAt { get; set; }
        public virtual DateTime? CancelledAt { get; set; }

//        public decimal Total
//        {
//            get { return SubTotal + Vat; }
//            set { }
//        }
//
//        public string Number
//        {
//            get { return (Id + 1000).ToString(CultureInfo.InvariantCulture); }
//            set { }
//        }

        public OrderRecord()
        {
            Details = new List<OrderDetailRecord>();
        }

//        public virtual void UpdateTotals()
//        {
//            var subTotal = 0m;
//            var vat = 0m;
//
//            foreach (var detail in Details)
//            {
//                subTotal += detail.SubTotal;
//                vat += detail.Vat;
//            }
//
//            SubTotal = subTotal;
//            Vat = vat;
//        }
    }
}