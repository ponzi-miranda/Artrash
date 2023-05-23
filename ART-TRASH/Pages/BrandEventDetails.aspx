<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrandEventDetails.aspx.cs" Inherits="ArtTrash.Pages.BrandEventDetails" %>

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
                            <h4>Detalle Evento:</h4>
                        </div>
                        <div class="card card-body">
                            <div class="row">
                                <div class="col-6">
                                    <br />
                                    <label for="txName">Nombre:</label>
                                    <asp:TextBox ID="txName" runat="server" type="text" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <br />
                                    <label for="txInscription">Precio Inscripción:</label>
                                    <asp:TextBox ID="txInscription" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    <br />
                                    <label for="txStartDate">Fecha Incio:</label>
                                    <asp:TextBox ID="txStartDate" runat="server" type="date" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <br />
                                    <label for="txFinishDate">Fecha Incio:</label>
                                    <asp:TextBox ID="txFinishDate" runat="server" type="date" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <br />
                                    <label for="txState">Estado:</label>
                                    <asp:TextBox ID="txState" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-6">
                                    <asp:Button ID="btListadoVentas" runat="server" Text="Listado de Ventas" CssClass="btn btn-dark w-100" OnClick="btListadoVentas_Click"/>
                                </div>
                                <div class="col-6">
                                    <asp:Button ID="btListadoStock" runat="server" Text="Listado de Stock" CssClass="btn btn-dark w-100" OnClick="btListadoStock_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 mt-3">
                                <h5>Últimas ventas:</h5>
                                     <div class="table-responsive small"><asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="false" CssClass="small table table-sm table-hover main-table w-100 table-responsive small " DataKeyNames="Id" ShowFooter="true">
                                        <Columns>
                                            <asp:BoundField HeaderText="FECHA" DataField="date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="PRODUCTO" DataField="description"/>
                                            <asp:BoundField HeaderText="MONTO" DataField="total" DataFormatString="{0:C2}" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div align="center">No hay Ventas aún :(</div>
                                        </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView></div>
                                </div>
                                <asp:Label ID="lbMsg" runat="server" />
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
