using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MK.BookStore.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace MK.BookStore.Driver
{
    public class BookPartDriver : ContentPartDriver<BookPart>
    {
        protected override string Prefix => "Book";

        protected override DriverResult Editor(BookPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Book_Edit", () => shapeHelper
                .EditorTemplate(TemplateName: "Parts/Book", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(BookPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
        protected override DriverResult Display(BookPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Book", () => shapeHelper.Parts_Book(
                Price: part.Price,
                Name: part.Name,
                Author: part.Author,
                Genre: part.Genre
            ));
        }
    }
}