<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PercentageSalesMetrics.aspx.cs" Inherits="ArtTrash.Pages.MetricsDetails" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container mt-5">
                <div class="row">
                    <div class="col-lg-2">
                    </div>
                    <div class="col-lg-8">
                        <div class="row">
                            <div class="col-8 pb-3">
                             <h4>Reporte % Ventas Marcas:</h4>
                            </div>
                            <div class="col-4 pb-3">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <img alt="Cargando..." src="/img/progressbar.gif" class="img-fluid" width="32" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
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
                                    <asp:Chart ID="Chart1" runat="server"
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
                                    </asp:Chart>
                                </div>
                                <div class="col-12">
                                     <div class="table-responsive small"><asp:GridView ID="gvSales" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true">
                                        <Columns>                                            
                                            <asp:BoundField HeaderText="#" DataField="Id" />
                                            <asp:BoundField HeaderText="EVENTO" DataField="event_name" />
                                            <asp:BoundField HeaderText="MARCA" DataField="brand_name" />
                                            <asp:BoundField HeaderText="CANTIDAD VENTAS" DataField="sales" ItemStyle-HorizontalAlign ="Right"/>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
