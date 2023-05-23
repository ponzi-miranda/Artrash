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
    public partial class EventDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Detalle Evento");
                lbMsg.Text = string.Empty;
                UsersDTO user = (UsersDTO)Session["usuario"];
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null && user.roleId != 2)
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
            EventsDTO thisEvent = ServiceHelper.ws.GetEventByID(id);
            if (thisEvent != null)
            {
                txFinishDate.Text = thisEvent.finish_date.ToString("yyyy-MM-dd");
                txInscription.Text = thisEvent.inscription.ToString();
                txName.Text = thisEvent.name;
                txStartDate.Text = thisEvent.start_date.ToString("yyyy-MM-dd");
                txState.Text = thisEvent.state;

                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(id).ToList();
                List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                List<UsersDTO> inscriptedBrands = new List<UsersDTO>();
                foreach (var inscription in inscriptions)
                {
                    inscriptedBrands.Add(brands.First(x => x.Id == inscription.brand_id));
                }
                ViewState["InscriptedBrands"] = inscriptedBrands;
                gvInscripciones.DataSource = ViewState["InscriptedBrands"];
                gvInscripciones.DataBind();
                if (thisEvent.state != "PENDIENTE")
                {
                    btAñadirMarcas.Enabled = false;
                    btEditar.Enabled = false;
                }
            }
        }
        protected void btAñadirMarcas_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/Pages/AddBrands.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]));
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btListadoStock_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("StockListEvent.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]));
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/Pages/EditEvent.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]));
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btListadoVentas_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SalesListEvent.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]));
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}