using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace E_Store.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                      "~/Scripts/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalnumoverride").Include(
                "~/Scripts/jquery.validate.number.override.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom-scripts").Include(
               "~/Scripts/Scripts.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/ErrorStyles",
                      "~/Content/bootstrap-select.min.css"));

#if DEBUG

            BundleTable.EnableOptimizations = false;
#else
        BundleTable.EnableOptimizations = true;
#endif
        }
    }
}