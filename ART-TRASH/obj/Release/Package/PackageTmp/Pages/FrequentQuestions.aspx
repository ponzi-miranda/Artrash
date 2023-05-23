<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrequentQuestions.aspx.cs" Inherits="ArtTrash.Pages.FrequentQuestions" %>

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
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-10">
                        <div class="col-8 pb-3">
                            <h4>Preguntas Frecuentes:</h4>
                        </div>
                        <div class="card card-body">
                            <br />
                            <h5>Contacto directo WhatsApp: <a href="https://wa.me/3517656587?text=Hola%20tengo%20una%20consulta%20sobre%20la%20tiendita%20web" target="_blank" class="text-white">📞</a></h5>
                            <div class="row">
                                <div class="col-6">
                                    <div class="card card-body text-left" runat="server">
                                        <asp:Button Text="¿Cómo funciona la Tiendita?" runat="server" Class="btn btn-block btn-secondary" ID="btTienda" OnClick="btTienda_Click" />
                                        <br />
                                        <asp:Button Text="¿Qué necesito para participar?" runat="server" Class="btn btn-block btn-secondary" ID="btComoInscribirse" OnClick="btComoInscribirse_Click" />
                                        <br />
                                        <asp:Button Text="Estoy inscripto. ¿Qué debo hacer ahora?" runat="server" Class="btn btn-block btn-secondary" ID="btQueHacer" OnClick="btQueHacer_Click" />
                                        <br />                                        
                                        <asp:Button Text="Quiero añadir productos y ya comenzo la tienda." runat="server" Class="btn btn-block btn-secondary" ID="btStock" OnClick="btStock_Click" />
                                        <br />
                                        <asp:Button Text="Tengo una sugerencia para el sitio Web" runat="server" Class="btn btn-block btn-secondary" ID="btSugerencia" OnClick="btSugerencia_Click" />
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="card card-body text-left" runat="server">
                                        <h6><asp:Label ID="lbAnswer" runat="server" /></h6>
                                        <asp:Label ID="lbMsg" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                        <br />
                    <div class="col-lg-1">
                    </div>
                </div>
            </div>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
