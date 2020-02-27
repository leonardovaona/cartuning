using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace ShoeMarket
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AutenticacionVista autenticacionVista = new AutenticacionVista();
            var usuarioActual = autenticacionVista.UsuarioActual;
            if (!autenticacionVista.UsuarioPoseePermiso(usuarioActual, 17))
                this.Response.Redirect("~/Default.aspx");

            divDatos.Visible = false;

            if (!Page.IsPostBack)
            {
                LlenarGrilla();
                CargarListaPermisos();
                divPermisos.Visible = false;
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ListItem newItem = new ListItem();
            newItem = lbPermisos.SelectedItem;
            lbPermisos.Items.Remove(newItem);
            lbPermisosAsociados.Items.Add(newItem);
        }

        private void CargarListaPermisos()
        {

            if (lbPermisosAsociados.Items.Count != 0)
            {
                lbPermisosAsociados.Items.Clear();
            }

            PermisoBLL permisoBLL = new PermisoBLL();
            List<PermisoBE> permisoList = permisoBLL.ConsultaRango(null, null);
            foreach (PermisoBE permiso in permisoList)
            {
                ListItem newItem = new ListItem();
                newItem.Text = permiso.Nombre;
                newItem.Value = Convert.ToString(permiso.Id);
                lbPermisos.Items.Add(newItem);
            }

            GridViewRow row = dataGridUsuario.SelectedRow;
            if (row != null)
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                UsuarioBE usuario = new UsuarioBE();
                usuario.Id = Convert.ToInt32(row.Cells[0].Text);
                usuario = usuarioBLL.Consulta(ref usuario);
                

                foreach (PermisoBE permiso in usuario.Perfil)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = permiso.Nombre;
                    newItem.Value = Convert.ToString(permiso.Id);
                    lbPermisosAsociados.Items.Add(newItem);
                }
            }
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAsociarPermiso_Click(object sender, EventArgs e)
        {
            divPermisos.Visible = true;
            CargarListaPermisos();
        }
        protected List<PermisoBE> CargarPermisosFromList()
        {
            System.Collections.Generic.List<PermisoBE> permisosList = new List<PermisoBE>();
            PermisoBLL permisoBLL = new PermisoBLL();
            foreach (ListItem listItem in lbPermisosAsociados.Items)
            {
                PermisoBE permiso = new PermisoBE();
                permiso.Id = Convert.ToInt16(listItem.Value);
                permiso = permisoBLL.Consulta(ref permiso);
                permisosList.Add(permiso);
            }

            return permisosList;
        }

        protected void btnGuardarPermisos_Click(object sender, EventArgs e)
        {
            GridViewRow row = dataGridUsuario.SelectedRow;
            if (row != null)
            {
                PermisoBLL permisoBLL = new PermisoBLL();
                UsuarioBLL usuarioBLL = new UsuarioBLL();

                UsuarioBE usuario = new UsuarioBE();
                usuario.Id = Convert.ToInt32(row.Cells[0].Text);
                if (usuarioBLL.QuitarPermiso(usuario))
                {                    
                    usuario.Perfil = CargarPermisosFromList();
                    if (usuarioBLL.AgregarPermiso(usuario))
                    {
                        lblMensaje.Text = "Se asociaron los permisos correctamente";
                        divPermisos.Visible = false;
                    }
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void dataGridUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {            
            dataGridUsuario.PageIndex = e.NewPageIndex;
            LlenarGrilla();
        }

    }
}