using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BE;

namespace ShoeMarket
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public bool DBCorrupted = false;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateLogin();
                CargarIdioma();
            }

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

        public void CargarIdioma()
        {
            IdiomaBLL idiomaBLL = new IdiomaBLL();
            List<IdiomaBE> idiomas = new List<IdiomaBE>();
            idiomas = idiomaBLL.ConsultaRango(null, null);

            foreach (IdiomaBE idioma in idiomas)
            {
                ListItem newItem = new ListItem();
                newItem.Text = idioma.Codigo;
                newItem.Value = Convert.ToString(idioma.Id);
                ddlIdioma.Items.Add(newItem);
            }
        }
    }
}