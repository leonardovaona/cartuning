using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace ShoeMarket
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = string.Empty;
                welcomeLabel.Text = string.Empty;

                AutenticacionVista autenticacionVista = new AutenticacionVista();
                var usuarioActual = autenticacionVista.UsuarioActual;

                if (usuarioActual != null)
                {
                    welcomeLabel.Text = string.Format("<h1>¡Bienvenido {0} a <span>Shoe</span>Market!<h1>", usuarioActual.Nombre);
                    btnIntegridadBD.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 1);

                    if (true) //IntegridadBLL.VerificarIntegridadBD() == null)
                    {

                        btnCarrito.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 3);
                        btnCambioDePrecios.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 2);
                        btnBitacora.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 4);
                        btnAdministracionUsuarios.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5);
                        btnBackupYRestore.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 1);
                    }
                    else
                    {
                        if (autenticacionVista.UsuarioPoseePermiso(usuarioActual, 1))
                            Response.Redirect("~/IntegridadBD.aspx", false);
                        else
                        {
                            lblMensaje.Text = "El sitio se encuentra en mantenimiento, por favor intente más tarde";
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
    
                }
                else
                    welcomeLabel.Text = "<h1>¡Bienvenido a <span>Shoe</span>Market!<h1>";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex);
            }
        }
        
        protected void btnCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Carrito.aspx");
        }

        protected void btnBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Bitacora.aspx", false);
        }

        protected void btnIntegridadBD_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/IntegridadBD.aspx",false);
        }

        protected void btnAdministracionUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuario.aspx", false);
        }
    }
}