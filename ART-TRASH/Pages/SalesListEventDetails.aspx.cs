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
    public partial class SalesListEventDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Ventas Detalle");
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null && Request.QueryString["brand"] != null)
                    {
                        LoadData(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["brand"]));
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

        protected void LoadData(int id, int brand)
        {
            UsersDTO user = (UsersDTO)Session["usuario"];
            if (user.Id != brand && user.roleId == 2)
            {
                Response.Redirect("Sales.aspx");
            }
            EventsDTO evento = ServiceHelper.ws.GetEventByID(id);
            List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(id).Where(x => x.state == "CONFIRMADA").ToList();
            List<Sales_detailsDTO> saleDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(brand).ToList();

            List<Sale> salesModel = new List<Sale>();
            string paymentMethod = "EFECTIVO";
            foreach (var sale in saleDetails)
            {
                var saleData = sales.Where(x => x.Id == sale.sale_id);
                if (saleData.Count() > 0)
                {
                    if (saleData.First().payment_method_id != 1)
                    {
                        paymentMethod = "TRANSFERENCIA";
                    }
                    Sale saleModel = new Sale();

                    saleModel.Id = sale.Id;
                    saleModel.description = ServiceHelper.ws.GetProductByID(sale.product_id).description;
                    saleModel.quantity = sale.quantity;
                    saleModel.date = saleData.First().date;
                    saleModel.payment_method = paymentMethod;
                    saleModel.total = sale.total;
                    saleModel.profit = sale.profit;
                    saleModel.brandTotal = sale.total - sale.profit;

                    salesModel.Add(saleModel);
                }
            }

            ViewState["Ventas"] = salesModel;
            gvSales.DataSource = ViewState["Ventas"];
            gvSales.DataBind();

            if (salesModel != null)
            {
                if (salesModel.Count > 0)
                {
                    decimal total = 0;
                    decimal profit = 0;
                    decimal totalBrand = 0;
                    gvSales.FooterRow.Cells[3].Text = "Total";
                    gvSales.FooterRow.Cells[4].Font.Bold = true;
                    gvSales.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    gvSales.FooterRow.Cells[5].Font.Bold = true;
                    gvSales.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    gvSales.FooterRow.Cells[6].Font.Bold = true;
                    gvSales.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                    foreach (var dr in salesModel)
                    {
                        total = total + dr.total;
                        profit = profit + dr.profit;
                        totalBrand = totalBrand + dr.brandTotal;
                        gvSales.FooterRow.Cells[6].Text = totalBrand.ToString("C");
                        gvSales.FooterRow.Cells[6].Font.Bold = true;
                        gvSales.FooterRow.Cells[5].Text = profit.ToString("C");
                        gvSales.FooterRow.Cells[5].Font.Bold = true;
                        gvSales.FooterRow.Cells[4].Text = total.ToString("C");
                        gvSales.FooterRow.Cells[4].Font.Bold = true;
                        gvSales.FooterRow.Cells[3].Font.Bold = true;
                    }
                }
            }

        }

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Sale> salesList = (List<Sale>)ViewState["Ventas"];
                List<Sale> salesFiltered = new List<Sale>();
                if (salesList != null)
                {
                    if (txDate.Text != string.Empty)
                    {
                        salesFiltered = salesList.Where(x => x.date == DateTime.Parse(txDate.Text)).ToList();
                    }
                    else
                    {
                        salesFiltered = salesList;
                    }

                    gvSales.DataSource = salesFiltered;
                    gvSales.DataBind();

                    decimal total = 0;
                    decimal profit = 0;
                    decimal totalBrand = 0;
                    gvSales.FooterRow.Cells[3].Text = "Total";
                    gvSales.FooterRow.Cells[4].Font.Bold = true;
                    gvSales.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    gvSales.FooterRow.Cells[5].Font.Bold = true;
                    gvSales.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    gvSales.FooterRow.Cells[6].Font.Bold = true;
                    gvSales.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                    foreach (var dr in salesFiltered)
                    {
                        total = total + dr.total;
                        profit = profit + dr.profit;
                        totalBrand = totalBrand + dr.brandTotal;
                        gvSales.FooterRow.Cells[6].Text = totalBrand.ToString("C");
                        gvSales.FooterRow.Cells[6].Font.Bold = true;
                        gvSales.FooterRow.Cells[5].Text = profit.ToString("C");
                        gvSales.FooterRow.Cells[5].Font.Bold = true;
                        gvSales.FooterRow.Cells[4].Text = total.ToString("C");
                        gvSales.FooterRow.Cells[4].Font.Bold = true;
                        gvSales.FooterRow.Cells[3].Font.Bold = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btDownload_Click(object sender, EventArgs e)
        {
            try
            {
                ReportsService.Service rs = new ReportsService.Service();
                rs.Url = "http://190.244.98.245:8053/ReportsService/Service.asmx";
                int idMarca = Convert.ToInt32(Request.QueryString["brand"]);
                int idEvento = Convert.ToInt32(Request.QueryString["id"]);
                var pdf = rs.CR_Sales(idMarca, idEvento);
                if (pdf != null)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", $"attachment; filename=\"CierreVentas{idMarca}{idEvento}\".pdf");
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(pdf);
                    Response.Flush();
                    Response.End();

                    return;
                }
                else
                {
                    lbMsg.Text = "Archivo no encontrado.";
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}