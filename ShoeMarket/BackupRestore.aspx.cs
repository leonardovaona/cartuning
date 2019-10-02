using System;
using System.Collections.Generic;
using ShoeMarket.Vistas;
using System.Web.UI;
using BE;
using BLL;

namespace ShoeMarket
{

    public partial class BackupRestore : System.Web.UI.Page
    {
        private UsuarioVista _vistaUsuarios = new UsuarioVista();
        private PermisoVista _vistaPermisos = new PermisoVista();

        private BitacoraVista _vista = new BitacoraVista();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                // Valido si el usuario posee permiso para acceder a esta página.
                AutenticacionVista autenticacionVista = new AutenticacionVista();
                var usuarioActual = autenticacionVista.UsuarioActual;
                if (!autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5))
                    // Si no lo tiene se redirecciona a página de inicio.
                    this.Response.Redirect("~/Default.aspx");

                lblMensaje.Text = string.Empty;
                if (!IsPostBack)
                    this.llenarGrilla();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void llenarGrilla()
        {
            BackupRestoreBLL backupRestore = new BackupRestoreBLL();
            List<BackupRestoreBE> Lista = backupRestore.BackupLista();
            
           
            dataGridBackup.DataSource = Lista;
            dataGridBackup.DataBind();
        }

        protected void btnRealizarBackup_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRutaBackup.Text))
            {
                lblMensaje.Text = "Debe especificar una ruta destino para el backup";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNombreBackup.Text))
                txtNombreBackup.Text = DateTime.Now.ToString();


            BackupRestoreBLL mBackup = new BackupRestoreBLL();
            int tamFragment = -1;
            if ((!string.IsNullOrWhiteSpace(txtTamanoBackup.Text)))
                tamFragment = System.Convert.ToInt32(txtTamanoBackup.Text);

            try
            {
                if (IntegridadBLL.VerificarIntegridadBD() == null)
                {
                    if ((mBackup.RealizarBackup(txtNombreBackup.Text, txtRutaBackup.Text, tamFragment)))
                    {
                        lblMensaje.Text = "Backup realizado con exito";
                        lblMensaje.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                else
                {
                    lblMensaje.Text = "Error de digitos verificadores!!";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            llenarGrilla();
        }

        protected void btnRealizarRestore_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBackupId.Text))
            {
                lblMensaje.Text = "Debe especificar una grupo de backup a restaurar";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            BackupRestoreBLL mBackup = new BackupRestoreBLL();
            List<BackupRestoreBE> lista = mBackup.BackupLista();
            var listaBackups = new List<string>();
            foreach (BackupRestoreBE objDTO in lista)
            {
                if ((objDTO.Id == System.Convert.ToInt32(txtBackupId.Text)))
                    listaBackups.Add(objDTO.Path);
            }

            if ((listaBackups.Count == 0))
            {
                lblMensaje.Text = "Grupo de backup no existente. Por favor seleccione otro";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int cantFrag = 1;
            try
            {
                if ((mBackup.RealizarRestore(listaBackups)))
                {
                    lblMensaje.Text = "Restore realizado con exito";
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

            llenarGrilla();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Default.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}