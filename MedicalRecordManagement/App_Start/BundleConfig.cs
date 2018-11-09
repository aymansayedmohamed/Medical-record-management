using System.Web.Optimization;

namespace MedicalRecordManagement
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
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/JSBundles").Include(
        "~/Scripts/angular.js",
        "~/Scripts/angular-messages.js",
        "~/Scripts/angular-route.js",
        "~/Scripts/angular-toastr.tpls.js",
        "~/Scripts/angular-slimscroll.js",
        "~/Scripts/angular-route.js",
        "~/Scripts/AngularUI/ui-router.js",
        "~/Scripts/angular-animate.js",
        "~/Scripts/angular-ui-router", 
        "~/Scripts/select.js",
        "~/Scripts/angular-strap.min.js",
        "~/Scripts/angular-loader.min.js",
        "~/Scripts/angular-strap.tpl.min.js",
        "~/Content/js/vendor/jquery.min.js",
        "~/Content/js/vendor/lightslider.min.js",
        "~/Content/js/vendor/bootstrap.bundle.min.js",
        "~/Scripts/ocLazyLoad.js",
        "~/app/routeConfig.js",
        "~/app/app.js",
        "~/Scripts/loading-bar.min.js",
        "~/Content/js/main.js",
        "~/app/Directives/resourceKeyDirective.js",
        "~/app/Controllers/main.controller.js",
        "~/app/Controllers/wallet.controller.js",
        "~/app/Services/wallet.service.js",
        "~/app/ngStorage.min.js",
        "~/app/angular-cookies.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
