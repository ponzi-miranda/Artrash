<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBrands.aspx.cs" Inherits="ArtTrash.Pages.AddBrands" %>

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
                    <div class="col-lg-2">
                    </div>
                    <div class="col-lg-8">
                        <div class="col-8 pb-3">
                            <h4>Asociar Marcas:</h4>
                        </div>
                        <div class="card card-body">
                            <br />
                            <label for="txName">Evento:</label>
                            <asp:TextBox ID="txEvento" runat="server" type="text" class="form-control"></asp:TextBox>
                            <br />
                            <label for="txInscription">Precio Inscripción:</label>
                            <asp:TextBox ID="txInscription" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                            <br />
                            <div class="row">
                                <div class="col-6">
                                    <div class="card card-body text-left" runat="server">
                                        <b>Marcas</b><br />
                                         <div class="table-responsive small"><asp:GridView ID="gvMarcas" runat="server" AutoGenerateColumns="false" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " DataKeyNames="Id" OnRowDataBound="gvMarcas_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbMarca" runat="server" CssClass="form-control" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Marca" DataField="name" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center">No se encontraron datos</div>
                                            </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                            <HeaderStyle CssClass="thead-dark" />
                                        </asp:GridView></div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="card card-body text-left" runat="server">
                                        <b>Marcas</b><br />
                                         <div class="table-responsive small"><asp:GridView ID="gvInscripciones" runat="server" AutoGenerateColumns="false" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " DataKeyNames="Id" OnRowDeleting="gvInscripciones_RowDeleting">
                                            <Columns>
                                                <asp:BoundField HeaderText="Marca" DataField="name" />
                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Img/no16.png" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25">
                                                    <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                                                </asp:CommandField>
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
                        <div class="card card-footer">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btAgregar" runat="server" Text="Agregar Marcas" class="btn btn-info w-100" OnClick="btAgregar_Click" />
                                    <asp:Label ID="lbMsg" runat="server" />
                                </div>
                            </div>
                        </div>
                        <br />
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
