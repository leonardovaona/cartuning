using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BE;
using BLL;
using ShoeMarket;



namespace ShoeMarket
{
    public partial class Login : System.Web.UI.Page
    {
        private AutenticacionVista vistaAutenticacion = new AutenticacionVista();
        private int MaxIntentos = 3;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack)
                {
                    if (vistaAutenticacion.UsuarioActual != null)
                    {
                        divIniciarSesion.Visible = false;
                        lblUsuarioActual.Text = vistaAutenticacion.UsuarioActual.Username;
                        CargarIdiomaUsuario();
                    }
                    else
                        divIniciarSesion.Visible = true;
                    divCerrarSesion.Visible = !divIniciarSesion.Visible;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblUsuarioActual.Text = ErrorHandler.ObtenerMensajeDeError(ex);
                lblUsuarioActual.ForeColor = System.Drawing.Color.OrangeRed;
            }
        }

        protected void CargarIdiomaUsuario()
        {
            IdiomaBLL idiomaBLL = new IdiomaBLL();
            IdiomaBE idioma = new IdiomaBE();

            idioma = (IdiomaBE)HttpContext.Current.Application["idioma" + vistaAutenticacion.UsuarioActual.idioma];

            //btnLogin.Text = idioma.Detalle.Where(a => a.Control == "botonlogin").First().Palabra;

        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            try
            {
                UsuarioBE usuarioLogin = null;

                
                usuarioLogin = vistaAutenticacion.CrearUsuarioParaIniciarSesion(txtUsuario.Text, txtClave.Text);


                if ((vistaAutenticacion.UsuarioBloqueado(usuarioLogin))== 1)
                {
                    lblMensaje.Text = "El usuario que ingreso se encuentra bloqueado. ";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else if (vistaAutenticacion.IniciarSesion(usuarioLogin))
                {

                    
                    lblMensaje.Text = "";
                    lblUsuarioActual.Text = vistaAutenticacion.UsuarioActual.Username;
                    divCerrarSesion.Visible = true;
                    divIniciarSesion.Visible = !divCerrarSesion.Visible;
                    (this.Master as Site).UpdateLogin();


                    bool bdOk = false;
                    string sMsg = null;                    
                    try
                    {
                        
                        sMsg = IntegridadBLL.VerificarIntegridadBD();

                        if ((sMsg == null))
                            bdOk = true;
                    }
                    catch (Exception ex)
                    {
                    }
                    if (!(bdOk))
                    {
                        AutenticacionVista autenticacionVista = new AutenticacionVista();
                        var usuarioActual = autenticacionVista.UsuarioActual;
                        if (autenticacionVista.UsuarioPoseePermiso(usuarioActual, 5))
                            this.Response.Redirect("~/IntegridadBD.aspx", false);
                    }
                    else
                        this.Response.Redirect("~/Default.aspx", false);
                }
                else
                {
                    if ((vistaAutenticacion.IntentosFallidos >= MaxIntentos))
                    {
                        vistaAutenticacion.BloquearUsuario(usuarioLogin);
                        lblMensaje.Text = "Intento numero " + System.Convert.ToString(MaxIntentos) + " fallido. Se ha bloqueado el usuario " + usuarioLogin.Nombre;
                        vistaAutenticacion.IntentosFallidos = 0;
                    }
                    else
                    {
                        var strIntentos = "";
                        if ((vistaAutenticacion.IntentosFallidos > 0))
                            strIntentos = "Intentos restantes " + System.Convert.ToString(MaxIntentos - vistaAutenticacion.IntentosFallidos);
                        lblMensaje.Text = "Usuario o contraseña invalida, por favor vuelva a intentarlo. " + strIntentos;
                    }
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ErrorHandler.ObtenerMensajeDeError(ex);
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                vistaAutenticacion.CerrarSesion();
                divIniciarSesion.Visible = true;
                divCerrarSesion.Visible = !divIniciarSesion.Visible;
                (this.Master as Site).UpdateLogin();
                
            }
            catch (Exception ex)
            {
                lblUsuarioActual.Text = ErrorHandler.ObtenerMensajeDeError(ex);
            }
        }
    }
}

