<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="ArtTrash.Pages.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="plAdmin" runat="server">
                <div class="container mt-5">
                    <div class="row">
                        <div class="col-8 pb-3">
                            <h4>Listado de Ventas</h4>
                        </div>
                        <div class="col-1 pb-3">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <img alt="Cargando..." src="/img/progressbar.gif" class="img-fluid" width="32" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="col-3 pb-3">
                            <asp:Button ID="btNewSale" runat="server" CssClass="btn btn-dark w-100" Text="Nueva Venta" OnClick="btNewSale_Click" />
                        </div>
                        <div class="col-lg-12">
                            <asp:Label ID="lbMsg" runat="server"></asp:Label>
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-12 col-md-6">
                                            <label>Evento:</label>
                                            <asp:DropDownList ID="ddEvento" runat="server" CssClass="form-control form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-12 col-md-6">
                                            <label>Fecha</label>
                                            <asp:TextBox ID="txFecha" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-4">
                                            <label>Forma de pago:</label>
                                            <asp:DropDownList ID="ddFormaPago" runat="server" CssClass="form-control form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <label>Estado:</label>
                                            <asp:DropDownList ID="ddEstado" runat="server" CssClass="form-control form-control">
                                                <asp:ListItem Text="TODOS" />
                                                <asp:ListItem Text="CONFIRMADA" />
                                                <asp:ListItem Text="CANCELADA" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-4">
                                            <label>&nbsp;</label>
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-dark w-100" CausesValidation="false" OnClick="btnBuscar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 mt-3">
                             <div class="table-responsive small"> <div class="table-responsive small"><asp:GridView ID="gvVentas" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" OnRowCommand="gvVentas_RowCommand">
                                <Columns>
                                    <asp:ButtonField HeaderText="" ButtonType="Image" ImageUrl="~/image/fea-search.svg" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px" CommandName="Detalles" />
                                    <asp:BoundField HeaderText="#" DataField="Id" />
                                    <asp:BoundField HeaderText="FECHA" DataField="date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="EVENTO" DataField="event_name" />
                                    <asp:BoundField HeaderText="PAGO" DataField="payment_method" />
                                    <asp:BoundField HeaderText="MONTO" DataField="total" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="RENTABILIDAD" DataField="profit" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="ESTADO" DataField="state" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">No se encontraron datos</div>
                                </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                <HeaderStyle CssClass="thead-dark" />
                            </asp:GridView></div>

                            <div class="row">
                                <div class="col-3">
                                    <div class="card">
                                        <div class="card-read-more">
                                            <h2>Efectivo</h2>
                                            <h4>
                                                <asp:Label ID="lbCaja" runat="server" Font-Bold="True" ForeColor="Blue" Text="test" CssClass="m-lg-2"></asp:Label></h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="card">
                                        <div class="card-read-more">
                                            <h2>Transferencia</h2>
                                            <h4>
                                                <asp:Label ID="lbTransferencia" runat="server" Font-Bold="True" ForeColor="Blue" Text="test" CssClass="m-lg-2"></asp:Label></h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="card">
                                        <div class="card-read-more">
                                            <h2>Mercado Pago</h2>
                                            <h4>
                                                <asp:Label ID="lbMercadoPago" runat="server" Font-Bold="True" ForeColor="Blue" Text="test" CssClass="m-lg-2"></asp:Label></h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="card">
                                        <div class="card-read-more">
                                            <h2>Total</h2>
                                            <h4>
                                                <asp:Label ID="lbTotal" runat="server" Font-Bold="True" ForeColor="Blue" Text="test" CssClass="m-lg-2"></asp:Label></h4>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="plBrand" runat="server">
                <div class="container mt-5">
                    <div class="row">
                        <div class="col-11 pb-3">
                            <h4>Listado de Ventas</h4>
                        </div>
                        <div class="col-1 pb-3">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <img alt="Cargando..." src="/img/progressbar.gif" class="img-fluid" width="32" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="col-lg-12">
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-12 col-md-6">
                                            <label>Evento:</label>
                                            <asp:DropDownList ID="ddEventoBrand" runat="server" CssClass="form-control form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-12 col-md-6">
                                            <label>Fecha</label>
                                            <asp:TextBox ID="txFechaBrand" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-6">
                                            <label>Producto:</label>
                                            <asp:DropDownList ID="ddProductos" runat="server" CssClass="form-control form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-6">
                                            <label>&nbsp;</label>
                                            <asp:Button ID="btBuscarBrand" runat="server" Text="Buscar" CssClass="btn btn-dark w-100" CausesValidation="false" OnClick="btBuscarBrand_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 mt-3">
                             <div class="table-responsive small"> <div class="table-responsive small"><asp:GridView ID="gvVentasBrands" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:BoundField HeaderText="#" DataField="Id" />
                                    <asp:BoundField HeaderText="FECHA" DataField="date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="EVENTO" DataField="event_name" />
                                    <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                    <asp:BoundField HeaderText="CANTIDAD" DataField="quantity" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="MONTO" DataField="total" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="RETENCION" DataField="profit" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="SALDO" DataField="price" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">No se encontraron datos</div>
                                </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                <HeaderStyle CssClass="thead-dark" />
                            </asp:GridView></div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

