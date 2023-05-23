using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ArtTrash.Service;
using ArtTrash.Helpers;
//using AlquilerMaquinarias.Service;
//using AlquilerMaquinarias.Helpers;

namespace ArtTrash
{
    public partial class Site : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            UsersDTO usuario = (UsersDTO)Session["usuario"];

            if (usuario == null)
            {
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            ////--------------------------------------------------------------
            ////I show the correct menu according to the type of user
            mnuUsuario.Visible = false;
            mnuAdmin.Visible = false;

            //eUserType userType = SessionHelper.Current.User.GetUserType();
            if (usuario.roleId == 1) mnuAdmin.Visible = true;
            if (usuario.roleId == 2) mnuUsuario.Visible = true;
            ////--------------------------------------------------------------

            ////Change Organisation Option:
            //liChangeOrg.Visible = (userType == eUserType.Staff);


            //lbUserName.InnerText = $"{SessionHelper.Current.User.Email} ({SessionHelper.Current.User.GetUserType()})";

            //if (SessionHelper.Current.Student != null && SessionHelper.Current.Student.ProfileImage != null)
            //    imgUser.Src = "data:image/png;base64," + Convert.ToBase64String(SessionHelper.Current.Student.ProfileImage);


            ////I show the correct logo according to the current Organisation
            //if (SessionHelper.Current.Organisation != null && SessionHelper.Current.Organisation.Logo != null)
            //    imgLogo.Src = "data:image/png;base64," + Convert.ToBase64String(SessionHelper.Current.Organisation.Logo);
            //else
            //    imgLogo.Src = @"image/user-default-avatar.png";
        }

        public void SetTitle(string title)
        {
            hTitle.InnerText = title;
        }
    }
}
//        public UsersDTO Usuario { get { return (UsersDTO)Session["usuario"]; } }

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            MasterLoad();
//        }

//        internal void MasterLoad()
//        {

//            //Session Expirada
//            if (Session["usuario"] == null) //&& Session["proveedor"] == null)
//                Response.Redirect(ResolveUrl("~/Login.aspx"));

//            //--
//            AplicarSeguridad();

//            //--
//            if (Usuario != null)
//            {
//                lbUser.Text = $"Usuario: {Usuario.name} <br/>[{ServiceHelper.ws.GetRolByID(Usuario.roleId).role}]";
//            }
//        }

//        private void AplicarSeguridad()
//        {
//            if (Usuario != null && Usuario.roleId == 1)
//            {
//                return;
//            }

//            List<SecurityDTO> permisos = new List<SecurityDTO>();
//            if (Session["permisos"] == null)
//            {
//                if (Usuario != null)
//                {
//                    permisos = ServiceHelper.ws.GetSecurityByRol(Usuario.roleId, ServiceHelper.NOMBRE_PROGRAMA).ToList();
//                }

//                Session.Add("permisos", permisos);
//            }
//            else
//            {
//                permisos = (List<SecurityDTO>)Session["permisos"];
//            }

//            //Oculto las opciones del menu para las que no tenemos permiso.
//            Dictionary<string, bool> dicMenu = new Dictionary<string, bool>();
//            HtmlGenericControl menu = GetMenu();
//            foreach (HtmlGenericControl liCategoria in menu.Controls)
//            {
//                //Categorias.
//                if (liCategoria.TagName == "li")
//                {
//                    liCategoria.Visible = false;

//                    foreach (var opcionMenu in liCategoria.Controls)
//                    {
//                        if (opcionMenu.GetType() == typeof(HtmlAnchor))
//                        {
//                            HtmlAnchor a = ((HtmlAnchor)opcionMenu);
//                            bool existe = permisos.Exists(x => x.Menu == a.InnerText);
//                            a.Visible = existe;
//                            dicMenu.Add(a.HRef, existe);
//                            if (existe)
//                                liCategoria.Visible = true;
//                        }
//                    }
//                }
//            }

//            //Valido permiso sobre la página actual + QueryString, o lo mando al Home.
//            if (dicMenu.ContainsKey(Request.AppRelativeCurrentExecutionFilePath + "?" + Request.QueryString.ToString()))
//            {
//                if (!dicMenu[Request.AppRelativeCurrentExecutionFilePath + "?" + Request.QueryString.ToString()])
//                {
//                    Response.Redirect("~/Pages/Menu.aspx");
//                }
//            }
//            else
//            {
//                //Valudo permiso sobre la página actual, o lo mando al Home.
//                if (dicMenu.ContainsKey(Request.AppRelativeCurrentExecutionFilePath))
//                {
//                    if (!dicMenu[Request.AppRelativeCurrentExecutionFilePath])
//                    {
//                        Response.Redirect("~/Pages/Menu.aspx");
//                    }
//                }
//            }
//        }

//        public HtmlGenericControl GetMenu()
//        {
//            return ulMenu;
//        }
//    }
//}

