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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (!Page.IsPostBack && !Page.IsCallback)
            {

            }
        }

        protected void btIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txEmail.Text) || string.IsNullOrWhiteSpace(txEmail.Text) || string.IsNullOrEmpty(txContraseña.Text))
                {
                    lbModalTitle.Text = "Usuario o contraseña incorrecto";
                    lbModalText.Text = "Usuario o contraseña incorrecto";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", showModalScript, true);
                }
                else
                {
                    var usuariosArray = ServiceHelper.ws.Login(txEmail.Text, txContraseña.Text);
                    var usuarios = usuariosArray.ToList();
                    if (usuarios != null && usuarios.ToList().Count > 0)
                    {
                        var usuario = usuarios[0];
                        Session["usuario"] = usuario;

                        EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();

                        if (lastEvent.state == "PENDIENTE" && lastEvent.start_date == DateTime.Today)
                        {
                            lastEvent.state = "EN CURSO";

                            ServiceHelper.ws.SetEvent(lastEvent);
                        }
                        else if (lastEvent.state == "EN CURSO" && lastEvent.finish_date < DateTime.Today)
                        {
                            lastEvent.state = "FINALIZADO";

                            ServiceHelper.ws.SetEvent(lastEvent);
                        }

                        Response.Redirect("~/Pages/Menu.aspx");
                    }
                    else 
                    {
                        lbMsg.Text = "Usuario o contraseña incorrecto";
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

            string showModalScript = @"$('#modal').modal('show');";
            string hideModalScript = @"$('#modal').modal('hide');
                                        $('.modal-backdrop').remove();";

        protected void btRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Register.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}