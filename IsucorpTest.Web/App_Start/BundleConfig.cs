using System.Web.Optimization;

namespace IsucorpTest.Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                        .Include("~/Scripts/jquery-{version}.js")
                        .Include("~/Scripts/jquery-ui-{version}.js")
                        .Include("~/Scripts/jquery.mask.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/created").Include(
                      "~/Scripts/created/language.js",
                      "~/Scripts/created/globalMethods.js"));
                      
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/paginate.css",
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/themes/base/jquery-ui.css",
                        "~/Content/themes/base/datepicker.css"));

            bundles.Add(new StyleBundle("~/bundles/i-18").Include(
                "~/Scripts/i-18/datepicker-en-GB.js",
                "~/Scripts/i-18/datepicker-es.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/Knockout").Include(
                "~/Scripts/knockout-3.4.2.js"));
        }
    }
}
