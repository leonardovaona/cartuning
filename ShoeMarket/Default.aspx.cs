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
                    btnIntegridadBD.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 19);

                    if (IntegridadBLL.VerificarIntegridadBD() == null)
                    {
                        btnBitacora.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 15);
                        btnAdministracionUsuarios.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 17);
                        btnAdministracionFamilias.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 21);
                        btnBackupYRestore.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 18);
                        btnIdioma.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 20);
                        btnProdcuto.Visible = autenticacionVista.UsuarioPoseePermiso(usuarioActual, 16);
                    }
                    else
                    {
                        if (autenticacionVista.UsuarioPoseePermiso(usuarioActual, 18))
                            Response.Redirect("~/IntegridadBD.aspx", false);
                        else
                        {
                            lblMensaje.Text = "El sitio se encuentra en mantenimiento, por favor intente más tarde";
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                }

                CargarProductos();

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
            btnIntegridadBD.Text = idioma.Detalle.Where(a => a.Control == "btnIntebridadBD").First().Palabra;
            btnIdioma.Text = idioma.Detalle.Where(a => a.Control == "btnIdioma").First().Palabra;

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
            welcomeLabel.Text = idioma.Detalle.Where(a => a.Control == "welcomelabel").First().Palabra;
            lblLema.Text = idioma.Detalle.Where(a => a.Control == "lblLema").First().Palabra;
        }

        protected void CargarProductos()
        {
            ProductoBLL productoBLL = new ProductoBLL();
            List<ProductoBE> productos = new List<ProductoBE>();
            productos = productoBLL.ConsultaRango(null, null);
            // Creación de imagenes para hacer clic ahi
            foreach (var producto in productos)
            {
                HyperLink hyperLink = new HyperLink();
                if (producto.Imagenes.Count > 0)
                { 
                    hyperLink.ImageUrl = "data:image;base64," + Convert.ToBase64String(producto.Imagenes[0]);
                }
                hyperLink.NavigateUrl = "Producto.aspx?id=" + producto.Id;                
                hyperLink.ID = Convert.ToString(producto.Id);                
                hyperLink.ImageHeight = 200;
                hyperLink.ImageWidth = 200;
                hyperLink.ToolTip = producto.Descripcion;
                hyperLink.Visible = true;
                divProductos.Controls.Add(hyperLink);                
                divProductos.Controls.Add(new LiteralControl("<br />"));
                divProductos.Controls.Add(new LiteralControl("<br />"));  

            }
        }

        protected void btnBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pedido.aspx", false);
        }

        protected void btnIntegridadBD_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/IntegridadBD.aspx", false);
        }

        protected void btnAdministracionUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuario.aspx", false);
        }

        protected void btnAdministracionFamilias_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Familia.aspx", false);
        }

        protected void btnIdioma_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Idioma.aspx", false);
        }

        protected void btnProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CrearProducto.aspx", false);
        }
    }
}