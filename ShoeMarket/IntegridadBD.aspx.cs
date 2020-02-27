using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace ShoeMarket
{
    public partial class IntegridadBD : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        { 
            AutenticacionVista autenticacionVista = new AutenticacionVista();
            var usuarioActual = autenticacionVista.UsuarioActual;
                if (!autenticacionVista.UsuarioPoseePermiso(usuarioActual, 19))
                {
                    this.Response.Redirect("~/Default.aspx", false);
                }
                CargarIdiomaUsuario(usuarioActual.idioma);
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
            btnVerificarIntegridad.Text = idioma.Detalle.Where(a => a.Control == "btnEjecutar").First().Palabra;
            btnRecalcularDigitosVerificadores.Text = idioma.Detalle.Where(a => a.Control == "btnEjecutar").First().Palabra;
            btnRestaurar.Text = idioma.Detalle.Where(a => a.Control == "btnRestaurar").First().Palabra;
            //btnVolver.Text = idioma.Detalle.Where(a => a.Control == "btnVolver").First().Palabra;

            //Master Page            
            MasterPage masterPage = new MasterPage();
            masterPage = this.Master;
            Label lblLogin = new Label();
            lblLogin = masterPage.FindControl("lblLogin") as Label;
            lblLogin.Text = idioma.Detalle.Where(a => a.Control == "HeadLoginStatus").First().Palabra;
            Label lblRegistrarse = new Label();
            lblRegistrarse = masterPage.FindControl("lblRegistrarse") as Label;
            lblRegistrarse.Text = idioma.Detalle.Where(a => a.Control == "HeadCrearUsuario").First().Palabra;

            // label
            lblIntegridad.Text = idioma.Detalle.Where(a => a.Control == "lblIntegridad").First().Palabra;
            lblCalcularIntegridad.Text = idioma.Detalle.Where(a => a.Control == "lblCalcularIntegridad").First().Palabra;
            lblRecalcularDigitos.Text = idioma.Detalle.Where(a => a.Control == "lblRecalcularDigitos").First().Palabra;
            lblRestaurar.Text = idioma.Detalle.Where(a => a.Control == "lblRestaurar").First().Palabra;

        }

        protected void btnVolver_Click(object sender, EventArgs e)

    {
        this.Response.Redirect("~/Default.aspx",false);
    }

        protected void btnVerificarIntegridad_Click(object sender, EventArgs e)
        {
            try
            {
                string sMsg = "";
                sMsg = IntegridadBLL.VerificarIntegridadBD();
                if ((sMsg == null))
                {
                    lblMensaje.Text = "OK. Test de integridad finalizado con exito.";
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    lblMensaje.Text = sMsg;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR. Test de integridad finalizado con error. " + ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnRecalcularDigitosVerificadores_Click(object sender, EventArgs e)
        {
            try
            {
                IntegridadBLL.RegenerarDigitosVerificadores();
                lblMensaje.Text = "OK. Digitos Verificadores regenerados con exito.";
                lblMensaje.ForeColor = System.Drawing.Color.Blue;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "ERROR. Regenericacion de digitos fallida. " + ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/BackupRestore.aspx",false);
        }

    }
}

