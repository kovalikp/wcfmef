using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebHostedService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var setup = AppDomain.CurrentDomain.SetupInformation;
            //var catalogDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            //var catalogs = new AggregateCatalog(new[] { new DirectoryCatalog(catalogDirectory) });
            //var container = new CompositionContainer(catalogs);

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}