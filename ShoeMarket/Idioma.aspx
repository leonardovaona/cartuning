<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Idioma.aspx.cs" Inherits="ShoeMarket.Idioma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Idioma">
        <asp:Label ID="lblNombre"  runat="server" text="Nombre idioma"/>
        <asp:TextBox ID="txtNombre" runat="server" MaxLength="16"></asp:TextBox>
        <br />
        <asp:Label ID="lblCodigo"  runat="server" text="Codigo idioma"/>
        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="16"></asp:TextBox>
        <br />
        <asp:Label ID="lblDescripcion"  runat="server" text="Descripcion idioma"/>
        <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="16"></asp:TextBox>
        <br />
        <asp:Label ID="lblBotonAdminUsuario" runat="server" text="Administrar Usuario"/>
        <asp:TextBox ID="txtAdminUsuario" runat="server" MaxLength="16"></asp:TextBox>    
        <br />
        <asp:Label ID="lblBotonAdminFamilia" runat="server" text="Administrar Familia"/>
        <asp:TextBox ID="txtAdminFamilia" runat="server" MaxLength="16"></asp:TextBox>    
        <br />
    </div>
</asp:Content>
