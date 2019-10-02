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
                    CargarIdiomaUsuario(usuarioActual.idioma);
                    welcomeLabel.Text = string.Format(welcomeLabel.Text, usuarioActual.Nombre);
                    btnIntegridadBD.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5);

                    if (IntegridadBLL.VerificarIntegridadBD() == null)
                    {                                                
                        btnBitacora.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5);
                        btnAdministracionUsuarios.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5);
                        btnAdministracionFamilias.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5);
                        btnBackupYRestore.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5);
                    }
                    else
                    {
                        if (autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5))
                            Response.Redirect("~/IntegridadBD.aspx", false);
                        else
                        {
                            lblMensaje.Text = "El sitio se encuentra en mantenimiento, por favor intente más tarde";
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
    
                }
                /*else
                    welcomeLabel.Text = "<h1>¡Bienvenido a <span>Shoe</span>Market!<h1>";*/
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex);
            }
        }

        protected void CargarIdiomaUsuario(int id)
        {
            IdiomaBLL idiomaBLL = new IdiomaBLL();
            IdiomaBE idioma = new IdiomaBE();

            idioma = (IdiomaBE)HttpContext.Current.Application["idioma" + id];

            // botones
            btnAdministracionFamilias.Text = idioma.Detalle.Where(a => a.Control == "btnAdministrarFamilias").First().Palabra;
            btnAdministracionUsuarios.Text = idioma.Detalle.Where(a => a.Control == "btnAdministrarUsuarios").First().Palabra;
            btnBitacora.Text = idioma.Detalle.Where(a => a.Control == "btnBitacora").First().Palabra;
            btnBackupYRestore.Text = idioma.Detalle.Where(a => a.Control == "btnBackupYRestore").First().Palabra;
            // label
            welcomeLabel.Text = idioma.Detalle.Where(a => a.Control == "welcomelabel").First().Palabra;
            lblLema.Text = idioma.Detalle.Where(a => a.Control == "lblLema").First().Palabra;
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

        protected void btnAdministracionFamilias_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Familia.aspx",false);
        }
    }
}