<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoeMarket.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
  <div class="row">
    <div class="col-sm-4">
    </div>
    <div ID="divIniciarSesion" runat="server" class="col-sm-4">   
         <br />
        <br />
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <div id="divMensaje"></div>

        <div "form-group">
            <label for="txtUsuario" >Usuario</label>
            <asp:TextBox ID="txtUsuario" runat="server" class="form-control" MaxLength="16"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el usuario." ControlToValidate="txtUsuario" EnableClientScript="True" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator> 
        </div>

        <div "form-group">
            <label for="txtUsuario">Contraseña</label>
            <asp:TextBox ID="txtClave" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar la contraseña." ControlToValidate="txtClave" EnableClientScript="True" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtClave" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{6,12}$" runat="server" ErrorMessage="La contraseña debe tener entre 6 y 12 caracteres." SetFocusOnError="True" ForeColor="Red"></asp:RegularExpressionValidator>
        </div>          
        <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesion" class="btn btn-primary" OnClick="btnLogin_Click1"  />
    </div>
    <div class="col-sm-4">
    </div>
  </div>
</div>
    <asp:Panel ID="divCerrarSesion" runat="server" CssClass="loguedUserBox">
        <div>
        <!--    <label>Usuario actual: <asp:Label ID="lblUsuarioActual" runat="server" Text=""></asp:Label></label>-->
        </div>
        <!--<div><asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesion" BackColor="LightGray" OnClick="btnCerrarSesion_Click"  /></div>-->
    </asp:Panel>

     <script  type="text/javascript">        
         window.onload = function cambiarLabel() {
             if (algunaCondicionCopada) {
                 document.getElementById('divMensaje').innerHTML = '<p>hi</p>';
             }
         };
     </script>
</asp:Content>
