<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSale.aspx.cs" Inherits="ArtTrash.Pages.NewSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
        function CalcularMonto2() {
            var Monto = document.getElementById("<%=txAmount.ClientID%>");
            var Monto1 = document.getElementById("<%=txAmount1.ClientID%>");
            var Monto2 = document.getElementById("<%=txAmount2.ClientID%>");

            Monto2.value = parseFloat(Monto.value) - Monto1.value;
        }
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container mt-5">
                <div class="row">
                    <div class="col-lg-2">
                    </div>
                    <div class="col-lg-8">
                        <div class="col-8 pb-3">
                            <h4>Nueva Venta:</h4>
                        </div>
                        <div class="card card-body">
                            <br />
                            <label for="ddBrand">Seleccione Marca:</label>
                            <asp:DropDownList ID="ddBrand" runat="server" CssClass="form-control form-control" AutoPostBack="true" OnSelectedIndexChanged="ddBrand_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <label for="txProduct">Buscar Producto:</label>
                            <asp:TextBox ID="txProduct" runat="server" type="text" class="form-control" AutoPostBack="true" OnTextChanged="txProduct_TextChanged"></asp:TextBox>
                            <br />
                            <label>Productos:</label>
                            <asp:DropDownList ID="ddProducts" runat="server" CssClass="form-control form-control">
                                <asp:ListItem Text="SELECCIONE MARCA" />
                            </asp:DropDownList>
                            <br />
                          
                            <%--<label for="rbPaymentList">Forma de Pago:</label>
                            <asp:RadioButtonList ID="rbPaymentList" runat="server">
                                <asp:ListItem Text=" -  EFECTIVO" Value="1"/>
                                <asp:ListItem Text=" -  TRANSFERENCIA" Value="2" />
                            </asp:RadioButtonList>--%>
                            <br />
                            <label for="txQuantity">Cantidad:</label>
                            <asp:TextBox ID="txQuantity" runat="server" type="text" class="form-control"></asp:TextBox>
                            <br />
                            <label for="txDiscount">Descuento (promos):</label>
                            <asp:TextBox ID="txDiscount" runat="server" type="text" class="form-control"></asp:TextBox>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Button ID="btAddProduct" runat="server" Text="Agregar Producto" CssClass="btn btn-dark w-100" OnClick="btAddProduct_Click" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btConfirmSale" runat="server" Text="Confirmar Venta" CssClass="btn btn-secondary w-100" OnClick="btConfirmSale_Click" />
                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="lbMsg" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 mt-3">
                                     <div class="table-responsive small"><asp:GridView ID="gvProductos" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" OnRowDeleting="gvProductos_RowDeleting" ShowFooter="true">
                                        <Columns>
                                            <asp:BoundField HeaderText="#" DataField="Id" />
                                            <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                            <asp:BoundField HeaderText="PRECIO" DataField="price" DataFormatString="{0:C2}" />
                                            <asp:BoundField HeaderText="TIPO" DataField="product_type" />
                                            <asp:BoundField HeaderText="CANTIDAD" DataField="quantity" />
                                            <asp:BoundField HeaderText="MONTO" DataField="subtotal" DataFormatString="{0:C2}" />
                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/no16.png" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25">
                                                <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div align="center">No se han añadido productos.</div>
                                        </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView></div>
                                </div>
                            </div>

                             <asp:Panel runat="server" ID="plFormasPago" Visible="false">

                            <h5>Seleccione forma de pago:</h5>
                            <div class="row">
                                <div class="col-8">
                                    <asp:DropDownList ID="ddPaymentType" runat="server" CssClass="form-control form-control" AutoPostBack="true" OnSelectedIndexChanged="ddPaymentType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <asp:TextBox ID="txAmount" type="number" placeholder="Monto" CssClass="form-control w-auto" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <asp:Panel runat="server" ID="plMixta" Visible="false">
                                <div class="row">
                                    <div class="col-8">
                                        <label>Forma de Pago 1:</label>
                                        <asp:DropDownList ID="ddPaymentType1" runat="server" CssClass="form-control form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>Monto 1:</label>
                                        <asp:TextBox ID="txAmount1" type="number" placeholder="Monto" CssClass="form-control w-auto" runat="server" onchange="CalcularMonto2()"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-8">
                                        <label>Forma de Pago 2:</label>
                                        <asp:DropDownList ID="ddPaymentType2" runat="server" CssClass="form-control form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <label>Monto 2:</label>
                                        <asp:TextBox ID="txAmount2" type="text" placeholder="Monto" CssClass="form-control w-auto" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 mt-3">
                                <asp:Button ID="btSave" runat="server" Text="Confirmar Forma de Pago" Class="btn btn-dark  w-100" OnClick="btSave_Click" />
                            </div>
                        </asp:Panel>

                        </div>
                        <br />
                       

                        <asp:Panel runat="server" Visible="false">  
                              <label>Promociónes:</label>
                            <asp:DropDownList ID="ddPromotions" runat="server" CssClass="form-control form-control">
                                <asp:ListItem Text="SELECCIONE MARCA" />
                            </asp:DropDownList>
                            <br />
                        </asp:Panel>
                    </div>
                    <div class="col-lg-2">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
