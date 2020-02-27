<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="ShoeMarket.Producto1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function changeElement (selectElementId, valueToSelect) {
            var element = document.getElementById(selectElementId);
            element.value = valueToSelect;
        }
    </script>

    <div class="container">
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8">
                <asp:Label ID="lblMensajeError" runat="server" />
                <div id="divListado" runat="server">
                </div>
                <div id="divProducto" runat="server">
                    <div class="row">
                        <div class="col-lg-4">
                            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                                <!-- Indicators -->
                                <ol class="carousel-indicators">
                                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                    <li data-target="#myCarousel" data-slide-to="1"></li>
                                    <li data-target="#myCarousel" data-slide-to="2"></li>
                                </ol>

                                <!-- Wrapper for slides -->
                                <div id= "divWrapper" runat="server" class="carousel-inner">
                
                                </div>
                                <!-- Left and right controls -->
                                <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                    <span class="sr-only">Next</span>
                                </a>
                            </div>                           
                        </div>
                        <div class="col-lg-4">
                            <p>
                                <asp:Label ID="lblDescripcion" runat="server" />
                            </p>
                            <br />
                            <h1>
                                <asp:Label ID="lblPrecio" runat="server" /></h1>
                            <br />
                            <kbd>
                                <asp:Label ID="lblTipoProducto" runat="server" /></kbd>
                            <br />
                            <br />
                            <asp:Label ID="lblCantidad" runat="server" Text="Cantidad: " />
                            <asp:DropDownList ID="ddlCantidad" runat="server">
                                <asp:ListItem Value="1" />
                                <asp:ListItem Value="2" />
                                <asp:ListItem Value="3" />
                                <asp:ListItem Value="4" />
                                <asp:ListItem Value="5" />
                                <asp:ListItem Value="6" />
                                <asp:ListItem Value="7" />
                                <asp:ListItem Value="8" />
                                <asp:ListItem Value="9" />
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Button ID="btnComprar" runat="server" Text="Comprar" class="btn btn-primary" OnClick="btnComprar_Click" />
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar al pedido" class="btn btn-primary" OnClick="btnAgregar_Click" />
                        </div>
                        <div class="col-lg-4">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:DropDownList ID="ddlPartes" class="dropdown-toggle" runat="server" OnSelectedIndexChanged="ddlPartes_SelectedIndexChanged" AutoPostBack="true" />
                            <br />
                            <br />
                            <asp:TextBox ID="txtMaterial" runat="server" />
                            <br />
                            <br />
                            <asp:TextBox ID="txtColor" runat="server" />
                            <asp:TextBox ID="txtColorMuestra" runat="server" Width="20" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-2">
            </div>
        </div>
    </div>

</asp:Content>
