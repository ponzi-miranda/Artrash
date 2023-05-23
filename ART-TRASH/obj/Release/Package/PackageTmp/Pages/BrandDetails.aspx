<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrandDetails.aspx.cs" Inherits="ArtTrash.Pages.BrandDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>

            <div class="container mt-5">
                <div class="row">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-8 pb-3">
                                <h4>Datos Emprendimiento:
                                    <asp:Label ID="lbBrand" runat="server" />
                                </h4>
                            </div>
                            <div class="col-4 pb-3">
                                <asp:Button ID="btEliminar" runat="server" Text="Eliminar Marca" CssClass="btn btn-secondary w-100" OnClick="btEliminar_Click" />
                            </div>
                        </div>
                        <div class="card card-body">
                            <label for="txNombreMarca">Nombre Marca/Emprendimiento:</label>
                            <asp:TextBox ID="txNombreMarca" runat="server" type="text" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txNombreMarca"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label for="txPersonaContaco">Persona de Contacto:</label>
                            <asp:TextBox ID="txPersonaContacto" runat="server" type="text" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txPersonaContacto"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label for="txTelefono">Telefono:</label>
                            <asp:TextBox ID="txTelefono" runat="server" type="text" class="form-control"></asp:TextBox>

                            <br />
                            <label for="txEmail">Email:</label>
                            <asp:TextBox ID="txEmail" runat="server" type="text" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txEmail"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Button ID="btPassword" runat="server" Text="Cambiar Contraseña" CssClass="btn btn-dark w-100" OnClick="btPassword_Click"/>
                            <asp:Panel ID="plPassword" runat="server" Visible="false">
                                <label for="txContraseña">Contraseña Anterior:</label>
                                <asp:TextBox ID="txContraseña" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                <br />
                                <label for="txContraseñaNueva">Nueva Contraseña:</label>
                                <asp:TextBox ID="txContraseñaNueva" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                <br />
                                <asp:Button ID="btMostrar" runat="server" Text="Mostrar/Ocultar" CssClass="btn btn-secondary w-100" OnClick="btMostrar_Click" />
                            </asp:Panel>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btActualizar" runat="server" Text="Actualizar Datos" CssClass="btn btn-dark w-100 mt-3" OnClick="btActualizar_Click" />
                                    <asp:Label ID="lbMsg" runat="server" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                    <div class="col-lg-3">
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
