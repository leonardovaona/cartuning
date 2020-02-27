<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IntegridadBD.aspx.cs" Inherits="ShoeMarket.IntegridadBD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron text-center">
        <h3>
            <asp:Label ID="lblIntegridad" runat="server" /></h3>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-5">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button ID="btnVerificarIntegridad" runat="server" Text="Ejecutar" class="btn btn-primary" OnClick="btnVerificarIntegridad_Click" />
                <asp:Label ID="lblCalcularIntegridad" runat="server" />
                <br />
                <br />
                <asp:Button ID="btnRecalcularDigitosVerificadores" runat="server" class="btn btn-primary" Text="Ejecutar" OnClick="btnRecalcularDigitosVerificadores_Click" />
                <asp:Label ID="lblRecalcularDigitos" runat="server" />
                <br />
                <br />                
                <asp:Button ID="btnRestaurar" runat="server" class="btn btn-primary" Text="Acceder" OnClick="btnRestaurar_Click" />
                <asp:Label ID="lblRestaurar" runat="server" />
            </div>
            <div class="col-sm-3">
            <asp:Button ID="btnVolver" class="btn btn-success" runat="server" Text="Volver" OnClick="btnVolver_Click"/>
        </div>
        </div>        
    </div>    
</asp:Content>
