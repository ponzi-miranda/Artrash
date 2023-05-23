<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesListEventDetails.aspx.cs" Inherits="ArtTrash.Pages.SalesListEventDetails" %>
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
                    <div class="col-8 pb-3">
                        <h4>Listado Ventas <asp:Label ID="lbBrand" runat="server"></asp:Label> Evento:
                            <asp:Label ID="lbEvent" runat="server"></asp:Label></h4>
                    </div>
                    <div class="col-1 pb-3">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img alt="Cargando..." src="/img/progressbar.gif" class="img-fluid" width="32" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div class="col-3 pb-3">
                        <asp:Button ID="btDownload" runat="server" CssClass="btn btn-dark w-100" Text="Descargar PDF" OnClick="btDownload_Click" Visible="false" />
                    </div>
                    <div class="col-lg-12">
                        <asp:Label ID="lbMsg" runat="server"></asp:Label>
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-md-9">
                                        <label>Fecha:</label>
                                        <asp:TextBox ID="txDate" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-3">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btBuscar" runat="server" Text="Buscar" CssClass="btn btn-dark w-100" CausesValidation="false" OnClick="btBuscar_Click"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12 mt-3">
                         <div class="table-responsive small"><asp:GridView ID="gvSales" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowFooter="true" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField HeaderText="#" DataField="Id" />
                                <asp:BoundField HeaderText="PRODUCTO" DataField="description" />
                                <asp:BoundField HeaderText="FECHA" DataField="date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="CANTIDAD" DataField="quantity"/>
                                <asp:BoundField HeaderText="PAGO" DataField="payment_method" />
                                <asp:BoundField HeaderText="TOTAL" DataField="total" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign ="Right"/>
                                <asp:BoundField HeaderText="20% RET" DataField="profit" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign ="Right"/>
                                <asp:BoundField HeaderText="SALDO" DataField="brandTotal" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign ="Right"/>
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">No se encontraron datos</div>
                            </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                            <HeaderStyle CssClass="thead-dark" />
                        </asp:GridView></div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btDownload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

