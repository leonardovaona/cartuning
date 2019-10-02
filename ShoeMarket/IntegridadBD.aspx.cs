using System;
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
            if (!autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5))
                
                this.Response.Redirect("~/Default.aspx",false);            
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex);
        }
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

