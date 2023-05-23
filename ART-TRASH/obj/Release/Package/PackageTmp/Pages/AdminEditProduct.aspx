<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminEditProduct.aspx.cs" Inherits="ArtTrash.Pages.AdminEditProduct" %>

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
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-8 pb-3">
                                <h4>Editar Producto:</h4>
                            </div>
                            <div class="col-4 pb-3">
                                <asp:Button ID="btEliminar" runat="server" Text="Eliminar Producto" CssClass="btn btn-secondary w-100" OnClick="btEliminar_Click" />
                            </div>
                        </div>
                        <div class="card card-body">
                            <label>Marca:</label>
                            <asp:TextBox ID="txBrand" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
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
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddProductsType" InitialValue="Seleccione" ErrorMessage="Seleccione una opción (*)" />
                            <br />
                            <label for="txPrice">Precio:</label>
                            <asp:TextBox ID="txPrice" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator RequiredFieldValidatorID="" runat="server" ControlToValidate="txPrice"
                            ErrorMessage="Este es un campo requerido (*)"></asp:RequiredFieldValidator>
                            <br />
                            <label>Estado:</label>
                            <asp:DropDownList ID="ddState" runat="server" CssClass="form-control form-control">
                                <asp:ListItem Text="ACTIVO" />
                                <asp:ListItem Text="INACTIVO" />
                            </asp:DropDownList>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btEditar" runat="server" Text="Guardar" class="btn btn-dark w-100" OnClick="btEditar_Click" OnClientClick="if(this.value === ' Guardar ') { return false; } else { this.value = ' Guardar '; }" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
