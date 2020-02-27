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
    public partial class CustomizarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                var idProducto = Convert.ToInt32( Request.QueryString["id"]);
                ProductoBLL productoBLL = new ProductoBLL();
                ProductoBE producto = new ProductoBE();
                producto.Id = idProducto;
                producto = productoBLL.Consulta(producto);


            }

            
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Default.aspx", false);
        }
    }
}