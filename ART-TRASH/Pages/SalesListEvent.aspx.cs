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
    public partial class SalesListEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Ventas Tiendita");
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        Response.Redirect("Sales.aspx");
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
                List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                List<Sales_detailsDTO> salesDetails = new List<Sales_detailsDTO>();
                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(id).Where(x => x.state == "CONFIRMADA").ToList();
                List<Sale> salesSumary = new List<Sale>();

                foreach (var inscription in inscriptions)
                {
                    decimal total = 0;
                    decimal profit = 0;
                    salesDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(inscription.brand_id).ToList();
                    foreach (var saleDetail in salesDetails)
                    {
                        if (sales.Exists(x => x.Id == saleDetail.sale_id))
                        {
                            total += saleDetail.total;
                            profit += saleDetail.profit;
                        }
                    }

                    Sale sale = new Sale();

                    sale.Id = brands.First(x => x.Id == inscription.brand_id).Id;
                    sale.brand_name = brands.First(x => x.Id == inscription.brand_id).name;
                    sale.total = total;
                    sale.profit = profit;
                    sale.brandTotal = total - profit;
                    salesSumary.Add(sale);
                }
                salesSumary.Reverse();

                

                ViewState["SalesSumary"] = salesSumary;
                ViewState["Sales"] = sales;

                ReloadGrid();
            }
        }

        protected void ReloadGrid() 
        {
            try
            {

                List<Sale> salesSumary = (List<Sale>)ViewState["SalesSumary"];
                List<SalesDTO> sales = (List<SalesDTO>)ViewState["Sales"];


                gvStock.DataSource = salesSumary;
                gvStock.DataBind();

                decimal caja = 0;
                decimal transferencia = 0;
                decimal mercadopago = 0;
                decimal totalSum = 0;

                List<PaymentsDTO> formasPagos = ServiceHelper.ws.GetPayments().ToList();
                List<Sales_PaymentsDTO> mixtas = ServiceHelper.ws.GetSalePayments().ToList(); //todo validate state
                foreach (var venta in sales)
                {

                    totalSum += venta.total;
                    if (formasPagos.Any(x => x.Id == venta.payment_method_id)) 
                    {
                        PaymentsDTO formaPago = formasPagos.FirstOrDefault(x => x.Id == venta.payment_method_id);

                        switch (formaPago.description)
                        {
                            case "Efectivo":
                                caja += venta.total;
                                break;
                            case "Transferencia":
                                transferencia += venta.total;
                                break;
                            case "Mercado Pago":
                                mercadopago += venta.total;
                                break;
                            case "Mixta":
                                List<Sales_PaymentsDTO> ventasFormasPagos = mixtas.Where(x => x.IdSale == venta.Id).ToList();

                                foreach (var formaPagoVenta in ventasFormasPagos)
                                {
                                    formaPago = formasPagos.First(x => x.Id == formaPagoVenta.IdPayments);
                                    switch (formaPago.description)
                                    {
                                        case "Efectivo":
                                            caja += formaPagoVenta.amount;
                                            break;
                                        case "Transferencia":
                                            transferencia += formaPagoVenta.amount;
                                            break;
                                        case "Mercado Pago":
                                            mercadopago += formaPagoVenta.amount;
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }

                lbCaja.Text = "$" + caja.ToString();
                lbMercadoPago.Text = "$" + mercadopago.ToString();
                lbTransferencia.Text = "$" + transferencia.ToString();


                if (salesSumary != null)
                {
                    if (salesSumary.Count > 0)
                    {
                        decimal total = 0;
                        decimal profit = 0;
                        decimal totalBrand = 0;
                        gvStock.FooterRow.Cells[2].Text = "Total";
                        gvStock.FooterRow.Cells[3].Font.Bold = true;
                        gvStock.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        gvStock.FooterRow.Cells[4].Font.Bold = true;
                        gvStock.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        gvStock.FooterRow.Cells[5].Font.Bold = true;
                        gvStock.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                        foreach (var dr in salesSumary)
                        {
                            total = total + dr.total;
                            profit = profit + dr.profit;
                            totalBrand = totalBrand + dr.brandTotal;
                            gvStock.FooterRow.Cells[5].Text = totalBrand.ToString("C");
                            gvStock.FooterRow.Cells[5].Font.Bold = true;
                            gvStock.FooterRow.Cells[5].Font.Bold = true;
                            gvStock.FooterRow.Cells[4].Text = profit.ToString("C");
                            gvStock.FooterRow.Cells[4].Font.Bold = true;
                            gvStock.FooterRow.Cells[3].Text = total.ToString("C");
                            gvStock.FooterRow.Cells[3].Font.Bold = true;
                            gvStock.FooterRow.Cells[2].Font.Bold = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvStock.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                if (e.CommandName == "Detalles")
                {
                    Response.Redirect("/Pages/SalesListEventDetails.aspx?id=" + Convert.ToInt32(Request.QueryString["id"]) + "&brand=" + id.ToString());
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
                if (string.IsNullOrWhiteSpace(txDate.Text))
                {
                    LoadData(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    LoadData(Convert.ToInt32(Request.QueryString["id"]));

                    List<SalesDTO> sales = (List<SalesDTO>)ViewState["Sales"];

                    sales = sales.Where(x => x.date == DateTime.Parse(txDate.Text, System.Globalization.CultureInfo.InvariantCulture)).ToList();

                    List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(Convert.ToInt32(Request.QueryString["id"])).ToList();
                    List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                    List<Sales_detailsDTO> salesDetails = new List<Sales_detailsDTO>();
                    List<Sales_detailsDTO> salesDetailsFiltered = new List<Sales_detailsDTO>();
                    List<Sale> salesSumary = new List<Sale>();

                    foreach (var inscription in inscriptions)
                    {
                        decimal total = 0;
                        decimal profit = 0;
                        salesDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(inscription.brand_id).ToList();

                        foreach (var saleDetail in salesDetails)
                        {
                            if (sales.Any(x => x.Id == saleDetail.sale_id))
                            {
                                salesDetailsFiltered.Add(saleDetail);
                            }
                        }

                        foreach (var saleDetail in salesDetailsFiltered)
                        {
                            if (sales.Exists(x => x.Id == saleDetail.sale_id))
                            {
                                total += saleDetail.total;
                                profit += saleDetail.profit;
                            }
                        }

                        Sale sale = new Sale();

                        sale.Id = brands.First(x => x.Id == inscription.brand_id).Id;
                        sale.brand_name = brands.First(x => x.Id == inscription.brand_id).name;
                        sale.total = total;
                        sale.profit = profit;
                        sale.brandTotal = total - profit;
                        salesSumary.Add(sale);
                    }


                    ViewState["SalesSumary"] = salesSumary;
                    ViewState["Sales"] = sales;

                    ReloadGrid();
                }                
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}