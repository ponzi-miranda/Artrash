using ArtTrash.Helpers;
using ArtTrash.Models;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class BrandEventDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Detalle Evento");
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
            UsersDTO brand = (UsersDTO)Session["usuario"];
            EventsDTO thisEvent = ServiceHelper.ws.GetEventByID(id);
            if (thisEvent != null)
            {
                txFinishDate.Text = thisEvent.finish_date.ToString("yyyy-MM-dd");
                txInscription.Text = thisEvent.inscription.ToString();
                txName.Text = thisEvent.name;
                txStartDate.Text = thisEvent.start_date.ToString("yyyy-MM-dd");
                txState.Text = thisEvent.state;

                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(id).ToList();
                List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(brand.Id).ToList();
                List<Sale> salesDetailEvent = new List<Sale>();


                foreach (var sale in sales)
                {
                    var salesDetailsEvent = salesDetails.Where(x => x.sale_id == sale.Id).ToList();

                    if (salesDetailsEvent != null)
                    {
                        foreach (var saleDetailEvent in salesDetailsEvent)
                        {
                            Sale saleEvent = new Sale();

                            saleEvent.total = saleDetailEvent.total;
                            saleEvent.date = sale.date;
                            saleEvent.description = ServiceHelper.ws.GetProductByID(saleDetailEvent.product_id).description;

                            salesDetailEvent.Add(saleEvent);
                        }
                    }
                }

                gvSales.DataSource = salesDetailEvent;
                gvSales.DataBind();

                if (salesDetailEvent.Count > 0)
                {
                    decimal total = 0;
                    gvSales.FooterRow.Cells[1].Text = "Total";
                    gvSales.FooterRow.Cells[2].Font.Bold = true;
                    gvSales.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;

                    foreach (var item in salesDetailEvent)
                    {
                        total = total + Convert.ToDecimal(item.total);
                        gvSales.FooterRow.Cells[2].Text ="$ " + total.ToString();
                        gvSales.FooterRow.Cells[2].Font.Bold = true;
                        gvSales.FooterRow.Cells[1].Font.Bold = true;
                    }
                }
            }
        }
        protected void btListadoStock_Click(object sender, EventArgs e)
        {
            try
            {
                UsersDTO brand = (UsersDTO)Session["usuario"];
                Response.Redirect("/Pages/StockListEventDetails.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]) + "&brand=" + brand.Id);
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
                UsersDTO brand = (UsersDTO)Session["usuario"];
                Response.Redirect("/Pages/SalesListEventDetails.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]) + "&brand=" + brand.Id);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}