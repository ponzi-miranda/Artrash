<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPromotion.aspx.cs" Inherits="ArtTrash.Pages.EditPromotion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
     <br />
    <br />
    <br />
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="container mt-5">
                <div class="row">
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-10">
                        <div class="col-8 pb-3">
                            <h4>Editar Promocion:</h4>
                        </div>
                        <div class="card card-body">
                            </br>
                            <div class="row mt-3">
                                <div class="col-8">
                                    <asp:TextBox ID="txDescription" runat="server" class="form-control" placeholder="Descripción"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <asp:TextBox ID="txPrice" runat="server" type="number" class="form-control w-auto" placeholder="Precio"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-8">
                                    <asp:DropDownList ID="ddProducts" class="form-control" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <asp:TextBox ID="txQuantity" runat="server" type="number" AutoPostBack="true" class="form-control w-auto" placeholder="Cantidad"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-6 ">
                                    <asp:Button ID="btAdd" runat="server" class="btn btn-secondary btn-sm w-100" Text="Añadir" Width="300" OnClick="btAdd_Click" CauseValidation="false" />
                                </div>
                                <div class="col-6 ">
                                    <asp:Button ID="btSave" runat="server" class="btn btn-dark btn-sm w-100" Text="Guardar Promoción" Width="300" CauseValidation="false" OnClick="btSave_Click" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-12">
                                    <asp:Label ID="lbMsg" runat="server" />
                                     <div class="table-responsive small"><asp:GridView ID="gvProducts" runat="server" DataKeyNames="Id" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowCommand="gvProducts_RowCommand">
                                        <Columns>
                                            <%--<asp:BoundField DataField="Id" HeaderText="Productos Seleccionados" />--%>
                                            <asp:BoundField DataField="description" HeaderText="Productos Seleccionados" />
                                            <asp:BoundField DataField="quantity" HeaderText="Cantidad" />
                                               <asp:ButtonField ButtonType="Image" ImageUrl="~~/image/fea-edit.svg.png" HeaderText="Editar" Text="Button" ItemStyle-HorizontalAlign="Center" CommandName="Editar">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:ButtonField>                                                
                                            <asp:ButtonField ButtonType="Image" HeaderText="Eliminar" ImageUrl="~/Img/no16.png" Text="Button" ItemStyle-HorizontalAlign="Center" CommandName="Eliminar">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark" />
                                        <EmptyDataTemplate>Todavía no ha seleccionado ningún producto.</EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
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
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
