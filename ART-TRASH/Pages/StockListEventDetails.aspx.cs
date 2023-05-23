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
    public partial class StockListEventDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Stock Tiendita Detalle");
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
            EventsDTO evento = ServiceHelper.ws.GetEventByID(id);
            List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(brand).ToList();
            List<StockDTO> stock = ServiceHelper.ws.GetStockByEventID(id).Where(x => x.brand_id == brand).ToList();
            UsersDTO user = (UsersDTO)Session["usuario"];
            if (user.Id != brand && user.roleId == 2)
            {
                Response.Redirect("Events.aspx");
            }

            List<StockModel> stockProducts = new List<StockModel>();
            foreach (var stockItem in stock)
            {
                StockModel product = new StockModel();
                product.Id = stockItem.Id;
                product.serial_number = products.First(x => x.Id == stockItem.product_id).serial_number;
                product.description = products.First(x => x.Id == stockItem.product_id).description;
                product.quantity = stockItem.quantity;

                stockProducts.Add(product);
            }
            lbBrand.Text = ServiceHelper.ws.GetBrandByID(brand).name;
            lbEvent.Text = evento.name;
            ViewState["Stock"] = stockProducts;
            gvStock.DataSource = stockProducts;
            gvStock.DataBind();
        }

        protected void btDownload_Click(object sender, EventArgs e)
        {
            try
            {
                ReportsService.Service rs = new ReportsService.Service();
                rs.Url = "http://190.244.98.245:8053/ReportsService/Service.asmx";
                int idMarca = Convert.ToInt32(Request.QueryString["brand"]);
                int idEvento = Convert.ToInt32(Request.QueryString["id"]);
                var pdf = rs.CR_Stock(idMarca, idEvento);
                if (pdf != null)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", $"attachment; filename=\"CierreStock{idMarca}{idEvento}\".pdf");
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

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<StockModel> stockList = (List<StockModel>)ViewState["Stock"];
                List<StockModel> stockFiltered = new List<StockModel>();
                if (stockList != null)
                {
                    if (txProduct.Text != string.Empty)
                    {
                        stockFiltered = stockList.Where(x => x.description.Contains(txProduct.Text) || x.serial_number.Contains(txProduct.Text)).ToList();
                    }
                    else
                    {
                        stockFiltered = stockList;
                    }

                    gvStock.DataSource = stockFiltered;
                    gvStock.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}