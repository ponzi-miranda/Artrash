<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductSaleMetrics.aspx.cs" Inherits="ArtTrash.Pages.ProductSaleMetrics" %>

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
                        <div class="col-8 pb-3">
                            <h4>Reporte Porcentaje de Ventas:</h4>
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
