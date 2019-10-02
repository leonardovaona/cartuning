using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;

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

            divDatos.Visible = false;

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

        protected void dataGridUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow Row = dataGridUsuario.SelectedRow;            
        }


        protected void dataGridUsuario_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = dataGridUsuario.Rows[e.NewSelectedIndex];
            divDatos.Visible = false;
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            divDatos.Visible = true;   
            GridViewRow row = dataGridUsuario.SelectedRow;
            if (row != null)
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                UsuarioBE usuarioSelected = new UsuarioBE();
                usuarioSelected.Id = Convert.ToInt32(row.Cells[0].Text);
                usuarioSelected = usuarioBLL.Consulta(ref usuarioSelected);
                txtNombre.Text = usuarioSelected.Nombre;
                txtApellido.Text = usuarioSelected.Apellido;
                txtUsername.Text = usuarioSelected.Username;
                
                if (usuarioSelected.Bloqueado == 1)
                {
                    rbBloqueadoTrue.Checked = true;
                    rbBloqueadoFalse.Checked = false;
                }
                else
                {
                    rbBloqueadoTrue.Checked = false;
                    rbBloqueadoFalse.Checked = true;
                }

                if (usuarioSelected.Eliminado == 1)
                {
                    rbEliminadoTrue.Checked = true;
                    rbEliminadoFalse.Checked = false;
                }
                else
                {
                    rbEliminadoTrue.Checked = false;
                    rbEliminadoFalse.Checked = true;
                }
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            UsuarioBE usuarioBaja = new UsuarioBE();

            GridViewRow row = dataGridUsuario.SelectedRow;
            if (row != null)
            {
                usuarioBaja.Id = Convert.ToInt16(row.Cells[0].Text);
                usuarioBaja.Eliminado = 1;
                if (usuarioBLL.Baja(ref usuarioBaja))
                {
                    lblMensaje.Text = "Usuario eliminado correctamente";
                    LlenarGrilla();
                }
             }
        }
    }
}