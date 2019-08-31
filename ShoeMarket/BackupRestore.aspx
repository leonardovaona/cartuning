<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackupRestore.aspx.cs" Inherits="ShoeMarket.BackupRestore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            height: 26px;
            width: 143px;
        }
        .auto-style3 {
            width: 143px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 782px">
        <tr>
            <td>
                <h2>BACKUP Y RESTORE</h2>   
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
                                    <td colspan="3"><h3>Backup:</h3></td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">Nombre del Backup:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtNombreBackup" runat="server" Width="384px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">Ruta destino del Backup (En Servidor):</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtRutaBackup" runat="server" Width="384px"></asp:TextBox>
                                    </td>
                          
                                   </tr> 
                                 <tr align="left"  >
                                    <td>Tamaño de los Fragmentos:</td>
                                    <td valign="bottom"  >
                                    <asp:TextBox ID="txtTamanoBackup" runat="server" Width="124px"></asp:TextBox>
                                    MB
                                    </td>
                            
                                </tr>
                       
                                <tr>
                                    <td colspan="4" align="center" class="auto-style3">                                
                                        <asp:Button ID="btnRealizarBackup" runat="server" Text="Realizar Backup" OnClick="btnRealizarBackup_Click1" />                                
                                    </td>
                                <br />
                                </tr>
                                    <tr><td></td></tr>
                                <tr>                                  
                                    <td colspan="3"><h3>Restore:</h3></td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="auto-style1">Listado de Backup:</td>
                                </tr> 
                                <tr>
                                    <td colspan="4" class="auto-style1">
                                        
                                        <asp:GridView ID="dataGridBackup" runat="server"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id"  />
                                                                                       <asp:BoundField datafield="Nombre"
                                                readonly="true"      
                                                headertext="Nombre"/>
                                              <asp:BoundField datafield="Fecha"
                                                convertemptystringtonull="true"
                                                headertext="Fecha"/>
                                              <asp:BoundField datafield="Path"
                                                convertemptystringtonull="true"
                                                headertext="Ubicacion"/>
                                              <asp:BoundField datafield="Descripcion"
                                                  convertemptystringtonull="true"
                                                  headertext="Descripcion" />
                                               <asp:BoundField datafield="Size"
                                                  convertemptystringtonull="true"
                                                  headertext="Tamaño" />
                                            </Columns>
                                        
                                        </asp:GridView>
                                        
                                    </td>
                                </tr> 
                                      <tr align="left"  >
                                    <td>Defina el Grupo de Backup a Restaurar:</td>
                                    <td valign="bottom"  >
                                         <asp:TextBox ID="txtBackupId" runat="server" Width="124px"></asp:TextBox>
                                    </td>
                            
                                </tr>
                                <tr>
                        
                                    <td colspan="4" valign="middle" align="center" class="auto-style3" >                                
                                        <asp:Button  ID="btnRealizarRestore" runat="server" Text="Realizar Restore" OnClick="btnRealizarRestore_Click1" />                                
                                    </td>
                                </tr>
                        </table>
            </td>
        </tr>
        <tr>
            <td td colspan="3" style="text-align:center">
                <br>
                <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />
            </td>
        </tr>
        <br />
    </table>
</asp:Content>
