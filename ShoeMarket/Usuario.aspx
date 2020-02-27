<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ShoeMarket.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center">
        <h3>
            <asp:Label ID="lblTituloUsuario" Text="Administración de usuarios" runat="server" /></h3>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8">
                <div id="divGrilla" class="conteiner">
                    <asp:GridView ID="dataGridUsuario" class="table table-condensed"  runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dataGridUsuario_SelectedIndexChanged"
                        SelectedIndex="1"
                        OnSelectedIndexChanging="dataGridUsuario_SelectedIndexChanging" AllowPaging="True"
                        OnPageIndexChanging="dataGridUsuario_PageIndexChanging">

                        <Columns>
                            <asp:BoundField DataField="Id"
                                ReadOnly="true"
                                HeaderText="Id" />
                            <asp:BoundField DataField="Nombre"
                                ReadOnly="true"
                                HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido"
                                ConvertEmptyStringToNull="true"
                                HeaderStyle-Width="50px"
                                HeaderText="Apellido">
                                <HeaderStyle Width="50px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Username"
                                ConvertEmptyStringToNull="true"
                                HeaderText="Username" />
                            <asp:BoundField DataField="Email"
                                ConvertEmptyStringToNull="true"
                                HeaderText="Email" />
                            <asp:ButtonField ButtonType="Button" CommandName="Select" Text="<--" />
                        </Columns>
                        <SelectedRowStyle BackColor="LightCyan"
                            ForeColor="DarkBlue"
                            Font-Bold="true" />
                        <PagerSettings  FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Button runat="server" ID="btnConsultar" class="btn btn-primary"  Text="Consultar" OnClick="btnConsultar_Click" />
                    <asp:Button runat="server" ID="btnBorrar" class="btn btn-primary"  Text="Borrar" OnClick="btnBorrar_Click" />
                    <asp:Button runat="server" ID="btnAsociarPermiso"  class="btn btn-primary"  Text="Asociar Permisos" OnClick="btnAsociarPermiso_Click" />
                    <br />
                </div>
                <div id="divDatos" class="form-group" runat="server">
                    <asp:Label ID="lblNombre"  runat="server" Text="Nombre" />
                    <asp:TextBox ID="txtNombre"  class="form-control" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblApellido"  runat="server" Text="Apellido" />
                    <asp:TextBox ID="txtApellido" class="form-control" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblUsername" runat="server" Text="Nombre de usuario" />
                    <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblBloqueado" runat="server" Text="Bloqueado" />
                    <asp:RadioButton ID="rbBloqueadoTrue" class="form-control"  runat="server" name="Bloqueado" Text="Si" />
                    <asp:RadioButton ID="rbBloqueadoFalse" class="form-control"  runat="server" name="Bloqueado" Text="No" />
                    <br />
                    <asp:Label ID="lblEliminado" class="list-group" runat="server" Text="Eliminado" />
                    <asp:RadioButton ID="rbEliminadoTrue" class="form-control"  runat="server" name="Eliminado" Text="Si" />
                    <asp:RadioButton ID="rbEliminadoFalse" class="form-control"  runat="server" name="Eliminado" Text="No" />
                </div>
                <div id="divPermisos" runat="server">
                    <asp:ListBox ID="lbPermisos" class="list-group" runat="server" />
                    <asp:Button runat="server" ID="btnAgregar" class="btn btn-primary"  Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button runat="server" ID="btnQuitar" class="btn btn-primary"  Text="Quitar" OnClick="btnQuitar_Click" />
                    <asp:ListBox ID="lbPermisosAsociados" class="list-group"  runat="server" SelectionMode="Multiple" />
                    <asp:Button ID="btnGuardarPermisos" class="btn btn-primary"  runat="server" Text="Guardar Permisos Asociados" OnClick="btnGuardarPermisos_Click" />
                </div>
            </div>
            <div class="col-sm-2">
                <asp:Button ID="btnVolver" class="btn btn-success" runat="server" Text="Volver" OnClick="btnVolver_Click" />
            </div>
        </div>
    </div>
</asp:Content>
