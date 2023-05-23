<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportStock.aspx.cs" Inherits="ArtTrash.Pages.ImportStock" %>

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
                    <div class="row align-items-center">
                        <div class="col col-auto">
                            <h4>Importar Stock
                            <asp:Label ID="lbBrand" runat="server"></asp:Label>                                
                            <asp:Label ID="lbEvent" runat="server"></asp:Label></h4>
                        </div>
                        <div class="col">
                            <div class="d-flex align-items-center justify-content-end">
                                <div class="head-button-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                        <ProgressTemplate>
                                            <img src="../image/working.gif" width="50" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <asp:Label ID="lbMsg" runat="server"></asp:Label>
                <asp:Panel runat="server" ID="plAdmin">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-3">
                                    <div class="form-row form-floating">
                                        <asp:DropDownList ID="ddEventsAdmin" runat="server" CssClass="form-control small">
                                        </asp:DropDownList>
                                        <label class="form-label" for="">Tienditas<span></span></label>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-row form-floating">
                                        <asp:DropDownList ID="ddBrands" runat="server" CssClass="form-control small">
                                        </asp:DropDownList>
                                        <label class="form-label" for="">Marcas<span></span></label>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-row form-floating">
                                        <asp:TextBox ID="txProductAdmin" runat="server" CssClass="form-control" type="text"></asp:TextBox>
                                        <label class="form-label" for="">Producto<span></span></label>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-row form-floating">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btBuscarAdmin" runat="server" Text="Buscar" CssClass="btn btn-secondary w-100" CausesValidation="false" OnClick="btBuscarAdmin_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row h-100">
                            <div class="col-12">
                                <div class="table-responsive main-table-wrapper">
                                    <asp:GridView ID="gvStockAdmin" runat="server" CssClass="table table-sm table-hover main-table w-100" AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" OnRowCommand="gvStockAdmin_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbStockAdmin" runat="server" CssClass="form-control" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="#" DataField="serial_number" />
                                            <asp:BoundField HeaderText="MARCA" DataField="brand" />
                                            <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                            <asp:BoundField HeaderText="TIPO" DataField="type" />
                                            <asp:BoundField HeaderText="CANTIDAD" DataField="quantity" />
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/image/fea-edit.svg" Text="Button" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div align="center">No se encontraron datos</div>
                                        </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="plBrand">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-4">
                                    <div class="form-row form-floating">
                                        <asp:DropDownList ID="ddEvents" runat="server" CssClass="form-control small">
                                        </asp:DropDownList>
                                        <label class="form-label" for="">Tienditas<span></span></label>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-row form-floating">
                                        <asp:TextBox ID="txProduct" runat="server" CssClass="form-control" type="text"></asp:TextBox>
                                        <label class="form-label" for="">Producto<span></span></label>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-row form-floating">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary w-100" CausesValidation="false" OnClick="btBuscar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row h-100">
                            <div class="col-12 h-100">
                                <div class="table-responsive main-table-wrapper">
                                    <asp:GridView ID="gvStock" runat="server" CssClass="table table-sm table-hover main-table w-100" AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" OnRowCommand="gvStock_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbStock" runat="server" CssClass="form-control" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="#" DataField="serial_number" />
                                            <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                            <asp:BoundField HeaderText="TIPO" DataField="type" />
                                            <asp:BoundField HeaderText="CANTIDAD" DataField="quantity" />
                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/image/fea-edit.svg" Text="Button" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div align="center">No se encontraron datos</div>
                                        </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-6">
                                <div class="form-row form-floating">
                                    <asp:Button ID="btSeleccionarTodos" runat="server" Text="Seleccionar Todo" CssClass="btn btn-info w-100" CausesValidation="false" OnClick="btSeleccionarTodos_Click" />
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-row form-floating">
                                    <asp:Button ID="btImportar" runat="server" Text="Importar Stock" CssClass="btn btn-dark w-100" CausesValidation="false" OnClick="btImportar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal -->
            <div class="modal fade" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLongTitle">Editar Stock</h5>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:DropDownList ID="ddBrandStock" runat="server" AutoPostBack="true" CssClass="form-control small">
                                            </asp:DropDownList>
                                            <label class="form-label" for="">Marca<span></span></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-row form-floating">
                                            <asp:DropDownList ID="ddProducts" runat="server" CssClass="form-control small">
                                                <asp:ListItem Text="SELECCIONE MARCA" />
                                            </asp:DropDownList>
                                            <label class="form-label" for="">Productos<span></span></label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <asp:TextBox ID="txCantidad" type="Number" placeholder="Cantidad" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6">
                                        <button type="button" class="btn btn-secondary btn-small w-100" data-dismiss="modal">Cancelar</button>
                                    </div>
                                    <div class="col-6">
                                        <asp:Button ID="btGuardar" runat="server" Text="Confirmar" Class="btn btn-dark btn-small w-100" OnClick="btGuardar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btGuardar" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
