using ArtTrash;
using ArtTrash.Helpers;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AlquilerMaquinarias.Paginas.Administracion
{

    public partial class Permisos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dvPerfil.Visible = false;
                lbMsg.Text = string.Empty;

                if (!Page.IsPostBack)
                {
                    //Cargo todos los menues existentes en la MasterPage.
                    LoadMasterPage();
                    ReCargarPerfiles();

                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        private void ReCargarPerfiles()
        {

            //Cargo Combo Perfiles
            var perfiles = ServiceHelper.ws.GetRoles().ToList();
            gvPerfiles.DataSource = perfiles;
            gvPerfiles.DataBind();

            //--
            ddPerfil.Items.Clear();
            foreach (var item in perfiles)
            {
                ddPerfil.Items.Add(new ListItem(item.role, item.Id.ToString()));
            }

            PerfilSelected();
        }

        private void LoadMasterPage()
        {
            //Site master = ((Site)this.Master);
            //UsersDTO usuario = master.Usuario;
            //HtmlGenericControl menu = master.GetMenu();

            //cbMenu.Items.Clear();
            //foreach (HtmlGenericControl liCategoria in menu.Controls)
            //{
            //    //Categorias.
            //    if (liCategoria.TagName == "li")
            //    {
            //        foreach (var opcionMenu in liCategoria.Controls)
            //        {
            //            if (opcionMenu.GetType() == typeof(HtmlAnchor))
            //            {
            //                HtmlAnchor a = ((HtmlAnchor)opcionMenu);
            //                cbMenu.Items.Add(new ListItem(a.InnerText, a.InnerText, true) { Selected = true });
            //            }
            //        }
            //    }
            //}
        }

        protected void btBuscarDocente_Click(object sender, EventArgs e)
        {
            try
            {
                BuscarUsuario();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        private void BuscarUsuario()
        {
            //var usuarios = ServiceHelper.ws.GetUsuarios(txBuscarDocente.Text.Trim());

            //gvUsuarios.DataSource = usuarios;
            //gvUsuarios.DataBind();
        }

        protected void btSavePerfiles_Click(object sender, EventArgs e)
        {
            try
            {
                //int idUsuario = Convert.ToInt32(ddDocente.SelectedValue);
                //List<UsuarioPerfilDTO> docentePerfiles = new List<UsuarioPerfilDTO>();

                //foreach (ListItem item in cbPerfiles.Items)
                //{
                //    if (item.Selected)
                //    {
                //        UsuarioPerfilDTO docentePerfil = new UsuarioPerfilDTO();
                //        docentePerfil.IdPerfil = Convert.ToInt32(item.Value);
                //        docentePerfil.IdUsuario = idUsuario;
                //        docentePerfiles.Add(docentePerfil);
                //    }
                //}

                //ServiceHelper.ws.SetUsuarioPerfiles(idUsuario, docentePerfiles.ToArray());
                //lbMsg.Text = "Perfiles del Docente Guardados.";

                lbMsg.Text = "Próximamente";
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ddPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PerfilSelected();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        private void PerfilSelected()
        {
            int idPerfil = Convert.ToInt32(ddPerfil.SelectedValue);
            var permisos = ServiceHelper.ws.GetSecurity(idPerfil, "ArtTrash").ToList();

            foreach (ListItem item in cbMenu.Items)
            {
                item.Selected = permisos.Exists(x => x.Menu == item.Value);
            }
        }

        protected void btSavePermisos_Click(object sender, EventArgs e)
        {
            try
            {
                int idPerfil = Convert.ToInt32(ddPerfil.SelectedValue);
                List<SecurityDTO> perfilPermisos = new List<SecurityDTO>();

                foreach (ListItem item in cbMenu.Items)
                {
                    if (item.Selected)
                    {
                        SecurityDTO perfilPermiso = new SecurityDTO();
                        perfilPermiso.IdRol = idPerfil;
                        perfilPermiso.Menu = item.Value;
                        perfilPermiso.Program = ServiceHelper.NOMBRE_PROGRAMA;

                        perfilPermisos.Add(perfilPermiso);
                    }
                }

                ServiceHelper.ws.SetPermisos(idPerfil, ServiceHelper.NOMBRE_PROGRAMA, perfilPermisos.ToArray());
                lbMsg.Text = "Permisos del Perfil Guardados.";

                //Recargo el menú
                Session["permisos"] = null;
                Site master = ((Site)this.Master);
                //master.MasterLoad();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvPerfiles.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                btSavePerfil.Attributes.Add("Id", id.ToString());
                txPerfil.Attributes.Add("Id", id.ToString());
                txPerfil.Text = System.Net.WebUtility.HtmlDecode(gvPerfiles.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
                dvPerfil.Visible = true;
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btSavePerfil_Click(object sender, EventArgs e)
        {
            try
            {
                //if (btSavePerfil.Attributes["Id"] == null)
                //    return;

                //int id = Convert.ToInt32(btSavePerfil.Attributes["Id"]);

                //SecurityDTO perfil = new SecurityDTO();
                //perfil.Id = id;
                //perfil.Nombre = txPerfil.Text.Trim();
                //ServiceHelper.ws.SetPerfil(perfil);

                //ReCargarPerfiles();
                //lbMsg.Text = "Perfil Actualizado.";
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btNuevoPerfil_Click(object sender, EventArgs e)
        {
            dvPerfil.Visible = true;
            txPerfil.Text = string.Empty;
            btSavePerfil.Attributes.Add("Id", "0");
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            dvPerfil.Visible = false;
            txPerfil.Text = string.Empty;
            btSavePerfil.Attributes.Add("Id", "0");
        }
    }
}