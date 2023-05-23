<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="ArtTrash.Pages.Events" %>

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
                        <h4>Listado de Eventos</h4>
                    </div>
                    <div class="col-1 pb-3">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img alt="Cargando..." src="/img/progressbar.gif" class="img-fluid" width="32" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <div class="col-3 pb-3">
                        <asp:Button ID="btNewEvent" runat="server" CssClass="btn btn-dark w-100" Text="Crear nuevo evento" OnClick="btNewEvent_Click" OnClientClick="if(this.value === ' Crear nuevo evento ') { return false; } else { this.value = ' Crear nuevo evento '; }" />
                    </div>
                    <div class="col-lg-12">
                        <asp:Label ID="lbMsg" runat="server"></asp:Label>
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <label>Nombre Evento:</label>
                                        <asp:DropDownList ID="ddEventos" runat="server" CssClass="form-control form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <label>Estado</label>
                                        <asp:DropDownList ID="ddEstado" runat="server" CssClass="form-control form-control">
                                            <asp:ListItem>TODOS</asp:ListItem>
                                            <asp:ListItem>EN CURSO</asp:ListItem>
                                            <asp:ListItem>FINALIZADO</asp:ListItem>
                                            <asp:ListItem>PENDIENTE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-4">
                                        <label>Fecha Incio:</label>
                                        <asp:TextBox ID="txFechaIncio" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <label>Fecha Fin:</label>
                                        <asp:TextBox ID="txFechaFin" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btBuscar" runat="server" Text="Buscar" CssClass="btn btn-dark w-100" CausesValidation="false" OnClick="btBuscar_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="inner-content">
                                <div class="row h-100">
                                    <div class="col-12 h-100">
                                        <div class="row align-items-center justify-content-between">
                                            <div class="table-responsive small">
                                                <div class="col-lg-12 mt-3">
                                                    <div class="table-responsive small">
                                                        <asp:GridView ID="gvEventos" runat="server" CssClass="small table table-sm table-hover table-responsive main-table w-100" AutoGenerateColumns="false" DataKeyNames="Id" ShowHeaderWhenEmpty="true" OnRowCommand="gvEventos_RowCommand">
                                                            <Columns>
                                                                <asp:ButtonField HeaderText="" ButtonType="Image" ImageUrl="~/image/fea-search.svg" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px" CommandName="Detalles" />
                                                                <asp:BoundField HeaderText="#" DataField="Id" />
                                                                <asp:BoundField HeaderText="NOMBRE" DataField="name" />
                                                                <asp:BoundField HeaderText="FECHA INICIO" DataField="start_date" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField HeaderText="FECHA FIN" DataField="finish_date" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField HeaderText="INSCRIPCION" DataField="inscription" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />
                                                                <asp:BoundField HeaderText="ESTADO" DataField="state" />
                                                                <%--<asp:ButtonField HeaderText="" ButtonType="Image" ImageUrl="~/image/fea-edit.svg.png" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px" CommandName="Editar" />--%>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div align="center">No se encontraron datos</div>
                                                            </EmptyDataTemplate><HeaderStyle CssClass="thead-dark" /><FooterStyle BackColor="#d9d9d9" ForeColor="Black" /><HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" /><SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#F7F7F7" /><SortedAscendingHeaderStyle BackColor="#4B4B4B" /><SortedDescendingCellStyle BackColor="#E5E5E5" /><SortedDescendingHeaderStyle BackColor="#242121" />
                                                            <HeaderStyle CssClass="thead-dark" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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

