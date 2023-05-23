<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SaleDetails.aspx.cs" Inherits="ArtTrash.Pages.SaleDetails" %>
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
                       <div class="row">
                            <div class="col-8 pb-3">
                                <h4>Detalle Venta: <asp:Label ID="lbDetail" runat="server" /></h4>
                            </div>
                            <div class="col-2 pb-3">
                                <asp:Button ID="btImprimir" runat="server" Text="Imprimir" CssClass="btn btn-secondary w-100" OnClick="btImprimir_Click"/>
                            </div>
                           <div class="col-2 pb-3">
                                <asp:Button  ID="btEliminar" runat="server" Text="Eliminar" CssClass="btn btn-dark w-100" OnClick="btEliminar_Click" OnClientClick="if(this.value === ' Eliminar ') { return false; } else { this.value = ' Eliminar '; }" />
                            </div>
                        </div>
                        <div class="card card-body">
                            <br />
                            <label for="txEvento">Evento:</label>
                            <asp:TextBox ID="txEvento" runat="server" type="text" CssClass="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                            <br />
                            <label for="txFecha">Fecha:</label>
                            <asp:TextBox ID="txFecha" runat="server" type="date"  CssClass="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                            <br />
                            <%--<label for="rbPaymentList">Forma de Pago:</label>
                            <asp:RadioButtonList ID="rbPaymentList" runat="server" Enabled="false">
                                <asp:ListItem Text=" -  EFECTIVO" Value="1"/>
                                <asp:ListItem Text=" -  TRANSFERENCIA" Value="2" />
                            </asp:RadioButtonList>--%>
                              <div class="row">
                                <div class="col-8">
                                    <label>Forma de Pago:</label>
                                    <asp:DropDownList ID="ddFormaPago" runat="server" CssClass="form-control form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <label>Monto:</label>
                                    <asp:TextBox ID="txMonto" type="number" ReadOnly="true" placeholder="Monto" CssClass="form-control w-auto" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <asp:Panel runat="server" ID="plMixta" Visible="false">
                                <div class="row">
                                    <div class="col-8">
                                        <label>Forma de Pago 1:</label>
                                        <asp:DropDownList ID="ddFormaPago1" runat="server" CssClass="form-control form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>Monto 1:</label>
                                        <asp:TextBox ID="txMonto1" type="number" ReadOnly="true" placeholder="Monto" CssClass="form-control w-auto" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-8">
                                        <label>Forma de Pago 2:</label>
                                        <asp:DropDownList ID="ddFormaPago2" runat="server" CssClass="form-control form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>Monto 2:</label>
                                        <asp:TextBox ID="txMonto2" type="text" placeholder="Monto" ReadOnly="true" CssClass="form-control w-auto" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lbMsg" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 mt-3">
                                     <div class="table-responsive small"><asp:GridView ID="gvProductos" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" ShowFooter="true">
                                    <Columns>
                                        <asp:BoundField HeaderText="#" DataField="Id" />
                                        <asp:BoundField HeaderText="MARCA" DataField="brand_name" />
                                        <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                        <asp:BoundField HeaderText="PRECIO" DataField="price" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign ="Right"/>
                                        <asp:BoundField HeaderText="TIPO" DataField="payment_method"/>
                                        <asp:BoundField HeaderText="CANTIDAD" DataField="quantity"/>
                                        <asp:BoundField HeaderText="MONTO" DataField="total" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign ="Right"/>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">No se han añadido productos.</div>
                                    </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                    <HeaderStyle CssClass="thead-dark" />
                                </asp:GridView></div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
            <div class="col-lg-1">
                    </div>
                </div>
            </div>
        </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btImprimir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
