<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearProducto.aspx.cs" Inherits="ShoeMarket.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center">
        <h3>
            <asp:label id="lblTituloProducto" text="Producto" runat="server" />
        </h3>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8">
                <asp:label id="lblMensajeProducto" runat="server" />
                <form class="form-inline">
                    <div class="form-group">
                        <asp:label id="lblDescripcion" text="Descripcion" runat="server" />
                        <asp:textbox id="txtDescripcion" runat="server" class="form-control" />

                        <asp:label id="lblPrecio" text="Precio" runat="server" />
                        <input type="input" class="form-control" id="txtPrecio" text="Guardar" runat="server">
                        <br />

                        <div id="divImagenes" runat="server">
                            
                        </div>
                        <div class="row" style="margin-left: 30px; !important">
                            <asp:image id="ImageUp" runat="server" style="width: 200px" />
                            <br />
                            <br />
                            <asp:fileupload id="ImageFileUpload" runat="server" />
                            <asp:button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" />
                            <br />
                            <br />
                        </div>
                        <br />
                        <asp:label runat="server" id="StatusLabel" text="Upload status: " />
                        <br />
                        <asp:listbox id="listBoxPartes" runat="server" />
                    </div>

                    <asp:dropdownlist id="ddlTipoProducto" class="btn btn-primary dropdown-toggle" runat="server">
                    </asp:dropdownlist>
                    <br />
                    <br />
                    <br />
                    <asp:label id="lblMensajeParte" runat="server" />
                    <div id="divParteProducto" class="form-group">
                        <asp:label id="lblDescripcionParte" text="Descripcion Parte" runat="server" />
                        <input type="input" class="form-control" id="txtDescripcionParte" runat="server">
                        <asp:dropdownlist id="ddlMaterial" class="btn btn-primary dropdown-toggle" runat="server">
                        </asp:dropdownlist>
                        <asp:dropdownlist id="ddlColor" class="btn btn-primary dropdown-toggle" runat="server" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged" AutoPostBack="true">
                        </asp:dropdownlist>
                        <asp:Label id="lblColorBack" runat="server"  Width="30"/>
                        <asp:TextBox ID="txtColor" runat="server" Width="30" />
                         <asp:button id="btnAgregarParte" text="Agregar" runat="server" onclick="btnAgregarParte_Click" />
                    </div>

                    <asp:button id="btnGuardar" text="Guardar" runat="server" class="btn btn-default" onclick="btnGuardar_Click" />
                </form>
            </div>
            <div class="col-sm-2">
                <asp:button id="btnVolver" class="btn btn-success" runat="server" text="Volver" onclick="btnVolver_Click" />
            </div>
        </div>
    </div>
</asp:Content>
