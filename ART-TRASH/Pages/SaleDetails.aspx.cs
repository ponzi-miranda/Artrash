using ArtTrash.Helpers;
using ArtTrash.Models;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class SaleDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Detalle Venta");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        ddFormaPago.DataSource = ServiceHelper.ws.GetPayments();
                        ddFormaPago.DataTextField = "description";
                        ddFormaPago.DataValueField = "Id";
                        ddFormaPago.DataBind();
                        ddFormaPago.Attributes.Add("disabled", "true");

                        ddFormaPago1.DataSource = ServiceHelper.ws.GetPayments();
                        ddFormaPago1.DataTextField = "description";
                        ddFormaPago1.DataValueField = "Id";
                        ddFormaPago1.DataBind();
                        ddFormaPago1.Attributes.Add("disabled", "true");

                        ddFormaPago2.DataSource = ServiceHelper.ws.GetPayments();
                        ddFormaPago2.DataTextField = "description";
                        ddFormaPago2.DataValueField = "Id";
                        ddFormaPago2.DataBind();
                        ddFormaPago2.Attributes.Add("disabled", "true");

                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        Response.Redirect("/Pages/Sales.aspx");
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
            SalesDTO sale = ServiceHelper.ws.GetSaleByID(id);
            if (sale != null)
            {
                if (sale.state == "CANCELADA")
                {
                    btEliminar.Enabled = false;
                }
                lbDetail.Text = sale.Id + " - " + sale.state;
                txEvento.Text = ServiceHelper.ws.GetEventByID(sale.event_id).name;
                txFecha.Text = sale.date.ToString("yyyy-MM-dd");
                //rbPaymentList.Items.FindByValue(sale.payment_method_id.ToString()).Selected = true;
                ddFormaPago.SelectedValue = sale.payment_method_id.ToString();
                txMonto.Text = sale.total.ToString();

                if (sale.payment_method_id == 4)
                {
                    plMixta.Visible = true;

                    List<Sales_PaymentsDTO> formasPago = ServiceHelper.ws.GetSalePaymentsBySaleID(sale.Id).ToList();

                    ddFormaPago1.SelectedValue = formasPago.First().IdPayments.ToString();
                    txMonto1.Text = formasPago.First().amount.ToString();
                    ddFormaPago2.SelectedValue = formasPago.Last().IdPayments.ToString();
                    txMonto2.Text = formasPago.Last().amount.ToString();
                }


                List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(id).ToList();
                List<Sale> sales = new List<Sale>();
                foreach (var saleDetail in salesDetails)
                {
                    Sale saleProduct = new Sale();
                    ProductsDTO product = ServiceHelper.ws.GetProductByID(saleDetail.product_id);

                    saleProduct.Id = saleDetail.Id;
                    saleProduct.brand_name = ServiceHelper.ws.GetBrandByID(saleDetail.brand_id).name;
                    saleProduct.description = product.description;
                    saleProduct.price = product.price;
                    saleProduct.payment_method = ServiceHelper.ws.GetProductTypes().First(x => x.Id == product.product_type_id).description;
                    saleProduct.quantity = saleDetail.quantity;
                    saleProduct.total = saleDetail.total;

                    sales.Add(saleProduct);
                }

                gvProductos.DataSource = sales;
                gvProductos.DataBind();

                if (sales != null)
                {
                    if (sales.Count > 0)
                    {
                        decimal total = 0;
                        gvProductos.FooterRow.Cells[5].Text = "Total";
                        gvProductos.FooterRow.Cells[6].Font.Bold = true;
                        gvProductos.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                        foreach (var dr in sales)
                        {
                            total = total + dr.total;
                            gvProductos.FooterRow.Cells[6].Text = total.ToString("C");
                            gvProductos.FooterRow.Cells[6].Font.Bold = true;
                            gvProductos.FooterRow.Cells[5].Font.Bold = true;
                        }
                    }
                }

            }
        }

        protected void btEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                SalesDTO sale = ServiceHelper.ws.GetSaleByID(Convert.ToInt32(Request.QueryString["id"]));
                if (sale != null)
                {
                    if (sale.date == DateTime.Today)
                    {
                        sale.state = "CANCELADA";

                        List<Sales_detailsDTO> saleDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).ToList();
                        foreach (var detail in saleDetails)
                        {
                            StockDTO stock = ServiceHelper.ws.GetStockByProductID(detail.product_id).Last();

                            stock.quantity = stock.quantity + detail.quantity;

                            ServiceHelper.ws.SetStock(stock);
                        }

                        ServiceHelper.ws.SetSale(sale);
                        lbMsg.Text = "Venta cancelada, esta acción no podrá ser revertida.";
                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        lbMsg.Text = "Solo puede eliminar ventas en el mismo día que fueron creadas.";
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                ReportsService.Service rs = new ReportsService.Service();
                rs.Url = "http://190.244.98.245:8053/ReportsService/Service.asmx";
                int idSale = Convert.ToInt32(Request.QueryString["id"]);                
                var pdf = rs.CR_Venta(idSale);
                if (pdf != null)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", $"attachment; filename=\"ComprobanteVentaNro{idSale}\".pdf");
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