using System;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Core.Settings.Metadata;
using Orchard.Data.Migration;

namespace MK.BookStore {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            SchemaBuilder
                .CreateTable("BookPartRecord",
                    table => table
                        .ContentPartRecord()
                        .Column<string>("Name", c => c.WithLength(50))
                        .Column<string>("Author", c => c.WithLength(50))
                        .Column<string>("Genre", c => c.WithLength(50))
                        .Column<decimal>("Price"));
            return 1;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("BookPart", part => part
                .Attachable());
            return 2;
        }
//        public int UpdateFrom2() {
//            SchemaBuilder.DropTable("BookPartRecord");
//            SchemaBuilder
//                .CreateTable("BookPartRecord",
//                    table => table
//                        .ContentPartRecord()
//                        .Column<string>("Name", c => c.WithLength(50))
//                        .Column<string>("Author", c => c.WithLength(50))
//                        .Column<string>("Genre", c => c.WithLength(50))
//                        .Column<decimal>("Price"));
//            return 3;
//        }


        public int UpdateFrom3() {

            // Define a new content type called "CartWidget"
            ContentDefinitionManager.AlterTypeDefinition("CartWidget", type => type

                // Attach the "CartWidgetPart"
                .WithPart("CartWidgetPart")
                .WithPart("CommonPart")
                // In order to turn this content type into a widget, it needs the WidgetPart
                .WithPart("WidgetPart")

                // It also needs a setting called "Stereotype" to be set to "Widget"
                .WithSetting("Stereotype", "Widget")
            );

            return 4;
        }
        public int UpdateFrom4()
        {
            SchemaBuilder.CreateTable("CustomerPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("FirstName", c => c.WithLength(50))
                .Column<string>("LastName", c => c.WithLength(50))
                .Column<string>("Title", c => c.WithLength(10))
                .Column<DateTime>("CreatedUtc")
            );

            SchemaBuilder.CreateTable("AddressPartRecord", table => table
                .ContentPartRecord()
                .Column<int>("CustomerId")
                .Column<string>("Type", c => c.WithLength(50))
            );

            ContentDefinitionManager.AlterPartDefinition("CustomerPart", part => part
                .Attachable(false)
            );

            ContentDefinitionManager.AlterTypeDefinition("Customer", type => type
                .WithPart("CustomerPart")
                .WithPart("UserPart")
            );

            ContentDefinitionManager.AlterPartDefinition("AddressPart", part => part
                .Attachable(false)
                .WithField("Name", f => f.OfType("TextField"))
                .WithField("AddressLine1", f => f.OfType("TextField"))
                .WithField("AddressLine2", f => f.OfType("TextField"))
                .WithField("Zipcode", f => f.OfType("TextField"))
                .WithField("City", f => f.OfType("TextField"))
                .WithField("Country", f => f.OfType("TextField"))
            );

            ContentDefinitionManager.AlterTypeDefinition("Address", type => type
                .WithPart("CommonPart")
                .WithPart("AddressPart")
            );

            return 5;
        }
        public int UpdateFrom5()
        {
            SchemaBuilder.CreateTable("OrderRecord", t => t
                .Column<int>("Id", c => c.PrimaryKey().Identity())
                .Column<int>("CustomerId", c => c.NotNull())
                .Column<DateTime>("CreatedAt", c => c.NotNull())
                .Column<decimal>("SubTotal", c => c.NotNull())
                .Column<decimal>("Vat", c => c.NotNull())
                .Column<string>("Status", c => c.WithLength(50).NotNull())
                .Column<string>("PaymentServiceProviderResponse", c => c.WithLength(null))
                .Column<string>("PaymentReference", c => c.WithLength(50))
                .Column<DateTime>("PaidAt", c => c.Nullable())
                .Column<DateTime>("CompletedAt", c => c.Nullable())
                .Column<DateTime>("CancelledAt", c => c.Nullable())
            );

            SchemaBuilder.CreateTable("OrderDetailRecord", t => t
                .Column<int>("Id", c => c.PrimaryKey().Identity())
                .Column<int>("OrderRecord_Id", c => c.NotNull())
                .Column<int>("ProductId", c => c.NotNull())
                .Column<int>("Quantity", c => c.NotNull())
                .Column<decimal>("UnitPrice", c => c.NotNull())
                .Column<decimal>("VatRate", c => c.NotNull())
            );

//            SchemaBuilder.CreateForeignKey("Order_Customer", "OrderRecord", new[] { "CustomerId" }, "CustomerPartRecord", new[] { "Id" });
//            SchemaBuilder.CreateForeignKey("OrderDetail_Order", "OrderDetailRecord", new[] { "OrderRecord_Id" }, "OrderRecord", new[] { "Id" });
//            SchemaBuilder.CreateForeignKey("OrderDetail_Book", "OrderDetailRecord", new[] { "ProductId" }, "BookPartRecord", new[] { "Id" });

            return 6;
        }
    }
}