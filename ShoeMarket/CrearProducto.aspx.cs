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
using System.Reflection;
using System.IO;


namespace ShoeMarket
{
    public partial class Producto : System.Web.UI.Page
    {
        static List<Byte[]> listaImagenes = new List<byte[]>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

            }
            
            CargarTipoProducto();
            CargarMaterial();
            CargarColores();
            txtColor.Enabled = false;
        }

        protected void CargarTipoProducto()
        {
            ProductoBLL productoBLL = new ProductoBLL();
            List<TipoProductoBE> listaTipoProducto = new List<TipoProductoBE>();
            listaTipoProducto = productoBLL.getTipoProducto();
            foreach (var tipoProducto in listaTipoProducto)
            {
                ListItem newItem = new ListItem();
                newItem.Text = tipoProducto.Descripcion;
                newItem.Value = Convert.ToString(tipoProducto.Id);
                ddlTipoProducto.Items.Add(newItem);
            }
        }

        protected void CargarMaterial()
        {
            ProductoBLL productoBLL = new ProductoBLL();
            List<MaterialBE> listaMateriales = new List<MaterialBE>();
            listaMateriales = productoBLL.getMateriales();
            foreach (var material in listaMateriales)
            {
                ListItem newItem = new ListItem();
                newItem.Text = material.Descripcion;
                newItem.Value = Convert.ToString(material.Id);
                ddlMaterial.Items.Add(newItem);
            }
        }

        protected void CargarColores()
        {
            List<Color> allColors = new List<Color>();

            foreach (Color color in new ColorConverter().GetStandardValues())
            {
                ListItem newItem = new ListItem();
                newItem.Text = color.Name;
                newItem.Value = color.Name;
                ddlColor.Items.Add(newItem);
            }
        }

        protected void btnAgregarParte_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionParte.Value == "")
                {
                    throw new System.ArgumentException("Debe ingresar la descripción");
                }

                ProductoBLL productoBLL = new ProductoBLL();
                ParteProductoBE parte = new ParteProductoBE();
                parte.Descripcion = txtDescripcionParte.Value;
                parte.Color = ddlColor.SelectedValue;
                parte.Material = productoBLL.getMaterial(Convert.ToInt32(ddlMaterial.SelectedValue));
                parte.Id = productoBLL.Alta(parte);
                if (parte.Id != 0)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = parte.Descripcion;
                    newItem.Value = Convert.ToString(parte.Id);
                    listBoxPartes.Items.Add(newItem);
                }
                txtDescripcionParte.Value = "";
                lblMensajeParte.Text = "";
                lblMensajeProducto.ForeColor = System.Drawing.Color.White;

            }
            catch (Exception ex)
            {
                lblMensajeParte.Text = ex.Message;
                lblMensajeParte.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensajeProducto.Text = "";
            lblMensajeProducto.ForeColor = System.Drawing.Color.White;
            try
            {
                ProductoBLL productoBLL = new ProductoBLL();
                ProductoBE producto = new ProductoBE();

                if (txtDescripcion.Text == "")
                {
                    throw new System.ArgumentException("Debe ingresar la descripción");
                }
                if (txtPrecio.Value == "")
                {
                    throw new System.ArgumentException("Debe ingresar el precio");
                }
                else
                {
                    if (Convert.ToInt32(txtPrecio.Value) <= 0)
                    {
                        throw new System.ArgumentException("El precio no puede ser cero o negativo.");
                    }
                }

                if (listaImagenes.Count == 0)
                {
                    throw new System.ArgumentException("Debe asociar al menos una imagen al producto.");
                }

                producto.Descripcion = txtDescripcion.Text;
                producto.Precio = Convert.ToDouble(txtPrecio.Value);
                foreach (ListItem item in listBoxPartes.Items)
                {
                    ParteProductoBE parte = new ParteProductoBE();
                    parte.Id = Convert.ToInt32(item.Value);
                    parte.Descripcion = item.Text;
                    producto.Partes.Add(parte);
                }

                if (producto.Partes.Count != 0)
                {
                    AutenticacionVista autenticacionVista = new AutenticacionVista();
                    var usuarioActual = autenticacionVista.UsuarioActual;
                    producto.Usuario = usuarioActual;
                    producto.Tipo = productoBLL.getTipoProducto(Convert.ToInt32(ddlTipoProducto.SelectedValue));
                    producto.Imagenes = listaImagenes;
                    if (productoBLL.Alta(producto))
                    {
                        lblMensajeProducto.Text = "Se genero el producto correctamente";
                        listaImagenes = null;
                        lblColorBack.ForeColor = Color.White;
                    }
                    else
                    {
                        throw new System.ArgumentException("Ocurrio un error al generar el producto.");
                    }
                }
                else
                {
                    throw new System.ArgumentException("Debe agregar al menos una parte");
                }
            }
            catch (Exception ex)
            {
                lblMensajeProducto.Text = ex.Message;
                lblMensajeProducto.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {

            if (ImageFileUpload.HasFile)
            {
                try
                {
                    FileUpload imagenSubida = (FileUpload)ImageFileUpload;
                    Byte[] imagenByte = null;
                    if (imagenSubida.HasFile && imagenSubida.PostedFile != null)
                    {

                        HttpPostedFile File = imagenSubida.PostedFile;
                        imagenByte = new Byte[File.ContentLength];                        
                        File.InputStream.Read(imagenByte, 0, File.ContentLength);
                        listaImagenes.Add(imagenByte);
                        System.Web.UI.WebControls.Image imageUpload = new System.Web.UI.WebControls.Image();
                        imageUpload.ImageUrl = "data:image;base64," + Convert.ToBase64String(imagenByte);
                        imageUpload.Width = 400;
                        divImagenes.Controls.Add(imageUpload);
                        imageUpload.Visible = true;
                        StatusLabel.Text = "Se cargo la imagen con exito";
                    }
                    else
                    {
                        StatusLabel.Text = "Solo archivo del tipo image, bmp o jpeg son permitidos";
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "No se puedo cargar el archivo. Error: " + ex.Message;
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {   
            txtColor.BackColor = Color.FromName(ddlColor.SelectedValue);
        }
    }
}