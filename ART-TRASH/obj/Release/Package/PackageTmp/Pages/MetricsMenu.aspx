<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MetricsMenu.aspx.cs" Inherits="ArtTrash.Pages.MetricsMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="container mt-5">
                <div class="row mt-5">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-6">
                        <div class="card card-body mt-5">
                            <asp:Panel ID="plBrands" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:Button Text="Ventas Por Categoria" runat="server" Class="btn btn-dark w-100" ID="ventasPorCategoria" OnClick="ventasPorCategoria_Click" />
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:Button Text="Ventas Por Producto" runat="server" Class="btn btn-dark w-100" ID="ventasPorProducto" OnClick="ventasPorProducto_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="plAdmin" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:Button Text="Reporte Rentabilidad Marcas" runat="server" Class="btn btn-dark w-100" ID="rentabilidadPorMarca" OnClick="rentabilidadPorMarca_Click" />
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:Button Text="Reporte % Ventas Marcas" runat="server" Class="btn btn-dark w-100" ID="ventasPorMarca" OnClick="ventasPorMarca_Click" />
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:Button Text="Reporte Cierre Total Eventos" runat="server" Class="btn btn-dark w-100" ID="rentabilidadPorEventos" OnClick="rentabilidadPorEventos_Click" />
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:Button Text="Reporte Cierre Total Marcas" runat="server" Class="btn btn-dark w-100" ID="marcasPorEventos" OnClick="marcasPorEventos_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Label ID="lbMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
            </div>
            <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
