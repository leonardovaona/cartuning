<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Idioma.aspx.cs" Inherits="ShoeMarket.Idioma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divjumbo" class="jumbotron text-center">
        <h3>
            <asp:Label ID="lblTituloIdioma" Text="Crear Idioma" runat="server" /></h3>
    </div>
    <div id="conteinter" class="container">
        <div id="divrow" class="row">
            <div id="divcol1" class="col-sm-2">
            </div>
            <div id="divcol2" class="col-sm-8">
                <div id="divIdioma" runat="server">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <br />
                </div>
            </div>
            <div id="divcol3" class="col-sm-2">
                <asp:Button ID="btnVolverPagina" class="btn btn-success" runat="server" Text="Volver" OnClick="btnVolverPagina_Click" />
            </div>
        </div>
    </div>
</asp:Content>
