using ArtTrash.Helpers;
using ArtTrash.Models;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class BrandProductSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Productos Vendidos");
                UsersDTO user = (UsersDTO)Session["usuario"];
                if (!Page.IsPostBack)
                {
                    DataTable Datos = (DataTable)ViewState["dtDatos"];
                    if (Datos == null)
                    {
                        Datos = new DataTable();

                        //'Año - Mes', 'Cantidad Ventas', 'Total $', 'Rentabilidad $'
                        Datos.Columns.Add(new DataColumn("Eventos", typeof(string)));
                        Datos.Columns.Add(new DataColumn("Cantidad Ventas", typeof(string)));
                        Datos.Columns.Add(new DataColumn("Total $", typeof(string)));
                        Datos.Columns.Add(new DataColumn("Rentabilidad $", typeof(string)));

                        ViewState["dtDatos"] = Datos;
                    }

                    List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                    List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Product"];
                    if (products == null)
                    {
                        products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();
                    }
                    List<EventsDTO> selectedEvent = (List<EventsDTO>)ViewState["Event"];
                    if (selectedEvent == null)
                    {
                        List<InscriptionsDTO> allInscriptions = ServiceHelper.ws.GetInscriptions().ToList();
                        selectedEvent = new List<EventsDTO>();
                        selectedEvent.Add(events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id)));
                    }


                    ddEvents.DataSource = events;
                    ddEvents.DataTextField = "name";
                    ddEvents.DataValueField = "Id";
                    ddEvents.DataBind();
                    ddEvents.Items.Add("TODOS LOS EVENTOS");
                    ddEvents.Items.FindByText(selectedEvent.First().name).Selected = true;

                    if (user.roleId != 1)
                    {
                        //LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ActualizarGrilla()
        {
            DataTable dtDatos = (DataTable)ViewState["dtDatos"];

            gvSales.DataSource = dtDatos;
            gvSales.DataBind();
        }


        protected void LoadData()
        {
            UsersDTO user = (UsersDTO)Session["usuario"];
            List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
            if (events != null)
            {
                List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Product"];
                if (products == null)
                {
                    products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();
                }
                List<EventsDTO> selectedEvents = (List<EventsDTO>)ViewState["Event"];
                if (selectedEvents == null)
                {
                    List<InscriptionsDTO> allInscriptions = ServiceHelper.ws.GetInscriptions().ToList();
                    //selectedEvent = events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id) && x.state == "FINALIZADO");
                    selectedEvents.Add(events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id)));
                }

                List<MetricsResume> metrics = new List<MetricsResume>();
                List<Sales_detailsDTO> sales_Details = ServiceHelper.ws.GetSalesDetails().ToList();
                foreach (var selectedEvent in selectedEvents)
                {
                    int totalQuantity = 0;
                    decimal totalVentas = 0;
                    decimal totalProfit = 0;
                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                    foreach (var sale in sales)
                    {
                        List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == 1).ToList();
                        foreach (var detail in salesDetails.Where(x => x.brand_id == user.Id))
                        {
                            totalVentas += detail.total;
                            totalQuantity += detail.quantity;
                            totalProfit += detail.profit;
                        }
                    }

                    MetricsResume metricTotalVentas = new MetricsResume();
                    metricTotalVentas.Id = metrics.Count() + 1;
                    metricTotalVentas.event_name = selectedEvent.name;
                    metricTotalVentas.total = totalVentas;
                    metricTotalVentas.quantity = totalQuantity;
                    metricTotalVentas.percentage = totalVentas.ToString("C");
                    metricTotalVentas.profit = totalProfit;
                    metrics.Add(metricTotalVentas);
                }

                gvSales.DataSource = metrics;
                gvSales.DataBind();

            }
        }

        protected string obtenerDatos()
        {

            UsersDTO user = (UsersDTO)Session["usuario"];
            List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
            List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Product"];
            if (products == null)
            {
                products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();
            }
            List<EventsDTO> selectedEvents = (List<EventsDTO>)ViewState["Event"];
            if (selectedEvents == null)
            {
                List<InscriptionsDTO> allInscriptions = ServiceHelper.ws.GetInscriptions().ToList();
                //selectedEvent = events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id) && x.state == "FINALIZADO");
                selectedEvents = new List<EventsDTO>();
                selectedEvents.Add(events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id)));
            }

            List<MetricsResume> metrics = new List<MetricsResume>();
            List<Sales_detailsDTO> sales_Details = ServiceHelper.ws.GetSalesDetails().ToList();
            foreach (var selectedEvent in selectedEvents)
            {
                int totalQuantity = 0;
                decimal totalVentas = 0;
                decimal totalProfit = 0;
                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                foreach (var sale in sales)
                {
                    List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == sale.Id).ToList();
                    foreach (var detail in salesDetails.Where(x => x.brand_id == user.Id))
                    {
                        totalVentas += detail.total;
                        totalQuantity += detail.quantity;
                        totalProfit += detail.profit;
                    }
                }

                MetricsResume metricTotalVentas = new MetricsResume();
                metricTotalVentas.Id = metrics.Count() + 1;
                metricTotalVentas.event_name = selectedEvent.name;
                metricTotalVentas.total = totalVentas;
                metricTotalVentas.quantity = totalQuantity;
                metricTotalVentas.percentage = totalVentas.ToString("C");
                metricTotalVentas.profit = totalProfit;
                metrics.Add(metricTotalVentas);
            }

            gvSales.DataSource = metrics;
            gvSales.DataBind();

            DataTable Datos = (DataTable)ViewState["dtDatos"];

            foreach (var r in metrics)
            {
                Datos.Rows.Add(new object[] { r.event_name, r.quantity, r.total, r.profit });
            }


            string strDatos = "[['Eventos', 'Cantidad Ventas', 'Total $', 'Rentabilidad $'],";

            foreach (DataRow dr in Datos.Rows)
            {
                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + dr[0] + "'," + dr[1].ToString() + "," + Decimal.Parse(dr[2].ToString()) / 1000 + "," + Decimal.Parse(dr[3].ToString()) / 1000;
                strDatos = strDatos + "],";
            }

            strDatos = strDatos + "]";
            return strDatos;
        }
        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                List<MetricsResume> metrics = new List<MetricsResume>();
                List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                UsersDTO user = (UsersDTO)Session["usuario"];
                List<Sales_detailsDTO> sales_Details = ServiceHelper.ws.GetSalesDetails().ToList();
                List<EventsDTO> selectedEvents = new List<EventsDTO>();
                if (events != null)
                {
                    if (txDate1.Text != string.Empty && txDate2.Text != string.Empty)
                    {
                        DateTime dateStart = DateTime.Parse(txDate1.Text);
                        DateTime dateFinish = DateTime.Parse(txDate2.Text);
                        if (dateStart < dateFinish)
                        {
                            selectedEvents = events.Where(x => x.finish_date < dateFinish && x.start_date > dateStart).ToList();
                            foreach (var thisEvent in selectedEvents)
                            {
                                    int totalQuantity = 0;
                                    decimal totalVentas = 0;
                                    decimal totalProfit = 0;
                                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(thisEvent.Id).ToList();
                                    foreach (var sale in sales)
                                    {
                                        List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == sale.Id).ToList();
                                        foreach (var detail in salesDetails.Where(x => x.brand_id == user.Id))
                                        {
                                            totalVentas += detail.total;
                                            totalQuantity += detail.quantity;
                                            totalProfit += detail.profit;
                                        }
                                    }

                                    MetricsResume metricTotalVentas = new MetricsResume();
                                    metricTotalVentas.Id = metrics.Count() + 1;
                                    metricTotalVentas.event_name = thisEvent.name;
                                    metricTotalVentas.profit = totalProfit;
                                    metricTotalVentas.total = totalVentas;
                                    metricTotalVentas.quantity = totalQuantity;
                                    metricTotalVentas.percentage = totalVentas.ToString("C");
                                    metrics.Add(metricTotalVentas);
                            }
                            gvSales.DataSource = metrics;
                            gvSales.DataBind();

                            ViewState["Event"] = selectedEvents;
                        }
                        else
                        {
                            lbMsg.Text = "La Fecha de Fin no puede ser menor a la fecha de inicio.";
                            return;
                        }
                    }
                    else if (ddEvents.SelectedValue != "TODOS LOS EVENTOS")
                    {
                        selectedEvents.Add(events.First(x => x.Id == Convert.ToInt32(ddEvents.SelectedValue)));
                    }
                    else
                    {
                        selectedEvents = events;
                    }                 
                }

                ViewState["Event"] = selectedEvents;


                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "drawChart()", true);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}