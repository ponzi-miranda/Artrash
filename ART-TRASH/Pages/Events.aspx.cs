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
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Tienditas");
                if (!Page.IsPostBack)
                {
                    List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                    events.Reverse();
                    ViewState["Events"] = events;
                    UsersDTO user = (UsersDTO)Session["usuario"];
                    if (user.roleId == 2)
                    {
                        btNewEvent.Visible = false;
                        List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByBrandID(user.Id).ToList();
                        List<EventsDTO> brandEvents = new List<EventsDTO>();

                        foreach (var inscription in inscriptions)
                        {
                            EventsDTO evento = events.First(x => x.Id == inscription.event_id);
                            brandEvents.Add(evento);
                        }

                        brandEvents.Reverse();

                        ViewState["Events"] = brandEvents;
                    }

                    ddEventos.DataSource = ViewState["Events"];
                    ddEventos.DataTextField = "name";
                    ddEventos.DataValueField = "Id";
                    ddEventos.DataBind();
                    ddEventos.Items.Add("TODOS");
                    ddEventos.Items.FindByText("TODOS").Selected = true;

                    gvEventos.DataSource = ViewState["Events"];
                    gvEventos.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvEventos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvEventos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                if(e.CommandName == "Detalles")
                {
                    UsersDTO user = (UsersDTO)Session["usuario"];
                    if (user.roleId == 2)
                    {
                        Response.Redirect("/Pages/BrandEventDetails.aspx?id=" + id.ToString());
                    }
                    else
                    {
                        Response.Redirect("/Pages/EventDetails.aspx?id=" + id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btNewEvent_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NewEvent.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<EventsDTO> eventsList = (List<EventsDTO>)ViewState["Events"];
                List<EventsDTO> eventsFiltered = new List<EventsDTO>();
                if (eventsList != null)
                {
                    if (ddEventos.SelectedValue != "TODOS")
                    {
                        if (eventsFiltered.Count() > 0)
                        {
                            eventsFiltered = eventsFiltered.Where(x => x.Id == Convert.ToInt32(ddEventos.SelectedValue)).ToList();
                        }
                        else
                        {
                            eventsFiltered = eventsList.Where(x => x.Id == Convert.ToInt32(ddEventos.SelectedValue)).ToList();
                        }
                    }
                    if (txFechaIncio.Text != string.Empty && txFechaFin.Text != string.Empty)
                    {
                        if (eventsFiltered.Count() > 0)
                        {
                            eventsFiltered = eventsFiltered.Where(x => x.start_date > DateTime.Parse(txFechaIncio.Text) && x.finish_date < DateTime.Parse(txFechaFin.Text)).ToList();
                        }
                        else
                        {
                            eventsFiltered = eventsList.Where(x => x.start_date > DateTime.Parse(txFechaIncio.Text) && x.finish_date < DateTime.Parse(txFechaFin.Text)).ToList();
                        }
                    }
                    if (ddEstado.SelectedValue != "TODOS")
                    {
                        if (eventsFiltered.Count() > 0)
                        {
                            eventsFiltered = eventsFiltered.Where(x => x.state == ddEstado.SelectedValue).ToList();
                        }
                        else
                        {
                            eventsFiltered = eventsList.Where(x => x.state == ddEstado.SelectedValue).ToList();
                        }
                    }                    
                    if (ddEventos.SelectedValue == "TODOS" && ddEstado.SelectedValue == "TODOS" && txFechaIncio.Text == string.Empty && txFechaFin.Text == string.Empty)
                    {
                        eventsFiltered = eventsList;
                    }

                    gvEventos.DataSource = eventsFiltered;
                    gvEventos.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}