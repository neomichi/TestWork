using System.Web;
using System.Web.Optimization;

namespace Cinema.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/messages_ru.js",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
              "~/Scripts/datetimepicker/moment.js",
              "~/Scripts/datetimepicker/moment_ru.js",
              "~/Scripts/datetimepicker/bootstrap-datepicker.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").
                      Include("~/Content/bootstrap-datetimepicker-standalone.css").
                      Include("~/Content/bootstrap.css", new CssRewriteUrlTransform()).
                      Include("~/Content/site.css"));
        }
    }
}
