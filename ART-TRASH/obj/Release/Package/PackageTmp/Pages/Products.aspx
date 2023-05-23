<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ArtTrash.Pages.Products" %>
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
                    <div class="col-5 pb-3">
                        <h4>Listado de Productos</h4>
                    </div>
                        <div class="col-1 pb-3">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <img alt="Cargando..." src="/img/progressbar.gif" class="img-fluid" width="32" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                    </div>
                      <div class="col-3 pb-3">
                        <%--<asp:Button ID="btNewPromotion" runat="server" CssClass="btn btn btn-secondary w-100" Visible="false" Text="Alta Promoción" OnClick="btNewPromotion_Click" />--%>
                        <asp:Button ID="btBorrar" runat="server" CssClass="btn btn btn-secondary w-100" Text="Borrar Productos" OnClick="btBorrar_Click" />
                    </div>
                    <div class="col-3 pb-3">
                        <asp:Button ID="btNewProduct" runat="server" CssClass="btn btn btn-dark w-100" Text="Alta Producto" OnClick="btNewProduct_Click" OnClientClick="if(this.value === ' Crear nuevo producto ') { return false; } else { this.value = ' Crear nuevo producto '; }" />
                    </div>
                    <div class="col-lg-12">
                    <asp:Label ID="lbMsg" runat="server"></asp:Label>
                        <asp:Panel runat="server" ID="plBrandFilters">
                            <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <label>Nombre Producto:</label>
                                        <asp:TextBox ID="txNombreProducto" runat="server" CssClass="form-control" type="text"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <label>Código Producto:</label>
                                        <asp:TextBox ID="txCodigoProducto" runat="server" CssClass="form-control"  type="text"></asp:TextBox>    
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-4">
                                        <asp:DropDownList ID="ddProductsType" runat="server" CssClass="form-control form-control" Visible="false">
                                            <asp:ListItem>PROMOCION</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>Estado</label>
                                        <asp:DropDownList ID="ddState" runat="server" CssClass="form-control form-control">
                                            <asp:ListItem>ACTIVO</asp:ListItem>
                                            <asp:ListItem>INACTIVO</asp:ListItem>                                      
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-dark w-100" OnClick="btnBuscar_Click" CausesValidation="false"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="plAdminFilters">
                            <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-md-4">
                                        <label>Nombre Producto:</label>
                                        <asp:TextBox ID="txNombreProductoAdmin" runat="server" CssClass="form-control" type="text"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-4">
                                        <label>Código Producto:</label>
                                        <asp:TextBox ID="txCodigoProductoAdmin" runat="server" CssClass="form-control"  type="text"></asp:TextBox>    
                                    </div>
                                    <div class="col-12 col-md-4">
                                        <label>Marca:</label>
                                        <asp:DropDownList ID="ddBrands" runat="server" CssClass="form-control form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-4">
                                        <asp:DropDownList ID="ddProductsTypeAdmin" runat="server" CssClass="form-control form-control" Visible="false">
                                            <asp:ListItem>PROMOCION</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>Estado</label>
                                        <asp:DropDownList ID="ddStateAdmin" runat="server" CssClass="form-control form-control">
                                            <asp:ListItem>ACTIVO</asp:ListItem>
                                            <asp:ListItem>INACTIVO</asp:ListItem>                                      
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btBuscarAdmin" runat="server" Text="Buscar" CssClass="btn btn-dark w-100" OnClick="btBuscarAdmin_Click" CausesValidation="false"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </asp:Panel>
                        
                    </div>

                    <div class="col-lg-12 mt-3">
                        <div class="table-responsive small">
                             <div class="table-responsive small"><asp:GridView ID="gvProductos" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id, type" ShowHeaderWhenEmpty="true" OnRowCommand="gvProductos_RowCommand">
                            <Columns>
                                <%--<asp:BoundField HeaderText="#" DataField="Id" />--%>
                                <asp:BoundField HeaderText="CÓDIGO" DataField="serial_number" />
                                <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                <asp:BoundField HeaderText="TIPO" DataField="product_type" />
                                <asp:BoundField HeaderText="PRECIO" DataField="price" />
                                <asp:ButtonField HeaderText="" ButtonType="Image" ImageUrl="~/image/fea-edit.svg" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px" CommandName="Editar" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">No se encontraron datos</div>
                            </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                            <HeaderStyle CssClass="thead-dark" />
                        </asp:GridView></div>
                        </div>
                        <div class="table-responsive small">
                             <div class="table-responsive small"> <div class="table-responsive small"><asp:GridView ID="gvProductosAdmin" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id, type" ShowHeaderWhenEmpty="true" OnRowCommand="gvProductosAdmin_RowCommand">
                            <Columns>
                                <%--<asp:BoundField HeaderText="#" DataField="Id" />--%>
                                <asp:BoundField HeaderText="MARCA" DataField="brand" />
                                <asp:BoundField HeaderText="CÓDIGO" DataField="serial_number" />
                                <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                <asp:BoundField HeaderText="TIPO" DataField="product_type" />
                                <asp:BoundField HeaderText="PRECIO" DataField="price" />
                                <asp:ButtonField HeaderText="" ButtonType="Image" ImageUrl="~/image/fea-edit.svg" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px" CommandName="Editar" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">No se encontraron datos</div>
                            </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                            <HeaderStyle CssClass="thead-dark" />
                        </asp:GridView></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
