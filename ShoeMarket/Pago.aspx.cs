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
    public partial class Pago : System.Web.UI.Page
    {
        private int idPedido = 0;
        private PedidoBE pedido = new PedidoBE();
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblMensaje.Text = "";
            //lblMensaje.ForeColor = System.Drawing.Color.White;

            idPedido = Convert.ToInt32(Request.QueryString["idpedido"]);

            PedidoBLL pedidoBLL = new PedidoBLL();
            pedido = pedidoBLL.Consulta(idPedido);
            //txtTotal.Text = pedidoBLL.ObtenerTotal(idPedido).ToString();
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                PagoBLL pagoBLL = new PagoBLL();
                PagoBE pago = new PagoBE();
                AutenticacionVista vista = new AutenticacionVista();

              /*  pago.Monto = Convert.ToDecimal(txtTotal.Text);
                pago.TipoTarjeta = ddlTipo.SelectedValue;
                pago.NumeroTarjeta = txtNumero.Text;
                pago.Titular = txtTitular.Text;
                pago.Vencimiento = txtVencimiento.Text;
                pago.CodigoSeguridad = Convert.ToInt32(txtCodigo.Text);
                pago.Pedido = pedido;
                pago.Usuario = vista.UsuarioActual;
                pago.Fecha = System.DateTime.Now;

                if (!pagoBLL.Alta(pago))
                {
                    throw new System.ArgumentException("Ocurrio un error al procesar el pago, por favor intente mas tarde.");
                }
                else
                {
                    lblMensaje.Text = "Pago confirmado con Exito!";                    
                }*/

            }
            catch (Exception ex)
            {
               // lblMensaje.Text = ex.Message;
                //lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Turno.aspx", false);
        }
    }
}