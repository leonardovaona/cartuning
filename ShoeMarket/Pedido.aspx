<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="ShoeMarket.Pedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="breadcrumb" align="center">
        <h1>
            <asp:Label ID="lblTituloPedido" Text="Pedido" runat="server" /></h1>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div id="divPedido" runat="server" class="col-sm-7">
                <table id="tableContent" class="table" runat="server">
                </table>
                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" OnClick="btnSiguiente_Click" />
            </div>
            <div class="col-sm-3">
                <asp:Button ID="btnVolver" runat="server" class="btn btn-success" Text="Volver" OnClick="btnVolver_Click" />
            </div>
        </div>
    </div>
</asp:Content>
