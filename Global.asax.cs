using System;
using Syncfusion.Licensing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProyectoAlmaInicio
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
        SyncfusionLicenseProvider.RegisterLicense("MjE1NDkzQDMxMzcyZTMzMmUzMEh4SlpKNjJGeGczMys1S0tIOThCb1IwVTFUYmxuZzV0QlFUYnRWYkRjZTg9");
AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
