<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="ShoeMarket.CrearUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="regitrarUsuario" runat="server">>
    <table style="width: 782px">
        <tr>
            <td>
                <h2>REGISTRARSE</h2>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                        <table>
                                <tr>
                                    <td colspan="3"><h3>Usuario:</h3></td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">Nombre:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtNombre" runat="server" Width="384px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">Apellido:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtApellido" runat="server" Width="384px"></asp:TextBox>
                                    </td>                               
                                </tr>
                                <tr>
                                    <td class="auto-style1">Nombre de Usuario:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtUsername" runat="server" Width="384px"></asp:TextBox>
                                    </td>                               
                                </tr>
                            <tr>
                                    <td class="auto-style1">Email</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtEmail" runat="server" Width="384px"></asp:TextBox>
                                    </td>                               
                                </tr>    
                            <tr>
                                    <td class="auto-style1">DNI</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtDNI" runat="server" Width="384px"></asp:TextBox>
                                    </td>                               
                                </tr>    
                            <tr>
                                    <td class="auto-style1">Contraseña</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtClave1" runat="server" Width="384px" TextMode="Password"></asp:TextBox>
                                    </td>                               
                                </tr>    
                            <tr>
                                    <td class="auto-style1">Repetir contraseña</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtClave2" runat="server" Width="384px" TextMode="Password"></asp:TextBox>
                                    </td>                               
                                </tr>    
                            <tr>
                                    <td class="auto-style1">Seleccionar idioma</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtIdioma" runat="server" Width="384px"></asp:TextBox>
                                    </td>                               
                                </tr>                                
                            <tr>
                                    <td class="auto-style1">Seleccionar idioma</td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="ddlIdiomas" runat="server"></asp:DropDownList>
                                    </td>                               
                                </tr>                                
                            <tr>
                                    <td colspan="4" valign="middle" align="center" class="auto-style3" >                                
                                        <asp:Button  ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />                                
                                    </td>
                                </tr>
                        </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align:center">
                <br>
                <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click"/>
            </td>
        </tr>
        <br />
    </table>
        </div>
    <div id="registroOk" runat="server">
        <asp:Label ID="lblregistroOk" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
