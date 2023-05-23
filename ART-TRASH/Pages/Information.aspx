<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Information.aspx.cs" Inherits="ArtTrash.Pages.Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="right-container">
                <!-- Inner Container -->
                <div class="inner-container">
                    <div class="page-head">
                        <div class="row align-items-center">
                            <div class="col col-auto">
                                <h1>Información Util</h1>
                            </div>
                            <div class="col">
                                <div class="d-flex align-items-center justify-content-end">
                                    <div class="head-button-group">
                                        <asp:Label ID="lbMsg" runat="server" Font-Bold="True" ForeColor="Red" CssClass="m-lg-2"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="inner-content form-container">
                        <div class="accordion createAccordion" id="accordionPanelsStayOpenExample">

                            <!-- Details -->
            <%--                <div class="accordion-item">
                                <div class="accordion-header" id="panelsStayOpen-headingOne">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                                        ¿Quíenes Somos?
                                    </button>
                                </div>
                                <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
                                    <div class="accordion-body">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="iframe-container">
                                                    <p align="center">
                                                        <iframe width="560" height="315" src="https://www.youtube.com/embed/9YffrCViTVk" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"></iframe>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="accordion-item">
                                <div class="accordion-header" id="panelsStayOpen-headingTwo">
                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseTwo" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                        ¿Cómo Administrar mis Productos?  
                                    </button>
                                </div>
                                <div id="panelsStayOpen-collapseTwo" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-headingTwo">
                                    <div class="accordion-body">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="iframe-container">
                                                    <p align="center">
                                                        <iframe width="560" height="315" src="https://www.youtube.com/embed/48UYnxyG-3g" title="Productos" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="accordion-item">
                                <div class="accordion-header" id="panelsStayOpen-heading3">
                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapse3" aria-expanded="false" aria-controls="panelsStayOpen-collapse3">
                                        ¿Cómo Administrar el Stock?    
                                    </button>
                                </div>
                                <div id="panelsStayOpen-collapse3" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-heading3">
                                    <div class="accordion-body">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="iframe-container">
                                                    <p align="center">
                                                        <iframe width="560" height="315" src="https://www.youtube.com/embed/8hHISnccI7Q" title="Stock" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        <%--    <div class="accordion-item">
                                <div class="accordion-header" id="panelsStayOpen-heading4">
                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapse4" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                        ¿Cómo Ver mis Ventas?  
                                    </button>
                                </div>
                                <div id="panelsStayOpen-collapse4" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-heading4">
                                    <div class="accordion-body">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="iframe-container">
                                                    <p align="center">
                                                        <iframe width="560" height="315" src="https://www.youtube.com/embed/9YffrCViTVk" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"></iframe>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
