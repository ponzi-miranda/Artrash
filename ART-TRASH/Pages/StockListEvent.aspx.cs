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
    public partial class StockListEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Stock Tiendita");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        Response.Redirect("Events.aspx");
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
            EventsDTO evento = ServiceHelper.ws.GetEventByID(id);
            if (evento != null)
            {
                lbEvent.Text = evento.name;

                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(id).ToList();
                List<UsersDTO> brands = new List<UsersDTO>();

                foreach (var inscription in inscriptions)
                {
                    UsersDTO brand = ServiceHelper.ws.GetBrandByID(inscription.brand_id);
                    brands.Add(brand);
                }

                gvStock.DataSource = brands;
                gvStock.DataBind();
            }
        }

        protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvStock.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                if (e.CommandName == "Detalles")
                {
                    Response.Redirect("/Pages/StockListEventDetails.aspx?id="+ Convert.ToInt32(Request.QueryString["id"]) + "&brand=" + id.ToString());
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}