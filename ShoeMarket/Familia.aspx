
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Familia.aspx.cs" Inherits="ShoeMarket.CrearFamilia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div id="divGrilla" runat="server">
    <asp:GridView ID="dataGridFamilia" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dataGridFamilia_SelectedIndexChanged"
                SelectedIndex="1"           
                OnSelectedIndexChanging="dataGridFamilia_SelectedIndexChanging">
        <columns>           
            <asp:boundField datafield="Id"                
                readonly="true"
                headertext="Id" />            
            <asp:boundfield datafield="Nombre"
              readonly="true"      
              headertext="Nombre" />           
            <asp:boundfield datafield="Descripcion"
              readonly="true"      
              headertext="Descripcion" />           
            <asp:ButtonField ButtonType="Button" CommandName="Select" Text="<--" />
        </columns>        
        <SelectedRowStyle BackColor="LightCyan"
                    ForeColor="DarkBlue"
                    Font-Bold="true" />
    </asp:GridView>
    <br />
    <asp:label ID="Label1" runat="server" Text=""></asp:label>
    <br />
    <asp:Button runat="server" ID="btnCrear" Text="Crear" OnClick="btnCrear_Click" />    
    <asp:Button runat="server" ID="btnBorrar" Text="Borrar" OnClick="btnBorrar_Click" />    
    <asp:Button runat="server" ID="btnConsultar" Text="Consultar" OnClick="btnConsultar_Click" />         
    <asp:Button runat="server" ID="btnAsociarPermiso" Text="Asociar Permisos" OnClick="btnAsociarPermiso_Click" />
    </div>
   <asp:Label ID="lblMensaje" runat="server" Text="" />
    <div id="divDatos" runat="server">
         <asp:Label ID="lblNombre" runat="server" Text="Nombre"/>                                    
         <asp:TextBox ID="txtNombre" runat="server" Width="384px"></asp:TextBox>
         <br />
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripión"/>
        <asp:TextBox ID="txtDescripcion" runat="server" Width="384px"></asp:TextBox>
         <br />
        <asp:Button ID="btnGuardar" runat="server" Text ="Guardar" OnClick="btnGuardar_Click" />
    </div>
    <div id="divAsociarPermiso" runat="server">
        <asp:ListBox ID="lbPermisos" runat="server" SelectionMode="Multiple"/>
        <asp:Button ID="btnAgregar" runat="server" Text ="Agregar" OnClick="btnAgregar_Click" />
        <asp:Button ID="btnQuitar" runat="server" Text ="Quitar" OnClick="btnQuitar_Click" />
        <asp:ListBox ID="lblPermisosAsignados" runat="server"  SelectionMode="Multiple" />
        <asp:Button ID ="btnGuardarAsociaciados" runat="server" Text="Guardar" OnClick="btnGuardarAsociados_Click" />
    </div>
</asp:Content>
