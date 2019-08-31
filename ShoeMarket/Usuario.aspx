<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ShoeMarket.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="dataGridUsuario" runat="server" AutoGenerateColumns="false">
        <columns>
           <asp:boundfield datafield="Nombre"
              readonly="true"      
              headertext="Nombre" />
           <asp:boundfield datafield="Apellido"
              convertemptystringtonull="true"
              HeaderStyle-Width ="50px"
              headertext="Apellido"/>
           <asp:boundfield datafield="Username"
              convertemptystringtonull="true"
              headertext="Username"/>
           <asp:boundfield datafield="Email"
              convertemptystringtonull="true"
              headertext="Email"/>
        </columns>
    </asp:GridView>
</asp:Content>
