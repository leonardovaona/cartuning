<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomizarProducto.aspx.cs" Inherits="ShoeMarket.CustomizarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center">
        <h3>
            <asp:Label ID="lblTituloCustomizarProducto" Text="Bitacora" runat="server" /></h3>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-8">

                <div class="row">
                    <div class="col-sm-5">
                        <asp:Label ID="lblDescripcion" Text="Descripcion" runat="server" />
                        <br />
                        <asp:Label ID="lblPrecio" Text="Precio" runat="server" />
                        <br />
                        <asp:DropDownList ID="ddlTipoProducto" class="btn btn-primary dropdown-toggle" runat="server">
                        </asp:DropDownList>                        
                        <br />                        
                        <div id="divParteProducto" class="form-group">
                            <asp:Label ID="lblDescripcionParte" Text="Descripcion Parte" runat="server" />
                            <input type="input" class="form-control" id="txtDescripcionParte" runat="server">
                            <asp:DropDownList ID="ddlMaterial" class="btn btn-primary dropdown-toggle" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlColor" class="btn btn-primary dropdown-toggle" runat="server">
                            </asp:DropDownList>                            
                        </div>
                    </div>
                    <div class="col-sm-7">
                        <div id="divImagenes" runat="server">
                          
                        </div>
                        <asp:ListBox ID="listBoxPartes" runat="server" />
                    </div>

                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" class="btn btn-default" OnClick="btnGuardar_Click" />
                </div>

            </div>
            <div class="col-sm-2">
                <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />
            </div>
        </div>
    </div>
</asp:Content>
