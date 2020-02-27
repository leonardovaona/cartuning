using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace ShoeMarket
{
    public partial class Producto1 : System.Web.UI.Page
    {
        int idProducto;
        static ProductoBE productoGlobal = new ProductoBE();
        static List<Byte[]> listaImagenes = new List<byte[]>();
        static int idxSiguiente = 0;
        static int idxAnterior = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtMaterial.Enabled = false;
            txtColor.Enabled = false;
            txtColorMuestra.Enabled = false;

            idProducto = Convert.ToInt32(Request.QueryString["id"]);

            if (idProducto != 0)
            {
                divListado.Visible = false;
                divProducto.Visible = true;
                CargarProducto(idProducto);
            }
            else
            {
                divListado.Visible = true;
                divProducto.Visible = false;
                CargarProductos();
            }


            if (!IsPostBack)
            {
               
            }
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
                hyperLink.NavigateUrl = "CustomizarProducto.aspx?id=" + producto.Id;
                hyperLink.ID = Convert.ToString(producto.Id);
                hyperLink.ImageHeight = 200;
                hyperLink.ImageWidth = 200;
                hyperLink.ToolTip = producto.Descripcion;
                hyperLink.Visible = true;

                divListado.Controls.Add(hyperLink);
                divListado.Controls.Add(new LiteralControl("<br />"));
                divListado.Controls.Add(new LiteralControl("<br />"));

            }
        }

        protected void CargarProducto(int id)
        {
            ProductoBLL productoBLL = new ProductoBLL();

            productoGlobal.Id = id;
            productoGlobal = productoBLL.Consulta(productoGlobal);
            listaImagenes = productoGlobal.Imagenes;
            lblDescripcion.Text = productoGlobal.Descripcion;
            lblPrecio.Text = "$" + productoGlobal.Precio.ToString();
            lblTipoProducto.Text = "Categoria: " + productoGlobal.Tipo.Descripcion;            

            for (int counter= 0; counter < productoGlobal.Imagenes.Count; counter++ )
            {           
                System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                image.ImageUrl = "data:image;base64," + Convert.ToBase64String(productoGlobal.Imagenes[counter]);

                HtmlGenericControl divImages = new HtmlGenericControl("div");
                divImages.ID = "div" + counter;
                if (counter == 0)
                {
                    divImages.Attributes.Add("class", "item active");
                }
                else
                {
                    divImages.Attributes.Add("class", "item");
                }                
                divImages.Controls.Add(image);
                divWrapper.Controls.Add(divImages);
            }

            if (ddlPartes.Items.Count == 0 )
            {

                foreach (var parte in productoGlobal.Partes)
                {
                    ListItem item = new ListItem();
                    item.Value = parte.Id.ToString();
                    item.Text = parte.Descripcion;
                    ddlPartes.Items.Add(item);
                    CambiarDatosParte(parte);
                    txtMaterial.Text = parte.Material.Descripcion;
                    txtColor.Text = parte.Color;
                    txtColorMuestra.BackColor = Color.FromName(parte.Color);
                }
            }
        }

        protected void CambiarDatosParte(ParteProductoBE parte)
        {
            txtMaterial.Text = parte.Material.Descripcion;
            txtColor.Text = parte.Color;
            txtColorMuestra.BackColor = Color.FromName(parte.Color);
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int bandera = 0;
            PedidoBLL pedidoBLL = new PedidoBLL();
            AutenticacionVista vista = new AutenticacionVista();
            PedidoBE pedido = new PedidoBE();
            int idPedido = pedidoBLL.getPedidoPorUsuario(vista.UsuarioActual.Id, "PENDIENTE").Id;
            pedido = (PedidoBE)HttpContext.Current.Session["Pedido" + idPedido];

            if (pedido == null)
            {
                bandera = 1;
                pedido.Fecha = DateTime.Now;
                pedido.Estado = "PENDIENTE";
                pedido.Usuario = vista.UsuarioActual;
            }
            PedidoBE pedidoAlta = new PedidoBE();
            DetallePedidoBE detalle = new DetallePedidoBE();
            detalle.Cantidad = Convert.ToInt32(ddlCantidad.SelectedItem.Value);
            detalle.Subtotal = 0;
            detalle.Producto = productoGlobal;
            //detalle.Producto.Id = productoGlobal.Id;
            pedido.Detalles.Add(detalle);
            pedidoAlta.Id = pedido.Id;
            pedidoAlta.Detalles.Add(detalle);
            if (bandera == 1)
            {
                if (!pedidoBLL.Alta(pedido))
                {                    
                    this.Response.Redirect("~/Pedido.aspx?id=" + pedido.Id, false);
                }
                else
                {
                    lblMensajeError.Text = "Ocurrio un error al agregar el producto a su pedido.";
                }
            }
            else
            {
                pedidoBLL.AltaDetalle(pedidoAlta);
                this.Response.Redirect("~/Pedido.aspx?id=" + pedido.Id, false);
            }
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {

        }

        protected void ddlPartes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductoBLL productoBLL = new ProductoBLL();
            ParteProductoBE parte = new ParteProductoBE();
            parte = productoBLL.getParte(Convert.ToInt32(ddlPartes.SelectedItem.Value));
            CambiarDatosParte(parte);
        }
    }
}