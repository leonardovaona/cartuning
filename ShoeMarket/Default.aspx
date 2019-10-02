<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoeMarket.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="home-banner">
        <div class="overlay">
            <div class="banner-text">
                <asp:Label ID="welcomeLabel" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblLema" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <section>
        <h2>Pagina principal</h2>
        <p><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></p>
        <p><asp:Button ID="btnCarrito" runat="server" Text="Ir a Carrito" Width ="250px" BackColor="LightGray" Visible="False" /></p>
        <p><asp:Button ID="btnAdministracionUsuarios" runat="server" Text="Administración de usuarios" Width ="250px" BackColor="LightGray"  Visible="False" OnClick="btnAdministracionUsuarios_Click" />&nbsp;</p>
        <p><asp:Button ID="btnAdministracionFamilias" runat="server" Text="Administración de familias" Width ="250px" BackColor="LightGray"  Visible="False" OnClick="btnAdministracionFamilias_Click" />&nbsp;</p>
        <p><asp:Button ID="btnBitacora" runat="server" Text="Bitacora" Width ="250px" BackColor="LightGray" Visible="False" OnClick="btnBitacora_Click" /></p>
        <!--<p><asp:Button ID="btnBackupYRestore" runat="server" Text="Backup y Restore" BackColor="LightGray" Visible="False"  Width ="250px"/></p>-->
        <p><asp:Button ID="btnIntegridadBD" runat="server" Text="Verificar Integridad de Datos" BackColor="LightGray" Visible="False"  Width ="250px" OnClick="btnIntegridadBD_Click1" /></p>
        <p><asp:Button ID="btnCambioDePrecios" runat="server" Text="Actualización de precios" BackColor="LightGray" Visible="False"  Width ="250px" /></p>
    </section>
</asp:Content>