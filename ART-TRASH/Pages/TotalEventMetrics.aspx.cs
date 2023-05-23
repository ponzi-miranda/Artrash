using ArtTrash.Helpers;
using ArtTrash.Models;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class TotalEventMetrics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                UsersDTO user = (UsersDTO)Session["usuario"];
                if (!Page.IsPostBack)
                {
                    if (user.roleId != 2)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void LoadData()
        {
            List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
            if (events != null)
            {
                EventsDTO selectedEvent = (EventsDTO)ViewState["Event"];
                if (selectedEvent == null)
                {
                    selectedEvent = events.Last();
                }

                ddEvents.DataSource = events;
                ddEvents.DataTextField = "name";
                ddEvents.DataValueField = "Id";
                ddEvents.DataBind();
                ddEvents.Items.Add("TODOS LOS EVENTOS");
                ddEvents.Items.FindByText(selectedEvent.name).Selected = true;

                List<MetricsResume> metrics = new List<MetricsResume>();

                decimal totalProfit = 0;
                decimal totalVentas = 0;
                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                {                        
                    List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).ToList();
                    foreach (var detail in salesDetails)
                    {
                        totalVentas += detail.total;
                        totalProfit += detail.profit;
                    }
                }


                MetricsResume metricTotalVentas = new MetricsResume();
                metricTotalVentas.Id = metrics.Count() + 1;
                metricTotalVentas.event_name = selectedEvent.name;
                metricTotalVentas.type_total = "VENTAS";
                metricTotalVentas.total = totalVentas;
                metricTotalVentas.percentage = totalVentas.ToString("C");
                metrics.Add(metricTotalVentas);
                
                MetricsResume metricTotalProfit = new MetricsResume();
                metricTotalProfit.Id = metrics.Count() + 1;
                metricTotalProfit.event_name = selectedEvent.name;
                metricTotalProfit.type_total = "RENTABILIDAD 20%";
                metricTotalProfit.total = totalProfit;
                metricTotalProfit.percentage = totalProfit.ToString("C");
                metrics.Add(metricTotalProfit);

                gvSales.DataSource = metrics;
                gvSales.DataBind();


                Chart1.DataBindCrossTable(metrics, "type_total", "event_name", "total", "Label=percentage");
            }
        }

        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                List<MetricsResume> metrics = new List<MetricsResume>();
                List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                if (events != null)
                {
                    if (txDate1.Text != string.Empty && txDate2.Text != string.Empty)
                    {
                        DateTime dateStart = DateTime.Parse(txDate1.Text);
                        DateTime dateFinish = DateTime.Parse(txDate2.Text);
                        if (dateStart < dateFinish)
                        {
                            List<EventsDTO> selectedEvents = events.Where(x => x.finish_date < dateFinish && x.start_date > dateStart).ToList();
                            foreach (var thisEvent in selectedEvents)
                            {
                                decimal totalProfit = 0;
                                decimal totalVentas = 0;
                                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(thisEvent.Id).ToList();
                                foreach (var sale in sales)
                                {
                                    List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).ToList();
                                    foreach (var detail in salesDetails)
                                    {
                                        totalVentas += detail.total;
                                        totalProfit += detail.profit;
                                    }
                                }

                                MetricsResume metricTotalVentas = new MetricsResume();
                                metricTotalVentas.Id = metrics.Count() + 1;
                                metricTotalVentas.event_name = thisEvent.name;
                                metricTotalVentas.type_total = "VENTAS";
                                metricTotalVentas.total = totalVentas;
                                metricTotalVentas.percentage = totalVentas.ToString("C");
                                metrics.Add(metricTotalVentas);

                                MetricsResume metricTotalProfit = new MetricsResume();
                                metricTotalProfit.Id = metrics.Count() + 1;
                                metricTotalProfit.event_name = thisEvent.name;
                                metricTotalProfit.type_total = "RENTABILIDAD 20%";
                                metricTotalProfit.total = totalProfit;
                                metricTotalProfit.percentage = totalProfit.ToString("C");
                                metrics.Add(metricTotalProfit);
                            }
                            gvSales.DataSource = metrics;
                            gvSales.DataBind();

                            Chart1.DataBindCrossTable(metrics, "type_total", "event_name", "total", "Label=percentage");
                        }
                        else
                        {
                            lbMsg.Text = "La Fecha de Fin no puede ser menor a la fecha de inicio.";
                            return;
                        }
                    }
                    else if (ddEvents.SelectedValue != "TODOS LOS EVENTOS")
                    {
                        ViewState["Event"] = events.First(x => x.Id == Convert.ToInt32(ddEvents.SelectedValue));
                        LoadData();
                    }
                    else
                    {
                        List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptions().ToList();
                        List<decimal> profit = new List<decimal>();
                        List<string> inscripciones = new List<string>();                        

                        foreach (var thisEvent in events)
                        {
                            decimal totalProfit = 0;
                            decimal totalVentas = 0;
                            List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(thisEvent.Id).ToList();
                            foreach (var sale in sales)
                            {
                                List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).ToList();
                                foreach (var detail in salesDetails)
                                {
                                    totalVentas += detail.total;
                                    totalProfit += detail.profit;
                                }
                            }


                            MetricsResume metricTotalVentas = new MetricsResume();
                            metricTotalVentas.Id = metrics.Count() + 1;
                            metricTotalVentas.event_name = thisEvent.name;
                            metricTotalVentas.type_total = "VENTAS";
                            metricTotalVentas.total = totalVentas;
                            metricTotalVentas.percentage = totalVentas.ToString("C");
                            metrics.Add(metricTotalVentas);

                            MetricsResume metricTotalProfit = new MetricsResume();
                            metricTotalProfit.Id = metrics.Count() + 1;
                            metricTotalProfit.event_name = thisEvent.name;
                            metricTotalProfit.type_total = "RENTABILIDAD 20%";
                            metricTotalProfit.total = totalProfit;
                            metricTotalProfit.percentage = totalProfit.ToString("C");
                            metrics.Add(metricTotalProfit);
                        }

                        gvSales.DataSource = metrics;
                        gvSales.DataBind();


                        Chart1.DataBindCrossTable(metrics, "type_total", "event_name", "total", "Label=percentage");
                    }
                }                
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}