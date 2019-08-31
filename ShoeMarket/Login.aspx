<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoeMarket.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section>
    <asp:Panel ID="divIniciarSesion" runat="server" CssClass="loginForm">
        
        <h2>Iniciar sesión</h2>

        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <div id="divMensaje"></div>

        <div class="formControl">
            <label for="txtUsuario">Usuario</label>
            <asp:TextBox ID="txtUsuario" runat="server" MaxLength="16"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el usuario." ControlToValidate="txtUsuario" EnableClientScript="True" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator> 
        </div>

        <div class="formControl">
            <label for="txtUsuario">Contraseña</label>
            <asp:TextBox ID="txtClave" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe ingresar la contraseña." ControlToValidate="txtClave" EnableClientScript="True" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtClave" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{6,12}$" runat="server" ErrorMessage="La contraseña debe tener entre 6 y 12 caracteres." SetFocusOnError="True" ForeColor="Red"></asp:RegularExpressionValidator>
        </div>          
        <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesion" BackColor="LightGray" OnClick="btnLogin_Click1"  />
            
    </asp:Panel>
            
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
    </section>
</asp:Content>
