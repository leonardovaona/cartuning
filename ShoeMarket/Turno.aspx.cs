using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace ShoeMarket
{
    public partial class Turno : System.Web.UI.Page
    {
        private int idPedido;
        protected void Page_Load(object sender, EventArgs e)
        {            
            //lblMensaje.ForeColor = System.Drawing.Color.;            
            idPedido = Convert.ToInt32(Request.QueryString["id"]);
            divConfirmarTurno.Visible = false;
            divLeyenda.Visible = true;
            btnPago.Visible = false;
            btnConfirmar.Visible = true;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pedido.aspx?id=" + idPedido, false);
        }

        protected void calendarTurno_SelectionChanged(object sender, EventArgs e)
        {
            lblFecha.Text = calendarTurno.SelectedDate.ToShortDateString();
        }
            
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try {
                TurnoBLL turnoBLL = new TurnoBLL();
                TurnoBE turno = new TurnoBE();
                AutenticacionVista autenticacionVista = new AutenticacionVista();

                turno.Fecha = calendarTurno.SelectedDate;
                string hora = selectHora.Value.ToString();
                string subs = hora.Substring(0, hora.IndexOf(":"));
                turno.Hora = Convert.ToInt32(subs);                
                turno.Usuario = autenticacionVista.UsuarioActual;

                if (!turnoBLL.Alta(turno))
                {
                    throw new System.ArgumentException("Ocurrio un error al guardar el turno, por favor intente mas tarde.");
                }
                else
                {
                    PedidoBLL pedidoBLL = new PedidoBLL();
                    pedidoBLL.Modificacion(idPedido, turno.Id);
                    divConfirmarTurno.Visible = true;
                    divLeyenda.Visible = true ;
                    btnConfirmar.Visible = false;
                    btnPago.Visible = true;
                }

            }
            catch (Exception ex)            
            {             
                lblMensaje.Text = ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        
        }

        protected void btnPago_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pago.aspx?idpedido=" + idPedido, false);
        }

    }
}