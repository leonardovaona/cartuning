using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;

namespace ShoeMarket
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "Debe ingresar su nombre";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblMensaje.Text = "Debe ingresar su apellido";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblMensaje.Text = "Debe ingresar un nombre de usuario";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMensaje.Text = "Debe ingresar un Email";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                lblMensaje.Text = "Debe ingresar un DNI";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtClave1.Text))
            {
                lblMensaje.Text = "Debe ingresar su clave";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtClave2.Text))
            {
                lblMensaje.Text = "Debe ingresar su clave";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (txtClave1.Text != txtClave2.Text)
            {
                lblMensaje.Text = "Las claven deben coincidir";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            UsuarioBE usuario = new UsuarioBE();
            usuario.Nombre = txtNombre.Text;
            usuario.Apellido = txtApellido.Text;
            usuario.Username = txtUsername.Text;
            usuario.Email = txtEmail.Text;
            usuario.DNI = txtDNI.Text;
            usuario.Clave = Encrypter.EncriptarSHA512(txtClave1.Text);
                        
            UsuarioBLL mUsuario = new UsuarioBLL();
            
            try
            {
                if (mUsuario.Alta(ref usuario))
                {
                    lblregistroOk.Text = "El usuario se ha registrado con exito";
                    lblregistroOk.ForeColor = System.Drawing.Color.Blue;
                    registroOk.Visible = true;
                    regitrarUsuario.Visible = false;                    
                }
                else {
                    registroOk.Visible = false;
                    regitrarUsuario.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Default.aspx");
        }
    }
}