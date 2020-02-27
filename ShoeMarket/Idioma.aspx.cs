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
    public partial class Idioma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IdiomaBLL idiomaBLL = new IdiomaBLL();
            List<IdiomaBE> idiomas = new List<IdiomaBE>();
            IdiomaBE idiomaBase = new IdiomaBE();
            idiomaBase.Id = 2;
            idiomaBase = idiomaBLL.Consulta(idiomaBase);

            Label newLabelCodigo = new Label();
            TextBox newTextBoxCodigo = new TextBox();

            Label newLabelDescripcion = new Label();
            TextBox newTextBoxDescripcion = new TextBox();

            newLabelCodigo.Visible = true;
            newTextBoxCodigo.Visible = true;
            newLabelDescripcion.Visible = true;
            newTextBoxDescripcion.Visible = true;
            newTextBoxDescripcion.CssClass = "form-control";
            newLabelCodigo.Text = "Language code";
            newLabelCodigo.ID = "Language_code";
            newLabelCodigo.Visible = true;
            newTextBoxCodigo.ID = "codigo";
            newTextBoxCodigo.CssClass = "form-control";
            divIdioma.Controls.Add(newLabelCodigo);
            divIdioma.Controls.Add(new LiteralControl("<br />"));
            divIdioma.Controls.Add(newTextBoxCodigo);
            divIdioma.Controls.Add(new LiteralControl("<br />"));

            newLabelDescripcion.Text = "Language Description";
            newLabelDescripcion.ID = "Language_Description";
            newLabelDescripcion.Visible = true;
            newTextBoxDescripcion.ID = "descripcion";
            divIdioma.Controls.Add(newLabelDescripcion);
            divIdioma.Controls.Add(new LiteralControl("<br />"));
            divIdioma.Controls.Add(newTextBoxDescripcion);
            divIdioma.Controls.Add(new LiteralControl("<br />"));

            foreach (DetalleIdiomaBE detalle in idiomaBase.Detalle)
            {
                TextBox newTextBox = new TextBox();
                Label newLabel = new Label();

                newTextBox.ID = detalle.Control;
                //newTextBox.Text = newTextBox.ID;
                newTextBox.Visible = true;
                newTextBox.CssClass = "form-control";
                newLabel.ID = detalle.Control + "_label";
                newLabel.Text = detalle.Palabra;
                newLabel.Visible = true;
                divIdioma.Controls.Add(newLabel);
                divIdioma.Controls.Add(new LiteralControl("<br />"));
                divIdioma.Controls.Add(newTextBox);
                divIdioma.Controls.Add(new LiteralControl("<br />"));
            }
            Button newButton = new Button();

            newButton.ID = "btnSave";
            newButton.Text = "Save";
            newButton.CssClass = "btn btn-primary";
            newButton.Click += new EventHandler(newButton_OnClick);
            divIdioma.Controls.Add(newButton);
        }

        private void newButton_OnClick(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            lblMensaje.ForeColor = System.Drawing.Color.White;

            IdiomaBE newIdioma = new IdiomaBE();

            try
            {
                foreach (var control in divIdioma.Controls)
                {
                    if (control is TextBox)
                    {
                        if (((TextBox)control).Text != "")
                            if (((TextBox)control).ID == "descripcion" || ((TextBox)control).ID == "codigo")
                            {
                                if (((TextBox)control).ID == "codigo")
                                    newIdioma.Codigo = ((TextBox)control).Text;
                                if (((TextBox)control).ID == "descripcion")
                                    newIdioma.Descripcion = ((TextBox)control).Text;
                            }
                            else
                            {
                                DetalleIdiomaBE newDetalle = new DetalleIdiomaBE();
                                newDetalle.Palabra = ((TextBox)control).Text;
                                newDetalle.Control = ((TextBox)control).ID;
                                newIdioma.Detalle.Add(newDetalle);
                            }
                        else
                        {
                            throw new System.ArgumentException("Todos los campos tienen que ser ingresados para guardar el idioma.");
                        }
                    }
                }



                IdiomaBLL idiomaBLL = new IdiomaBLL();
                Label label = new Label();

                if (idiomaBLL.Alta(newIdioma))
                {
                    label.Text = "Se creo el idioma";
                }
                else
                {
                    throw new System.ArgumentException("Error al crear el idioma");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnVolverPagina_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
    }
}