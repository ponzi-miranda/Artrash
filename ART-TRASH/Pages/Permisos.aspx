<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="AlquilerMaquinarias.Paginas.Administracion.Permisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <div class="container mt-5">
        <br />
        <div class="row">

            <div class="col-md-4">
                <div class="card card-body" runat="server" id="Div1">
                    <h6>Perfiles</h6>

                    <asp:Button ID="btNuevoPerfil" runat="server" Text="Nuevo" Class="btn btn-sm btn-secondary w-50" OnClick="btNuevoPerfil_Click" />

                    <div runat="server" id="dvPerfil">
                        <br />
                        <label for="txPerfil">Perfil:</label>
                        <asp:TextBox ID="txPerfil" runat="server" class="form-control"></asp:TextBox>

                        <div class="form-inline">
                            <asp:Button ID="btSavePerfil" runat="server" Text="Guardar" Class="btn btn-sm btn-info w-50" OnClick="btSavePerfil_Click" />
                            <asp:Button ID="btCancelar" runat="server" Text="Cancelar" Class="btn btn-sm btn-secondary w-50" OnClick="btCancelar_Click" />
                        </div>

                    </div>

                    <br />
                     <div class="table-responsive small"><asp:GridView ID="gvPerfiles" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Font-Size="Small" OnRowCommand="gvPerfiles_RowCommand" Width="100%" DataKeyNames="Id">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="role" HeaderText="role" />
                            <asp:ButtonField ButtonType="Image" CommandName="Modify" ImageUrl="~~/image/fea-edit.svg.png" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView></div>

                </div>

            </div>

            <div class="col-md-4">
                <div class="card card-body">

                    <h6>Permisos por Perfil</h6>

                    <label for="ddPerfil">Perfil:</label>
                    <asp:DropDownList ID="ddPerfil" runat="server" class="custom-select d-block w-100" AutoPostBack="True" OnSelectedIndexChanged="ddPerfil_SelectedIndexChanged">
                    </asp:DropDownList>


                    <br />
                    <label for="cbMenu">Opción de Menú:</label>
                    <asp:CheckBoxList ID="cbMenu" runat="server">
                    </asp:CheckBoxList>

                    <asp:Button ID="btSavePermisos" runat="server" Text="Guardar Permisos" Class="btn btn-sm btn-info w-100" OnClick="btSavePermisos_Click" />


                </div>

            </div>

            <div class="col-md-4">
                <div class="card card-body" runat="server" id="dvContacto">

                    <h6>Usuario del Sistema</h6>

                    <label for="ddDocente">Usuario:</label>
                    <div class="form-inline">
                        <asp:TextBox ID="txBuscarDocente" runat="server" class="form-control w-50"></asp:TextBox>
                        <asp:Button ID="btBuscarDocente" runat="server" Text="Buscar" Class="btn btn-info w-50" OnClick="btBuscarDocente_Click" />
                    </div>
                    <br />
                     <div class="table-responsive small"><asp:GridView ID="gvUsuarios" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Font-Size="Small" OnRowCommand="gvPerfiles_RowCommand" Width="100%" DataKeyNames="Id">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                            <asp:BoundField DataField="Rol" HeaderText="Perfil" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView></div>


                </div>

            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <br />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
