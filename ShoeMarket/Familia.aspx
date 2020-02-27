<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Familia.aspx.cs" Inherits="ShoeMarket.CrearFamilia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron text-center">
            <h3><asp:Label ID="lblTituloFamilia" Text="Administración de familia" runat="server" /></h3>
     </div>
    <div class="container">
        <div class="row">   
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8">
                <div id="divGrilla" class="conteiner" runat="server">
                    <asp:GridView ID="dataGridFamilia" class="table table-condensed" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dataGridFamilia_SelectedIndexChanged"
                        SelectedIndex="1"
                        OnSelectedIndexChanging="dataGridFamilia_SelectedIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Id"
                                ReadOnly="true"
                                HeaderText="Id" />
                            <asp:BoundField DataField="Nombre"
                                ReadOnly="true"
                                HeaderText="Nombre" />
                            <asp:BoundField DataField="Descripcion"
                                ReadOnly="true"
                                HeaderText="Descripcion" />
                            <asp:ButtonField ButtonType="Button" CommandName="Select" Text="<--" />
                        </Columns>
                        <SelectedRowStyle BackColor="LightCyan"
                            ForeColor="DarkBlue"
                            Font-Bold="true" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Button runat="server" ID="btnCrear" class="btn btn-primary" Text="Crear" OnClick="btnCrear_Click" />
                    <!--<asp:Button runat="server" ID="btnBorrar" class="btn btn-primary" Text="Borrar" OnClick="btnBorrar_Click" />    -->
                    <asp:Button runat="server" ID="btnConsultar" class="btn btn-primary" Text="Consultar" OnClick="btnConsultar_Click" />
                    <asp:Button runat="server" ID="btnAsociarPermiso" class="btn btn-primary" Text="Asociar Permisos" OnClick="btnAsociarPermiso_Click" />
                </div>
                <asp:Label ID="lblMensaje" runat="server" Text="" />
                <div id="divDatos" class="container" runat="server">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre" />
                    <asp:TextBox ID="txtNombre" class="form-control" runat="server" Width="384px"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripión" />
                    <asp:TextBox ID="txtDescripcion" class="form-control" runat="server" Width="384px"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnGuardar" runat="server" class="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
                </div>
                <div id="divAsociarPermiso" class="container" runat="server">
                    <asp:ListBox ID="lbPermisos" runat="server" class="list-group" SelectionMode="Multiple" />
                    <asp:Button ID="btnAgregar" runat="server" class="btn btn-primary" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnQuitar" runat="server" class="btn btn-primary" Text="Quitar" OnClick="btnQuitar_Click" />
                    <asp:ListBox ID="lblPermisosAsignados" class="list-group" runat="server" SelectionMode="Multiple" />
                    <asp:Button ID="btnGuardarAsociaciados" class="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardarAsociados_Click" />
                </div>
                <br />
                <br />
            </div>
            <div class="col-sm-2">
                <asp:Button ID="btnVolver" class="btn btn-success" runat="server" Text="Volver" OnClick="btnVolver_Click"/>
            </div>
        </div>
     </div>
</asp:Content>
