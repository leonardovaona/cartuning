
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearFamilia.aspx.cs" Inherits="ShoeMarket.CrearFamilia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <table style="width: 782px">
        <tr>
            <td>
                <h2>FAMILIA</h2>
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
                                    <td colspan="3"><h3>Familia:</h3></td>
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
                                    <td class="auto-style1">DNI</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtDNI" runat="server" Width="384px"></asp:TextBox>
                                    </td>                               
                                </tr>    
                            <tr>
                                    <td class="auto-style1">Seleccionar idioma</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtIdioma" runat="server" Width="384px"></asp:TextBox>
                                    </td>                               
                                </tr>                                
                            <tr>
                                    <td colspan="4" valign="middle" align="center" class="auto-style3" >                                
                                        <asp:Button  ID="btnGuardar" runat="server" Text="Guardar" />                                
                                    </td>
                                </tr>
                        </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align:center">
                <br>
                <asp:Button ID="btnVolver" runat="server" Text="Volver"/>
            </td>
        </tr>
        <br />
    </table>

</asp:Content>
