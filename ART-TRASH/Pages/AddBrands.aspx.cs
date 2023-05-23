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
    public partial class AddBrands : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Añadir Marcas");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        Response.Redirect("/Pages/ListEvents.aspx");
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
            EventsDTO currentEvent = ServiceHelper.ws.GetEventByID(id);
            if (currentEvent != null)
            {
                txEvento.Text = currentEvent.name;
                txInscription.Text = currentEvent.inscription.ToString();
            }
            
            List<UsersDTO> brands = ServiceHelper.ws.GetBrands().OrderBy(x => x.name).ToList();
            ViewState["Brands"] = brands;
            gvMarcas.DataSource = ViewState["Brands"];
            gvMarcas.DataBind();

            List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(id).ToList();

            List<UsersDTO> inscriptedBrands = new List<UsersDTO>();
            foreach (var inscription in inscriptions)
            {
                inscriptedBrands.Add(brands.First(x => x.Id == inscription.brand_id));
            }
            ViewState["InscriptedBrands"] = inscriptedBrands;
            gvInscripciones.DataSource = ViewState["InscriptedBrands"];
            gvInscripciones.DataBind();
        }

        protected void btAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(Convert.ToInt32(Request.QueryString["id"])).ToList();
                foreach (GridViewRow row in gvMarcas.Rows)
                {
                    CheckBox CheckRow = (row.Cells[0].FindControl("cbMarca") as CheckBox);
                    if (CheckRow.Checked)
                    {
                        if (inscriptions.Find(x => x.brand_id == Convert.ToInt32(gvMarcas.DataKeys[row.RowIndex]["Id"])) == null)
                        {
                            InscriptionsDTO inscription = new InscriptionsDTO();

                            inscription.event_id = Convert.ToInt32(Request.QueryString["id"]);
                            inscription.brand_id = Convert.ToInt32(gvMarcas.DataKeys[row.RowIndex]["Id"]);

                            ServiceHelper.ws.SetInscription(inscription);
                        }
                        else
                        {
                            lbMsg.Text = "No se puede añadir the same brand";
                        }

                    }
                }
                LoadData(Convert.ToInt32(Request.QueryString["id"]));
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;                
            }
        }

        protected void gvMarcas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                var index = e.Row.RowIndex;
                if (index > -1)
                {
                    int id = Convert.ToInt32(gvMarcas.DataKeys[index]["Id"]);

                    List<InscriptionsDTO> inscriptedBrands = ServiceHelper.ws.GetInscriptionsByEventID(Convert.ToInt32(Request.QueryString["id"])).ToList();
                    foreach (var brand in inscriptedBrands)
                    {
                        if (id == brand.brand_id)
                        {
                            CheckBox CheckRow = (e.Row.Cells[0].FindControl("cbMarca") as CheckBox);
                            CheckRow.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvInscripciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvInscripciones.DataKeys[e.RowIndex].Value);
                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(Convert.ToInt32(Request.QueryString["id"])).ToList();

                ServiceHelper.ws.DeleteInscription(inscriptions.First(x => x.brand_id == id).Id);

                LoadData(Convert.ToInt32(Request.QueryString["id"]));
            }
            catch (Exception ex)
            {

                lbMsg.Text = ex.Message;
            }
        }
    }
}