using System.Web;
using System.Web.Optimization;

namespace ProyectoFinalL2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Content/js/vendor/jquery-2.2.4.min.js",
                    "~/Content/js/vendor/bootstrap.min.js",
                    "~/Content/js/easing.min.js",
                    "~/Content/js/hoverIntent.js",
                    "~/Content/js/superfish.min.js",
                    "~/Content/js/jquery.ajaxchimp.min.js",
                    "~/Content/js/jquery.magnific-popup.min.js",
                    "~/Content/js/owl.carousel.min.js",
                    "~/Content/js/jquery.nice-select.min.js",
                    "~/Content/js/mail-script.js",
                    "~/Content/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/linearicons.css",
                       "~/Content/css/font-awesome.min.css",
                        "~/Content/css/bootstrap.css",
                        "~/Content/css/magnific-popup.css",
                        "~/Content/css/nice-select.css",
                        "~/Content/css/animate.min.css",
                        "~/Content/css/owl.carousel.css",
                        "~/Content/css/main.css"));
        }
    }
}
