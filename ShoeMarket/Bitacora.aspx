<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="ShoeMarket.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center">
        <h3>
            <asp:Label ID="lblTituloBitacora" Text="Bitacora" runat="server" /></h3>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-8">

                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewBitacora" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <div class="table-responsive">
                                                <asp:GridView class="table" ID="grillaBitacora" runat="server"
                                                    AutoGenerateColumns="false"
                                                    AllowPaging="true"
                                                    OnPageIndexChanging="grillaBitacora_PageIndexChanging">
                                                   
                                                    <Columns>
                                                        <asp:BoundField DataField="Fecha"
                                                            ReadOnly="true"
                                                            HeaderText="Fecha" />
                                                        <asp:BoundField DataField="Username"
                                                            ConvertEmptyStringToNull="true"
                                                            HeaderStyle-Width="50px"
                                                            HeaderText="Username" >
                                                        <HeaderStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Descripcion"
                                                            ConvertEmptyStringToNull="true"
                                                            HeaderText="Descripcion" />
                                                        <asp:BoundField DataField="Criticidad"
                                                            ConvertEmptyStringToNull="true"
                                                            HeaderText="Criticidad" />
                                                    </Columns>
                                                    <PagerSettings  FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                               
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />
                    </td>
                </tr>
            </div>
        <div class="col-sm-2">
        </div>
    </div>
    </div>
</asp:Content>
