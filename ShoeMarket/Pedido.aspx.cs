using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BLL;
using BE;

namespace ShoeMarket
{
    public partial class Pedido : System.Web.UI.Page
    {
        private int idPedido = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

            }            
            idPedido = Convert.ToInt32(Request.QueryString["id"]);
            CargarProductos(idPedido);
            
        }

        protected void CargarProductos(int idPedido)
        {
            PedidoBLL pedidoBLL = new PedidoBLL();
            PedidoBE pedido = new PedidoBE();
            
            pedido = pedidoBLL.Consulta(idPedido);

            HtmlTableRow row = null;
            HtmlTableCell cell = null;
            int i = 0;
            decimal precioTotal = 0;

            row = new HtmlTableRow();

            cell = new HtmlTableCell();
            Label lblImagen = new Label();
            lblImagen.ID = "imagen";
            lblImagen.Text = "";
            cell.Controls.AddAt(0, lblImagen);

            HtmlTableCell cell1 = new HtmlTableCell();
            Label lblDescripcion = new Label();
            lblDescripcion.ID = "lblDescripcion";
            lblDescripcion.Text = "Descripción";
            cell1.Controls.AddAt(0, lblDescripcion);

            HtmlTableCell cell2 = new HtmlTableCell();
            Label lblCantidad = new Label();
            lblCantidad.ID = "lblCantidad";
            lblCantidad.Text = "Cantidad";
            cell2.Controls.AddAt(0, lblCantidad);

            HtmlTableCell cell3 = new HtmlTableCell();
            Label lblPrecio = new Label();
            lblPrecio.ID = "lblPrecio";
            lblPrecio.Text = "Precio";
            cell3.Controls.AddAt(0, lblPrecio);

            HtmlTableCell cell4 = new HtmlTableCell();
            Label lblSubtotal = new Label();
            lblSubtotal.ID =  "lblSubtotal";
            lblSubtotal.Text = "Subtotal";
            cell4.Controls.AddAt(0, lblSubtotal);
            
            row.Attributes.Add("class", "active");
            
            row.Controls.AddAt(0, cell);
            row.Controls.AddAt(1, cell1);
            row.Controls.AddAt(2, cell2);
            row.Controls.AddAt(3, cell3);
            row.Controls.AddAt(4, cell4);
            tableContent.Controls.AddAt(i, row);
            i++;
            foreach (var detalle in pedido.Detalles)
            {
                row = new HtmlTableRow();

                cell = new HtmlTableCell();                
                cell.Width = "15";
                System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                image.ImageUrl = "data:image;base64," + Convert.ToBase64String(detalle.Producto.Imagenes[0]);
                image.Width = 150;
                cell.Controls.AddAt(0, image);

                cell1 = new HtmlTableCell();
                lblDescripcion = new Label();
                lblDescripcion.ID = detalle.Id + "lblDescripcion";
                lblDescripcion.Text = detalle.Producto.Descripcion;
                cell1.Controls.AddAt(0, lblDescripcion);

                cell2 = new HtmlTableCell();
                lblCantidad = new Label();
                lblCantidad.ID = detalle.Id + "lblCantidad";
                lblCantidad.Text = detalle.Cantidad.ToString();
                cell2.Controls.AddAt(0, lblCantidad);
                
                cell3 = new HtmlTableCell();
                lblPrecio = new Label();
                lblPrecio.ID = detalle.Id + "lblPrecio";
                lblPrecio.Text = detalle.Producto.Precio.ToString();
                cell3.Controls.AddAt(0, lblPrecio);

                cell4 = new HtmlTableCell();
                lblSubtotal = new Label();
                lblSubtotal.ID = detalle.Id + "lblSubtotal";
                lblSubtotal.Text = (detalle.Producto.Precio * detalle.Cantidad).ToString();
                cell4.Controls.AddAt(0, lblSubtotal);

                HtmlTableCell cell5 = new HtmlTableCell();
                Button butonQuitar = new Button();
                butonQuitar.ID = Convert.ToString(detalle.Id);
                butonQuitar.Text = "Quitar";

                butonQuitar.CssClass = "btn btn-primary";
                butonQuitar.Click += new EventHandler (newButton_OnClick);                
                cell5.Controls.AddAt(0, butonQuitar);


                row.Attributes.Add("class", "warning");

                precioTotal = precioTotal + (Convert.ToDecimal(detalle.Cantidad) * Convert.ToDecimal(detalle.Producto.Precio));

                row.Controls.AddAt(0, cell);
                row.Controls.AddAt(1, cell1);
                row.Controls.AddAt(2, cell2);
                row.Controls.AddAt(3, cell3);
                row.Controls.AddAt(4, cell4);
                row.Controls.AddAt(5, cell5);
                tableContent.Controls.AddAt(i, row);
                i++;
            }
            Label lblTotal = new Label();
            lblTotal.ID = "lblTotal";
            lblTotal.Text = "Total: "+ precioTotal;
            
            divPedido.Controls.Add(lblTotal);           

        }

        private void newButton_OnClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int buttonId = Convert.ToInt32(button.ID);
            PedidoBLL pedidoBLL = new PedidoBLL();
            pedidoBLL.BajaDetalle(buttonId, idPedido);
            Response.Redirect("~/Pedido.aspx?id=" + idPedido, false);
        }
            protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Turno.aspx?id=" + idPedido, false);
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
    }
}