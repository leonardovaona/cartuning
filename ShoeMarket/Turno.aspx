<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Turno.aspx.cs" Inherits="ShoeMarket.Turno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="breadcrumb" align="center">
        <h1>
            <asp:Label ID="lblTituloTurno" Text="Turno" runat="server" /></h1>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-7">
                <div id="divLeyenda" runat="server">
                    <b style="color: blue;">Seleccione la fecha y hora del turno que desea</b>
                    <br />
                    <br />

                    <div class="row">
                        <div class="col-sm-6">
                            <asp:Label ID="lblMensaje" runat="server" />
                            <label for="calendarioTurno">Seleccione el día: </label>
                            <asp:Calendar ID="calendarTurno" runat="server" OnSelectionChanged="calendarTurno_SelectionChanged"></asp:Calendar>
                            <br />

                            <label for="selectHora">Seleccione el horario: </label>

                            <select ID="selectHora" runat="server" class="btn btn-primary dropdown-toggle" onchange="setLabelHora();">
                                <option value="9:00">9:00</option>
                                <option value="10:00">10:00</option>
                                <option value="11:00">11:00</option>
                                <option value="12:00">12:00</option>
                                <option value="13:00">13:00</option>
                                <option value="14:00">14:00</option>
                                <option value="15:00">15:00</option>
                                <option value="16:00">16:00</option>
                                <option value="17:00">17:00</option>
                                <option value="18:00">18:00</option>
                            </select>
                        </div>
                    </div>

                    <div class="col-sm-6">
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <br />

                        </div>
                    </div>
                    <br />

                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" class="btn btn-primary" OnClick="btnConfirmar_Click" />

                </div>
                <div id="divConfirmarTurno" runat="server">
                  <b style="color: blue;">  Se ha reservado el turno para el día: 
                            <asp:Label ID="lblFecha" runat="server" /></b>
                    <input type="text" ID="txtHora" runat="server" width="300"  /> 
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnPago" runat="server" Text="Pagar" class="btn btn-primary" OnClick="btnPago_Click" />
                </div>
            </div>

            <div class="col-sm-3">
                <asp:Button ID="btnVolver" runat="server" class="btn btn-success" Text="Volver" OnClick="btnVolver_Click" />
            </div>
        </div>
    </div>
    <script type="text/javascript"> 
        function setLabelHora() {
            var selectElement = document.getElementById("MainContent_selectHora");

            var labelElement = document.getElementById("MainContent_txtHora");
            var palabra = "a las ";
            labelElement.value = palabra;

        }
    </script>
</asp:Content>
