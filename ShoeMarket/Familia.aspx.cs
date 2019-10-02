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
    public partial class CrearFamilia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AutenticacionVista autenticacionVista = new AutenticacionVista();
            var usuarioActual = autenticacionVista.UsuarioActual;
            if (!autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5))
                this.Response.Redirect("~/Default.aspx");

            divGrilla.Visible = true;
            
            if (!Page.IsPostBack)
            {
                LlenarGrilla();
                divDatos.Visible = false;
                divAsociarPermiso.Visible = false;
            }
        }

        private void LlenarGrilla()
        {
            PermisoBLL permisoBLL = new PermisoBLL();
            PermisoBE permisoBE = new PermisoBE();            
            dataGridFamilia.DataSource = permisoBLL.ConsultaRango(null, null);
            dataGridFamilia.DataBind();
        }

        private void CargarListaPermisos()
        {
            
            if (lblPermisosAsignados.Items.Count != 0)
            {                
                    lblPermisosAsignados.Items.Clear();            
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

            GridViewRow row = dataGridFamilia.SelectedRow;
            if (row != null)
            {
                PermisoBE permisoBE = new PermisoBE();
                permisoBE.Id = Convert.ToInt32(row.Cells[0].Text);
                FamiliaBE familia = new FamiliaBE(permisoBE);
                familia.Permisos = permisoBLL.ConsultaPermisos(familia);

                foreach (PermisoBE permiso in familia.Permisos)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = permiso.Nombre;
                    newItem.Value = Convert.ToString(permiso.Id);
                    lblPermisosAsignados.Items.Add(newItem);
                }
            }
        }

        protected void dataGridFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow Row = dataGridFamilia.SelectedRow;
            divAsociarPermiso.Visible = false;
        }
        
        protected void dataGridFamilia_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = dataGridFamilia.Rows[e.NewSelectedIndex];
            divDatos.Visible = false;
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            divDatos.Visible = true;
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            divDatos.Visible = true;
            GridViewRow row = dataGridFamilia.SelectedRow;
            if (row != null)
            {
                PermisoBLL permisoBLL = new PermisoBLL();
                PermisoBE selectedPermiso = new PermisoBE();
                selectedPermiso.Id = Convert.ToInt32(row.Cells[0].Text);
                selectedPermiso = permisoBLL.Consulta(ref selectedPermiso);
                txtNombre.Text = selectedPermiso.Nombre;
                txtDescripcion.Text = selectedPermiso.Descripcion;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            FamiliaBLL familiaBLL = new FamiliaBLL();
            FamiliaBE newFamilia = new FamiliaBE();

            PermisoBLL permisoBLL = new PermisoBLL();
            PermisoBE newPermiso = new PermisoBE();

            newPermiso.Nombre = txtNombre.Text;
            newPermiso.Descripcion = txtDescripcion.Text;

            if (permisoBLL.Alta(ref newPermiso))
            {
                lblMensaje.Text = "Se genero la familia correctamente";
                lblMensaje.BackColor = System.Drawing.Color.Blue;
                LlenarGrilla();
            }
            else
            {
                lblMensaje.Text = "Error al generar la familia";
                lblMensaje.BackColor = System.Drawing.Color.Red;
            }
        }

        protected void btnAsociarPermiso_Click(object sender, EventArgs e)
        {
            divAsociarPermiso.Visible = true;
            CargarListaPermisos();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ListItem newItem = new ListItem();
            newItem = lbPermisos.SelectedItem;
            lbPermisos.Items.Remove(newItem);
            lblPermisosAsignados.Items.Add(newItem);                                       
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            ListItem newItem = new ListItem();
            newItem = lblPermisosAsignados.SelectedItem;
            lblPermisosAsignados.Items.Remove(newItem);
            lbPermisos.Items.Add(newItem);
        }

        protected void btnGuardarAsociados_Click(object sender, EventArgs e)
        {
            GridViewRow row = dataGridFamilia.SelectedRow;
            if (row !=null)
            {
                PermisoBLL permisoBLL = new PermisoBLL();
                FamiliaBLL familiaBLL = new FamiliaBLL();
                FamiliaBE familia = new FamiliaBE();
                familia.Id = Convert.ToInt32(row.Cells[0].Text);
                if (permisoBLL.QuitarPermisos(familia))
                {                    
                    PermisoBE permisoAsigado = new PermisoBE();
                    familia.Permisos = CargarPermisosFromList();
                    if (permisoBLL.AgregarPermisos(familia))
                    {
                        lblMensaje.Text = "Se asociaron los permisos correctamente";
                    }
                }
            }            
        }

        protected List<PermisoBE> CargarPermisosFromList()
        {
            List<PermisoBE> permisosList = new List<PermisoBE>();
            PermisoBLL permisoBLL = new PermisoBLL();
            foreach (ListItem listItem in lblPermisosAsignados.Items)
            {
                    PermisoBE permiso = new PermisoBE();
                    permiso.Id = Convert.ToInt16(listItem.Value);
                    permiso = permisoBLL.Consulta(ref permiso);
                    permisosList.Add(permiso);
            }
            
            return permisosList;
        }
    }
}