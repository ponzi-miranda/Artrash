using ArtTrash.Helpers;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class BrandDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Detalle Marca");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        Response.Redirect("Menu.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void LoadData(int id)
        {
            UsersDTO user = ServiceHelper.ws.GetBrandByID(id);
            if (user != null)
            {
                if (user.roleId == 1 || user.roleId == 2 && user.Id == id)
                {
                    if (user.active)
                    {
                        lbBrand.Text = user.name;
                    }
                    else
                    {
                        lbBrand.Text = user.name + " ELIMINADA";
                        txContraseña.ReadOnly = true;
                        txEmail.ReadOnly = true;
                        txNombreMarca.ReadOnly = true;
                        txPersonaContacto.ReadOnly = true;
                        txTelefono.ReadOnly = true;
                        btEliminar.Enabled = false;
                        btActualizar.Enabled = false;
                    }
                    txEmail.Text = user.email;
                    txNombreMarca.Text = user.name;
                    txPersonaContacto.Text = user.contact_person;
                    txTelefono.Text = user.mobile;
                    var currentUser = (UsersDTO)Session["usuario"];
                    if (currentUser.roleId != 1)
                    {
                        btEliminar.Visible = false;
                    }
                    else
                    {
                        btPassword.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Menu.aspx");
                }
            }
        }

        protected void btActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var user = ServiceHelper.ws.GetBrandByID(Convert.ToInt32(Request.QueryString["id"]));
                if (user != null)
                {
                    user.contact_person = txPersonaContacto.Text;
                    user.email = txEmail.Text;
                    user.mobile = txTelefono.Text;
                    user.name = txNombreMarca.Text;

                    if (txContraseña.Text != user.password && txContraseñaNueva.Text != string.Empty)
                    {
                        lbMsg.Text = "Debe ingresar su contraseña anterior para actualizarla.";
                        return;
                    }
                    else
                    {
                        user.password = txContraseñaNueva.Text;
                    }

                    ServiceHelper.ws.SetUser(user);

                    LoadData(user.Id);
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
        
        protected void btEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var user = ServiceHelper.ws.GetBrandByID(Convert.ToInt32(Request.QueryString["id"]));
                if (user != null)
                {
                    user.active = false;                    

                    ServiceHelper.ws.SetUser(user);

                    LoadData(user.Id);
                }
                 
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (plPassword.Visible)
                {
                    plPassword.Visible = false;
                }
                else
                {
                    plPassword.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txContraseñaNueva.TextMode == TextBoxMode.Password)
                {
                    txContraseñaNueva.TextMode = TextBoxMode.SingleLine;
                }
                else
                {
                    txContraseñaNueva.TextMode = TextBoxMode.Password;
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}