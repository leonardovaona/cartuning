<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ShoeMarket.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divGrilla">
    <asp:GridView ID="dataGridUsuario" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dataGridUsuario_SelectedIndexChanged"
                SelectedIndex="1"           
                OnSelectedIndexChanging="dataGridUsuario_SelectedIndexChanging">
        <columns>           
            <asp:boundField datafield="Id"                
                readonly="true"
                headertext="Id" />            
            <asp:boundfield datafield="Nombre"
              readonly="true"      
              headertext="Nombre" />
           <asp:boundfield datafield="Apellido"
              convertemptystringtonull="true"
              HeaderStyle-Width ="50px"
              headertext="Apellido">
              <HeaderStyle Width="50px"></HeaderStyle>
            </asp:boundfield>
           <asp:boundfield datafield="Username"
              convertemptystringtonull="true"
              headertext="Username"/>
           <asp:boundfield datafield="Email"
              convertemptystringtonull="true"
              headertext="Email"/>
            <asp:ButtonField ButtonType="Button" CommandName="Select" Text="<--" />
        </columns>        
        <SelectedRowStyle BackColor="LightCyan"
                    ForeColor="DarkBlue"
                    Font-Bold="true" />
    </asp:GridView>
    <br />
    <asp:label ID="lblMensaje" runat="server" Text=""></asp:label>
    <br />
    <asp:Button runat="server" ID="btnConsultar" Text="Consultar" OnClick="btnConsultar_Click" />
    <asp:Button runat="server" ID="btnBorrar" Text="Borrar" OnClick="btnBorrar_Click" />    
    </div>
    <div id="divDatos" runat="server">
         <asp:Label ID="lblNombre" runat="server" Text="Nombre"/>                                    
         <asp:TextBox ID="txtNombre" runat="server" Width="384px"></asp:TextBox>
         <br />
        <asp:Label ID="lblApellido" runat="server" Text="Apellido"/>                                    
         <asp:TextBox ID="txtApellido" runat="server" Width="384px"></asp:TextBox>
         <br />
        <asp:Label ID="lblUsername" runat="server" Text="Nombre de usuario"/>                                    
         <asp:TextBox ID="txtUsername" runat="server" Width="384px"></asp:TextBox>
         <br />
        <asp:Label ID="lblBloqueado" runat="server" Text="Bloqueado"/>
         <asp:RadioButton ID="rbBloqueadoTrue" runat="server" name="Bloqueado" Text="Si" />
        <asp:RadioButton ID="rbBloqueadoFalse" runat="server" name="Bloqueado" Text="No"/>
         <br />
        <asp:Label ID="lblEliminado" runat="server" Text="Eliminado"/>
         <asp:RadioButton ID="rbEliminadoTrue" runat="server" name="Eliminado" Text="Si" />
        <asp:RadioButton ID="rbEliminadoFalse" runat="server" name="Eliminado" Text="No"/>
    </div>
</asp:Content>
