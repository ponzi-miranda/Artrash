using ArtTrash.Helpers;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (!Page.IsPostBack && !Page.IsCallback)
            {

            }
        }

        protected void btRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTerminos.Checked)
                {
                    UsersDTO user = new UsersDTO();

                    user.active = true;
                    user.email = txEmail.Text;
                    user.name = txNombreMarca.Text;
                    user.password = txContraseña.Text;
                    user.roleId = 2; //Brand
                    user.mobile = txTelefono.Text;
                    user.contact_person = txPersonaContacto.Text;

                    ServiceHelper.ws.SetUser(user);

                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    lbMsg.Text = "Para registrarse debe aceptar los Términos y Condiciones del sitio.";
                }
                
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}