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
    }
}