using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeMarket
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public bool DBCorrupted = false;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            UpdateLogin();
        }
        public void UpdateLogin()
        {
            AutenticacionVista autenticacionVista = new AutenticacionVista();
            var usuarioActual = autenticacionVista.UsuarioActual;

            if (usuarioActual != null)
                HeadLoginStatus.InnerText = usuarioActual.Username;
            else
                HeadLoginStatus.InnerText = "Iniciar Sesion";
        }
    }
}