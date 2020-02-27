<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackupRestore.aspx.cs" Inherits="ShoeMarket.BackupRestore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center">
        <h3>
            <asp:Label ID="lblTituloBackupRestore" Text="Backup / Restore" runat="server" /></h3>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-3">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>

                <asp:Label ID="lblBackupTitle" runat="server" />
                <br />
                <asp:Label ID="lblBackupName" runat="server" />
                <br />
                <asp:TextBox ID="txtNombreBackup" class="form-control"  runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="lblBackupRuta" runat="server" />
                <br />
                <asp:TextBox ID="txtRutaBackup" class="form-control"  runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="lblFragmentos" runat="server" />
                <br />
                <asp:TextBox ID="txtTamanoBackup" class="form-control"  runat="server" ></asp:TextBox>
                MB
                <br />
                <asp:Button ID="btnRealizarBackup" runat="server" class="btn btn-primary" Text="Realizar Backup" OnClick="btnRealizarBackup_Click1" />

            </div>
            <div class="col-sm-7">
                <asp:Label ID="lblRestoreTitle" runat="server" />

                <asp:Label ID="lblBackupList" runat="server" />
                <br />

                <asp:GridView ID="dataGridBackup" runat="server" class="table table-condensed"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre"
                            ReadOnly="true"
                            HeaderText="Nombre" />
                        <asp:BoundField DataField="Fecha"
                            ConvertEmptyStringToNull="true"
                            HeaderText="Fecha" />
                        <asp:BoundField DataField="Path"
                            ConvertEmptyStringToNull="true"
                            HeaderText="Ubicacion" />
                        <asp:BoundField DataField="Descripcion"
                            ConvertEmptyStringToNull="true"
                            HeaderText="Descripcion" />
                        <asp:BoundField DataField="Size"
                            ConvertEmptyStringToNull="true"
                            HeaderText="Tamaño" />
                    </Columns>

                </asp:GridView>
                <br />
                <asp:Label ID="lblGrupoBackup" runat="server" />
                <br />

                <asp:TextBox ID="txtBackupId" class="form-control"  runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnRealizarRestore" runat="server" class="btn btn-primary" Text="Realizar Restore" OnClick="btnRealizarRestore_Click1" />

            </div>
            <div class="col-sm-1">
                <asp:Button ID="btnVolver" class="btn btn-success" runat="server" Text="Volver" OnClick="btnVolver_Click" />
            </div>
        </div>
    </div>
</asp:Content>
