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
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Ventas");
                if (!Page.IsPostBack)
                {
                    var user = (UsersDTO)Session["usuario"];


                    List<SalesDTO> sales = ServiceHelper.ws.GetSales().ToList();
                    List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                    List<ProductsDTO> products = ServiceHelper.ws.GetProducts().ToList();
                    List<PaymentsDTO> payments = ServiceHelper.ws.GetPayments().ToList();

                    ddEvento.DataSource = events;
                    ddEvento.DataTextField = "name";
                    ddEvento.DataValueField = "name";
                    ddEvento.DataBind();
                    ddEvento.Items.Add("TODOS LOS EVENTOS");
                    ddEvento.Items.FindByText("TODOS LOS EVENTOS").Selected = true;

                    ddEventoBrand.DataSource = ServiceHelper.ws.GetEvents();
                    ddEventoBrand.DataTextField = "name";
                    ddEventoBrand.DataValueField = "name";
                    ddEventoBrand.DataBind();
                    ddEventoBrand.Items.Add("TODOS LOS EVENTOS");
                    ddEventoBrand.Items.FindByText("TODOS LOS EVENTOS").Selected = true;

                    ddFormaPago.DataSource = payments;
                    ddFormaPago.DataTextField = "description";
                    ddFormaPago.DataValueField = "description";
                    ddFormaPago.DataBind();
                    ddFormaPago.Items.Add("TODAS");
                    ddFormaPago.Items.FindByText("TODAS").Selected = true;

                    if (user.roleId == 1)
                    {
                        plAdmin.Visible = true;
                        plBrand.Visible = false;
                        List<Sale> salesModel = new List<Sale>();
                        foreach (var sale in sales)
                        {
                      
                            Sale saleModel = new Sale();

                            saleModel.Id = sale.Id;
                            saleModel.date = sale.date;
                            saleModel.event_name = events.First(x => x.Id == sale.event_id).name;
                            saleModel.payment_method = payments.FirstOrDefault(x => x.Id == sale.payment_method_id).description;
                            saleModel.total = sale.total;
                            saleModel.profit = sale.profit;
                            saleModel.state = sale.state;

                            salesModel.Add(saleModel);
                        }
                        salesModel.Reverse();
                        ViewState["Ventas"] = salesModel;
                        gvVentas.DataSource = ViewState["Ventas"];
                        gvVentas.DataBind();



                        decimal caja = 0;
                        decimal transferencia = 0;
                        decimal mercadopago = 0;
                        decimal totalSum = 0;

                        List<Sales_PaymentsDTO> mixtas = ServiceHelper.ws.GetSalePayments().ToList(); //todo validate state
                        foreach (var venta in sales)
                        {

                            totalSum += venta.total;
                            PaymentsDTO formaPago = payments.First(x => x.Id == venta.payment_method_id);

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
                                        formaPago = payments.First(x => x.Id == formaPagoVenta.IdPayments);
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

                        lbCaja.Text = "$" + caja.ToString();
                        lbMercadoPago.Text = "$" + mercadopago.ToString();
                        lbTransferencia.Text = "$" + transferencia.ToString();
                        lbTotal.Text = "$" + totalSum.ToString();

                    }
                    else
                    {
                        plAdmin.Visible = false;
                        plBrand.Visible = true;

                        ddProductos.DataSource = ServiceHelper.ws.GetProductsByBrandID(user.Id);
                        ddProductos.DataTextField = "description";
                        ddProductos.DataValueField = "description";
                        ddProductos.DataBind();
                        ddProductos.Items.Add("TODOS");
                        ddProductos.Items.FindByText("TODOS").Selected = true;

                        List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(user.Id).ToList();
                        List<Sale> salesModel = new List<Sale>();         
                        foreach (var sale in sales)
                        {
                            var salesDetailsEvent = salesDetails.Where(x => x.sale_id == sale.Id).ToList();

                            if (salesDetailsEvent != null)
                            {
                                foreach (var saleDetailEvent in salesDetailsEvent)
                                {
                                    Sale saleEvent = new Sale();

                                    saleEvent.Id = salesModel.Count() + 1;
                                    saleEvent.date = sale.date;
                                    saleEvent.event_name = events.First(x => x.Id == sale.event_id).name;
                                    saleEvent.total = saleDetailEvent.total;
                                    saleEvent.profit = saleDetailEvent.profit;
                                    saleEvent.price = saleEvent.total - saleEvent.profit;
                                    saleEvent.quantity = saleDetailEvent.quantity;
                                    saleEvent.description = products.First(x => x.Id == saleDetailEvent.product_id).description;

                                    salesModel.Add(saleEvent);
                                }
                            }
                        }

                        salesModel.Reverse();
                        ViewState["VentasMarcas"] = salesModel;
                        gvVentasBrands.DataSource = ViewState["VentasMarcas"];
                        gvVentasBrands.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btNewSale_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NewSale.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvVentas.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
            
                if (e.CommandName == "Detalles")
                {
                        Response.Redirect("/Pages/SaleDetails.aspx?id=" + id.ToString());
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<PaymentsDTO> payments = ServiceHelper.ws.GetPayments().ToList();
                List<Sale> salesList = (List<Sale>)ViewState["Ventas"];
                List<Sale> salesFiltered = new List<Sale>();
                if (salesList != null)
                {
                    if (ddEvento.SelectedValue != "TODOS LOS EVENTOS")
                    {
                        if (salesFiltered.Count() > 0)
                        {
                            salesFiltered = salesFiltered.Where(x => x.event_name == ddEvento.SelectedValue).ToList();
                        }
                        else
                        {
                            salesFiltered = salesList.Where(x => x.event_name == ddEvento.SelectedValue).ToList();
                        }
                    }
                    if (txFecha.Text != string.Empty)
                    {
                        if (salesFiltered.Count() > 0)
                        {
                            salesFiltered = salesFiltered.Where(x => x.date == DateTime.Parse(txFecha.Text)).ToList();
                        }
                        else
                        {
                            salesFiltered = salesList.Where(x => x.date == DateTime.Parse(txFecha.Text)).ToList();
                        }
                    }
                    if (ddFormaPago.SelectedValue != "TODAS")
                    {
                        if (salesFiltered.Count() > 0)
                        {
                            salesFiltered = salesFiltered.Where(x => x.payment_method == ddFormaPago.SelectedValue).ToList();
                        }
                        else
                        {
                            salesFiltered = salesList.Where(x => x.payment_method == ddFormaPago.SelectedValue).ToList();
                        }
                    }   
                    if (ddEstado.SelectedValue != "TODOS")
                    {
                        if (salesFiltered.Count() > 0)
                        {
                            salesFiltered = salesFiltered.Where(x => x.state == ddEstado.SelectedValue).ToList();
                        }
                        else
                        {
                            salesFiltered = salesList.Where(x => x.state == ddEstado.SelectedValue).ToList();
                        }
                    }
                    if (ddFormaPago.SelectedValue == "TODAS" && ddEstado.SelectedValue == "TODOS" && txFecha.Text == string.Empty && ddEvento.SelectedValue == "TODOS LOS EVENTOS")
                    {
                        salesFiltered = salesList;
                    }   

                    gvVentas.DataSource = salesFiltered;
                    gvVentas.DataBind();


                    decimal caja = 0;
                    decimal transferencia = 0;
                    decimal mercadopago = 0;
                    decimal totalSum = 0;

                    List<Sales_PaymentsDTO> mixtas = ServiceHelper.ws.GetSalePayments().ToList(); //todo validate state
                    foreach (var venta in salesFiltered)
                    {

                        totalSum += venta.total;
                        PaymentsDTO formaPago = payments.First(x => x.description == venta.payment_method);

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
                                    formaPago = payments.First(x => x.Id == formaPagoVenta.IdPayments);
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

                    lbCaja.Text = "$" + caja.ToString();
                    lbMercadoPago.Text = "$" + mercadopago.ToString();
                    lbTransferencia.Text = "$" + transferencia.ToString();
                    lbTotal.Text = "$" + totalSum.ToString();


                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btBuscarBrand_Click(object sender, EventArgs e)
        {
            try
            {
                List<Sale> salesList = (List<Sale>)ViewState["VentasMarcas"];
                List<Sale> salesFiltered = new List<Sale>();
                if (salesList != null)
                {
                    if (ddEventoBrand.SelectedValue != "TODOS LOS EVENTOS")
                    {
                        if (salesFiltered.Count() > 0)
                        {
                            salesFiltered = salesFiltered.Where(x => x.event_name == ddEventoBrand.SelectedValue).ToList();
                        }
                        else
                        {
                            salesFiltered = salesList.Where(x => x.event_name == ddEventoBrand.SelectedValue).ToList();
                        }
                    }
                    if (txFechaBrand.Text != string.Empty)
                    {
                        if (salesFiltered.Count() > 0)
                        {
                            salesFiltered = salesFiltered.Where(x => x.date == DateTime.Parse(txFechaBrand.Text)).ToList();
                        }
                        else
                        {
                            salesFiltered = salesList.Where(x => x.date == DateTime.Parse(txFechaBrand.Text)).ToList();
                        }
                    }
                    if (ddProductos.SelectedValue != "TODOS")
                    {
                        if (salesFiltered.Count() > 0)
                        {
                            salesFiltered = salesFiltered.Where(x => x.description == ddProductos.SelectedValue).ToList();
                        }
                        else
                        {
                            salesFiltered = salesList.Where(x => x.description == ddProductos.SelectedValue).ToList();
                        }
                    }                   
                    if (ddProductos.SelectedValue == "TODOS" && txFechaBrand.Text == string.Empty && ddEventoBrand.SelectedValue == "TODOS LOS EVENTOS")
                    {
                        salesFiltered = salesList;
                    }

                    gvVentasBrands.DataSource = salesFiltered;
                    gvVentasBrands.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}