<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewProduct.aspx.cs" Inherits="ArtTrash.Pages.NewProduct" %>

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
                                <h4>Nuevo Producto:</h4>
                            </div>
                            <div class="card card-body">
                            <br />
                            <label for="txSerialNumber">Código Producto:</label>
                            <asp:TextBox ID="txSerialNumber" runat="server" type="text" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txSerialNumber"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label for="txDescription">Denominación:</label>
                            <asp:TextBox ID="txDescription" runat="server" type="text" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txDescription"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label>Tipo de Producto:</label>
                            <asp:DropDownList ID="ddProductsType" runat="server" CssClass="form-control form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddProductsType" InitialValue="Seleccione" ErrorMessage="Seleccione una opción (*)"/>
                            <br />
                            <label for="txPrice">Precio:</label>
                            <asp:TextBox ID="txPrice" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txPrice"
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
