using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MK.BookStore.Models
{
    public class OrderDetailRecord
    {
        public virtual int Id { get; set; }
        public virtual int OrderRecord_Id { get; set; }
        public virtual int ProductId { get; set; }

        public virtual int Quantity { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual decimal VatRate { get; set; }

//        public virtual decimal UnitVat
//        {
//            get { return UnitPrice * VatRate; }
//            set { }
//        }
//
//        public virtual decimal Vat
//        {
//            get { return UnitVat * Quantity; }
//            set { }
//        }
//        public virtual decimal SubTotal
//        {
//            get { return UnitPrice * Quantity; }
//            set { }
//        }
//        
//        public virtual decimal Total
//        {
//            get { return SubTotal + Vat; }
//            set { }
//        }
    }
}