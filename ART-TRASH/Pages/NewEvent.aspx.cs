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
    public partial class NewEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Alta Tiendita");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {

                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btCrear_Click(object sender, EventArgs e)
        {
            try
            {

                EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                if (lastEvent != null && lastEvent.state != "FINALIZADO")
                {
                    lbMsg.Text = "No puede crear un evento mientras haya uno pendiente/en curso.";
                    return;
                }

                EventsDTO newEvent = new EventsDTO();

                newEvent.inscription = Convert.ToDecimal(txInscription.Text);
                newEvent.name = txName.Text;
                newEvent.start_date = Convert.ToDateTime(txStartDate.Text);
                newEvent.finish_date = Convert.ToDateTime(txFinishDate.Text);
                newEvent.state = "PENDIENTE";

                newEvent = ServiceHelper.ws.SetEvent(newEvent);

                Response.Redirect("Events.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}