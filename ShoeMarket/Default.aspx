<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoeMarket.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="home-banner">
        <div class="overlay">
            <div class="jumbotron text-center">
                <h1><asp:Label ID="welcomeLabel" runat="server" Text=""></asp:Label></h1>
                <p><asp:Label ID="lblLema" runat="server" Text=""></asp:Label></p> 
            </div>
        </div>
    </div>
 <div class="container">
  <div class="row">
    <div class="col-sm-4">
        <p><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></p>   
        <p><asp:Button ID="btnAdministracionUsuarios" class="btn btn-primary" runat="server" Text="Administración de usuarios"  Visible="False"  Width ="200px" OnClick="btnAdministracionUsuarios_Click" />&nbsp;</p>
        <p><asp:Button ID="btnAdministracionFamilias"  class="btn btn-primary" runat="server" Text="Administración de familias"  Visible="False"  Width ="200px" OnClick="btnAdministracionFamilias_Click" />&nbsp;</p>
        <p><asp:Button ID="btnBitacora" runat="server" class="btn btn-primary" Text="Bitacora"  Visible="False"  Width ="200px" OnClick="btnBitacora_Click" /></p>
        <!--<p><asp:Button ID="btnBackupYRestore" runat="server" class="btn btn-success" Text="Backup y Restore"  Visible="False" /></p>-->
        <p><asp:Button ID="btnIntegridadBD" class="btn btn-primary" runat="server" Text="Verificar Integridad de Datos"  Width ="200px" Visible="False"  OnClick="btnIntegridadBD_Click1" /></p>
        <p><asp:Button ID="btnIdioma"  class="btn btn-primary" runat="server" Text="Idioma"  Visible="False"   Width ="200px" OnClick="btnIdioma_Click" /></p>
        <p><asp:Button ID="btnProdcuto"  class="btn btn-primary" runat="server" Text="Producto"  Visible="False"  Width ="200px" OnClick="btnProducto_Click" /></p>
      </div>
    <div id="divProductos" runat="server" class="col-sm-4">

    </div>
    <div class="col-sm-4">
    </div>
  </div>
</div>
</asp:Content>