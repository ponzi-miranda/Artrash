<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockListEvent.aspx.cs" Inherits="ArtTrash.Pages.StockListEvent" %>

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
                        <h4>Listado de Stock Evento: <asp:Label ID="lbEvent" runat="server"></asp:Label></h4>
                    </div>
                    <div class="col-lg-12">
                        <asp:Label ID="lbMsg" runat="server"></asp:Label>
                        <div class="card">
                            <div class="card-body">
                                <div class="col-lg-12 mt-3">
                                     <div class="table-responsive small"><asp:GridView ID="gvStock" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" OnRowCommand="gvStock_RowCommand">
                                        <Columns>
                                            <asp:ButtonField HeaderText="" ButtonType="Image" ImageUrl="~/image/fea-search.svg" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px" CommandName="Detalles" />
                                            <asp:BoundField HeaderText="#" DataField="Id" />
                                            <asp:BoundField HeaderText="NOMBRE" DataField="name" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

