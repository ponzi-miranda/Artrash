<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListBrands.aspx.cs" Inherits="ArtTrash.Pages.ListBrands" %>
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
                    <div class="col-lg-12">
                        <h4>Listado  Marcas:</h4>
                    </div>           
                    <div class="col-lg-12">
                        <asp:Label ID="lbMsg" runat="server"></asp:Label>
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-md-9">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txBrand" runat="server" CssClass="form-control" type="text"></asp:TextBox>
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
                         <div class="table-responsive small"><asp:GridView ID="gvMarcas" runat="server" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" OnRowCommand="gvMarcas_RowCommand">
                            <Columns>
                                <asp:ButtonField HeaderText="" ButtonType="Image" ImageUrl="~/image/fea-search.svg" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px" CommandName="Detalles" />
                                <asp:BoundField HeaderText="#" DataField="Id" />
                                <asp:BoundField HeaderText="MARCA" DataField="name" />
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
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>

