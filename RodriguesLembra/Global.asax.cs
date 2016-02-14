using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RodriguesLembra
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
                {
                    var configuration = new Migrations.Configuration();
                    var migrator = new DbMigrator(configuration);
                    migrator.Update();
                }
            }
            catch (ArgumentNullException)
            {
                Trace.TraceWarning("MigrateDatabaseToLatestVersion key is null. Database not migrated.");
            }
        }
    }
}
