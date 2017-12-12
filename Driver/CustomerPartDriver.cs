using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MK.BookStore.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace MK.BookStore.Driver
{
    public class CustomerPartDriver : ContentPartDriver<CustomerPart>
    {

        protected override string Prefix => "Customer";

        protected override DriverResult Editor(CustomerPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Customer_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/Customer", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(CustomerPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            var user = part.User;
            updater.TryUpdateModel(user, Prefix, new[] { "Email" }, null);

            return Editor(part, shapeHelper);
        }
    }
}