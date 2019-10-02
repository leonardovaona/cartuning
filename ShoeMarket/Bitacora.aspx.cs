using System;
using ShoeMarket.Vistas;

namespace ShoeMarket
{
    public partial class Bitacora : System.Web.UI.Page
    {
        private UsuarioVista _vistaUsuarios = new UsuarioVista();
        private PermisoVista _vistaPermisos = new PermisoVista();

        private BitacoraVista _vista = new BitacoraVista();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {                
                AutenticacionVista autenticacionVista = new AutenticacionVista();
                var usuarioActual = autenticacionVista.UsuarioActual;
                if (!autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5))             
                    this.Response.Redirect("~/Default.aspx");

                lblMensaje.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    MultiView1.ActiveViewIndex = 0;
                    LlenarGrilla();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LlenarGrilla()
        {
            this._vista.LlenarGrilla(ref grillaBitacora);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Default.aspx", false);
        }
    }
}