<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PercentageProdutTypeSales.aspx.cs" Inherits="ArtTrash.Pages.PercentageProdutTypeSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        //Do not call this method on pageload
        google.setOnLoadCallback(drawChart);
        function drawChart() {

            var data = new google.visualization.arrayToDataTable(<%=this.obtenerDatos()%>);


            var options = {
                "title": "Tipos de producto vendidos:",
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


            var chart = new google.visualization.PieChart(document.getElementById('columnchart_material'));
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
                                <h4>Reporte Porcentaje de Ventas:</h4>
                            </div>
                            <div class="col-4 pb-3">
                            </div>
                        </div>
                        <div class="card card-body">
                            <label>Entre Fechas:</label>
                            <div class="row">
                                <div class="col-6">
                                    <asp:TextBox ID="txDate1" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <asp:TextBox ID="txDate2" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <label>Evento:</label>
                                    <asp:DropDownList ID="ddEvents" runat="server" CssClass="form-control form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-6">
                                    <label>&nbsp;</label>
                                    <asp:Button ID="btFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-dark w-100" CausesValidation="false" OnClick="btFiltrar_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="card card-body">
                            <div class="row">
                                <div class="col-12">
                                    <h4>Porcentaje de Ventas:</h4>
                  <%--                  <asp:Chart ID="Chart1" runat="server"
                                        Height="500px" Width="700px">
                                        <Legends>
                                            <asp:Legend Alignment="Center" Docking="Bottom"
                                                IsTextAutoFit="False" Name="Default"
                                                LegendStyle="Row" />
                                        </Legends>
                                        <Series>
                                            <asp:Series Name="Default" />
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"
                                                BorderWidth="0">
                                                <AxisY>
                                                    <LabelStyle Format="{0:p}" />
                                                </AxisY>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>--%>

                                    <div id="columnchart_material" style="width: 700px; height: 500px;"></div>
                                </div>
                                <div class="col-12">
                                     <div class="table-responsive small"><asp:GridView ID="gvSales" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:BoundField HeaderText="#" DataField="Id" />
                                            <asp:BoundField HeaderText="EVENTO" DataField="event_name" />
                                            <asp:BoundField HeaderText="TIPO" DataField="product_name" />
                                            <asp:BoundField HeaderText="CANTIDAD VENTAS" DataField="sales" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div align="center">No se encontraron datos</div>
                                        </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView></div>
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
</asp:Content>
