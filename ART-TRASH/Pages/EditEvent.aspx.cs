using ArtTrash.Service;
using ArtTrash.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class EditEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Editar Evento");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        Response.Redirect("/Pages/Events.aspx");
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
            EventsDTO editEvent = ServiceHelper.ws.GetEventByID(id);
            if (editEvent != null)
            {
                txFinishDate.Text = editEvent.finish_date.ToString("yyyy-MM-dd");
                txInscription.Text = editEvent.inscription.ToString();
                txName.Text = editEvent.name;
                txStartDate.Text = editEvent.start_date.ToString("yyyy-MM-dd");
            }
        }
        protected void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                EventsDTO editEvent = ServiceHelper.ws.GetEventByID(Convert.ToInt32(Request.QueryString["id"]));

                editEvent.inscription = Convert.ToDecimal(txInscription.Text);
                editEvent.name = txName.Text;
                editEvent.start_date = Convert.ToDateTime(txStartDate.Text);
                editEvent.finish_date = Convert.ToDateTime(txFinishDate.Text);

                editEvent = ServiceHelper.ws.SetEvent(editEvent);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}