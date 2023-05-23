﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrandProductSales.aspx.cs" Inherits="ArtTrash.Pages.BrandProductSales" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        //Do not call this method on pageload
        google.setOnLoadCallback(drawChart);
        function drawChart() {

            var data = new google.visualization.arrayToDataTable(<%=this.obtenerDatos()%>);


            var options = {
                "title": "Resumen de Ventas:",
                "fontSize": 12,
                "pieSliceTextStyle": { "color": "#FFF" },
                "sliceVisibilityThreshold": false,
                "legend": {
                    "position": "right",
                    "textStyle": {
                        "color": "#000000",
                        "fontSize": 14
                    }
                },
                "tooltip": {
                    "textStyle": { "color": "#000000" },
                    "showColorCode": false
                },
                "colors": ["#1f386b", "#DC3912", "#FF9900", "#109618", "#990099"],
            };


            var chart = new google.visualization.ColumnChart(document.getElementById('columnchart_material'));
            //var formatter = new google.visualization.NumberFormat({ pattern: '##,##%' });
            //formatter.format(data, 1);
            chart.draw(data, options);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <br />
    <br />
    <div class="container mt-5">
        <div class="row">
            <div class="col-lg-2">
            </div>
            <div class="col-lg-8">
                <div class="row">
                    <div class="col-8 pb-3">
                        <h4>Reporte Resumen Ventas:</h4>
                    </div>
                    <div class="col-4 pb-3">
                    </div>
                </div>
                <div class="card card-body">
                    <div class="row">
                        <div class="col-4">
                            <label>Entre Fechas:</label>
                            <asp:TextBox ID="txDate1" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                        </div>
                        <div class="col-4">
                            <label>&nbsp;</label>
                            <asp:TextBox ID="txDate2" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                        </div>
                        <div class="col-4">
                            <label>Evento:</label>
                            <asp:DropDownList ID="ddEvents" runat="server" CssClass="form-control form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <label>&nbsp;</label>
                            <asp:Button ID="btFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-dark w-100" CausesValidation="false" OnClick="btFiltrar_Click" />
                        </div>
                    </div>
                </div>
                <div class="card card-body">
                    <div class="row">
                        <div class="col-12">
                            <h4>Productos Vendidos:</h4>
                            <div id="columnchart_material" style="width: 700px; height: 500px;">
                            </div>
                            <div class="col-12">
                                <div class="table-responsive small">
                                    <asp:GridView ID="gvSales" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:BoundField HeaderText="#" DataField="Id" />
                                            <asp:BoundField HeaderText="EVENTO" DataField="event_name" />
                                            <asp:BoundField HeaderText="CANT. VENTAS" DataField="quantity" />
                                            <asp:BoundField HeaderText="RENTABILIDAD" DataField="profit" DataFormatString="{0:C2}" />
                                            <asp:BoundField HeaderText="TOTAL" DataField="total" DataFormatString="{0:C2}" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div align="center">No se encontraron datos</div>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="thead-dark" />
                                        <FooterStyle BackColor="#d9d9d9" ForeColor="Black" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#242121" />
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <asp:Label ID="lbMsg" runat="server" />
                    </div>
                    <div class="col-lg-2">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
