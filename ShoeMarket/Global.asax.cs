using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BLL;
using BE;
using System.Threading;
using System.Globalization;

namespace ShoeMarket
{
    public class Global : System.Web.HttpApplication
    {
        void Application_AcquireRequestState(object sender, EventArgs e)
        {
                 
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            IdiomaBLL idiomaBLL = new IdiomaBLL();
            List<IdiomaBE> idiomas = new List<IdiomaBE>();
            idiomas = idiomaBLL.ConsultaRango(null, null);

            foreach (IdiomaBE idioma in idiomas)
            {
                HttpContext.Current.Application["idioma" +idioma.Id] = idioma;                
            }
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