<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="ShoeMarket.CrearUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center">
        <h3>
            <asp:Label ID="lblUsuario" Text="Registrarse" runat="server" /></h3>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-8">
                <div id="regitrarUsuario" runat="server">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    <br /> 
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre" />
                    <br />
                    <asp:TextBox ID="txtNombre" runat="server" class="form-control" ></asp:TextBox>
                    <br />
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido" />
                    <br />
                    <asp:TextBox ID="txtApellido" runat="server" class="form-control" ></asp:TextBox>
                    <br />
                    <asp:Label ID="lblUsername" runat="server" Text="Username" />
                    <br />
                    <asp:TextBox ID="txtUsername" runat="server" class="form-control" ></asp:TextBox>
                    <asp:Label ID="lblEmail" runat="server" Text="Email" />
                    <br />
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" ></asp:TextBox>
                    <br />
                    <asp:Label ID="lblDNI" runat="server" Text="DNI" />
                    <br />
                    <asp:TextBox ID="txtDNI" runat="server" class="form-control" ></asp:TextBox>
                    <br />
                    <asp:Label ID="lblClave1" runat="server" Text="Contraseña" />
                    <br />
                    <asp:TextBox ID="txtClave1" runat="server" class="form-control"  TextMode="Password"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblClave2" runat="server" Text="Repetir contraseña" />
                    <br />
                    <asp:TextBox ID="txtClave2" runat="server" class="form-control"  TextMode="Password"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblIdioma" runat="server" Text="Idioma" />
                                      
                    <asp:DropDownList ID="ddlIdiomas"  class="btn btn-primary dropdown-toggle" runat="server"></asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <div >
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar"  class="btn btn-primary" OnClick="btnGuardar_Click" />
                        </div>

                    <div id="registroOk" runat="server">
                        <asp:Label ID="lblregistroOk" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                </div>
                <div class="col-sm-2">
                 
                </div>            
        </div>
    </div>
</asp:Content>
