using Orchard.UI.Resources;

namespace MK.BookStore {
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            // Create and add a new manifest
            var manifest = builder.Add();

            // Define a "common" style sheet
            manifest.DefineStyle("Common").SetUrl("common.css");

            // Define the "shoppingcart" style sheet
            manifest.DefineStyle("Cart").SetUrl("cart.css").SetDependencies("Common");

           

            manifest.DefineStyle("CartWidget").SetUrl("CartWidget.css").SetDependencies("Common");

            manifest.DefineScript("Cart").SetUrl("cart.js").SetDependencies("jQuery", "jQuery_LinqJs", "ko");
            

            manifest.DefineStyle("Summary").SetUrl("checkout-summary.css").SetDependencies("Common");

            manifest.DefineStyle("Order").SetUrl("order.css").SetDependencies("Common");

            manifest.DefineStyle("SimulatedPSP").SetUrl("simulated-psp.css").SetDependencies("Common");
        }
    }
}
