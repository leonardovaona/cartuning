using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeMarket
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AutenticacionVista autenticacionVista = new AutenticacionVista();
            var usuarioActual = autenticacionVista.UsuarioActual;
            if (!autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5))
                this.Response.Redirect("~/Default.aspx");
                        
            if (!Page.IsPostBack)
            {
            
                LlenarGrilla();
            }
        }

        private void LlenarGrilla()
        {
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            dataGridUsuario.DataSource = usuarioBLL.ConsultaRango(null, null);
            dataGridUsuario.DataBind();
        }
    }
}