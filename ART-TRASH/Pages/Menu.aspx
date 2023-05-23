<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ArtTrash.Pages.Menu" %>

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
                            <asp:Label ID="lbMsg" runat="server" />
                    </div>
                    <br />
                </div>
                <section class="wrapper">
                    <div class="container-fostrap">
                        <div class="content">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-1 col-md-12">

                                    </div>
                                    <div class="col-lg-2 col-md-12">
                                        <div class="card">
                                            <img alt="Logo" src="/Img/sneakers-512.png" class="img-fluid" />
                                                <asp:Button Text="Productos" runat="server" Class="btn btn-dark btn-md" ID="btProducts" OnClick="btProducts_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-12">
                                        <div class="card">
                                            <img alt="Logo" src="/Img/cash-512.png"class="img-fluid" />
                                                <asp:Button Text="Ventas" runat="server" Class="btn btn-dark btn-md" ID="btSales" OnClick="btSales_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-12">
                                        <div class="card">
                                            <img alt="Logo" src="/Img/lightbulb-512.png" class="img-fluid" />
                                                <asp:Button Text="Eventos" runat="server" Class="btn btn-dark btn-md" ID="btEvents" OnClick="btEvents_Click" />
                                        </div>
                                    </div>                                
                                    <div class="col-lg-2 col-md-12">
                                        <div class="card">
                                            <img alt="Logo" src="/Img/937737.png" class="img-fluid" />
                                                <asp:Button Text="Stock" runat="server" Class="btn btn-dark btn-md" ID="btStock" OnClick="btStock_Click" />
                                        </div>
                                    </div>
                                    <%--<div class="col-lg-2 col-md-12">
                                        <div class="card">
                                            <img alt="Logo" src="/Img/937737.png" class="img-fluid" />
                                            <div class="card-read-more">
                                                <asp:Button Text="Métricas" runat="server" Class="btn btn-block btn-dark mt-3" ID="btMetrics" OnClick="btMetrics_Click" />
                                            </div>
                                        </div>
                                    </div>--%>
                                        <div class="col-lg-2 col-md-12">
                                        <div class="card">
                                            <img alt="Logo" src="/Img/mac-512.png" class="img-fluid" />
                                                <asp:Button Text="Perfil" runat="server" Class="btn btn-dark btn-md" ID="btPerfil" OnClick="btPerfil_Click" />
                                                <asp:Button Text="Marcas" runat="server" Class="btn btn-dark btn-md" ID="btMarcas" OnClick="btMarcas_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-12">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
            </section>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
