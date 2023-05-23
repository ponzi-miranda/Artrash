<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEvent.aspx.cs" Inherits="ArtTrash.Pages.NewEvent" %>

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
                    <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4">
                        <div class="col-8 pb-3">
                            <h4>Nuevo Evento:</h4>
                        </div>
                        <div class="card card-body">
                            <br />
                            <label for="txName">Nombre:</label>
                            <asp:TextBox ID="txName" runat="server" type="text" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txName"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label for="txInscription">Precio Inscripción:</label>
                            <asp:TextBox ID="txInscription" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txInscription"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label for="txStartDate">Fecha Incio:</label>
                            <asp:TextBox ID="txStartDate" runat="server" type="date" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txStartDate"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label for="txFinishDate">Fecha Fin:</label>
                            <asp:TextBox ID="txFinishDate" runat="server" type="date" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txFinishDate"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btCrear" runat="server" Text="Crear" class="btn btn-dark w-100" OnClick="btCrear_Click" OnClientClick="if(this.value === ' Crear ') { return false; } else { this.value = ' Crear '; }" />
                                    <asp:Label ID="lbMsg" runat="server" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
