using ArtTrash.Helpers;
using ArtTrash.Models;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class PercentageProfitMetrics : System.Web.UI.Page
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

                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(selectedEvent.Id).ToList();
                List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                List<MetricsResume> metrics = new List<MetricsResume>();
                foreach (var inscription in inscriptions)
                {
                    decimal total = 0;
                    decimal profitTotal = 0;
                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                    foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                    {
                        List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == inscription.brand_id).ToList();
                        foreach (var detail in salesDetails)
                        {
                            total += detail.total;
                            profitTotal += detail.profit;
                        }                        
                    }

                    MetricsResume metric = new MetricsResume();
                    metric.Id = metrics.Count() + 1;
                    metric.event_name = selectedEvent.name;
                    metric.brand_name = brands.First(x => x.Id == inscription.brand_id).name;
                    metric.total = total;
                    metric.profit = profitTotal;
                    metric.percentage = profitTotal.ToString("C");
                    metrics.Add(metric);
                }
                gvProfit.DataSource = metrics;
                gvProfit.DataBind();

                Chart1.DataBindCrossTable(metrics, "brand_name", "event_name", "profit", "Label=percentage");

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

                            List<decimal> profit = new List<decimal>();
                            List<string> inscripciones = new List<string>();
                            foreach (var selectedEvent in selectedEvents)
                            {
                                foreach (var brand in brands)
                                {
                                    decimal total = 0;
                                    decimal profitTotal = 0;
                                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();//inscription.event_id).ToList();
                                    foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                                    {
                                        List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == brand.Id).ToList();//inscription.brand_id).ToList();
                                        foreach (var detail in salesDetails)
                                        {
                                            total += detail.total;
                                            profitTotal += detail.profit;
                                        }
                                    }

                                    MetricsResume metric = new MetricsResume();
                                    metric.Id = metrics.Count() + 1;
                                    metric.event_name = selectedEvent.name;//events.First(x => x.Id == inscription.event_id).name;
                                    metric.brand_name = brand.name;//brands.First(x => x.Id == inscription.brand_id).name;
                                    metric.total = total;
                                    metric.profit = profitTotal;
                                    metric.percentage = profitTotal.ToString("C");
                                    metrics.Add(metric);
                                }
                            }
                            gvProfit.DataSource = metrics;
                            gvProfit.DataBind();

                            Chart1.DataBindCrossTable(metrics, "brand_name", "event_name", "profit", "Label=percentage");
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
                        MetricsResume metri1c = new MetricsResume();
                       
                        foreach (var thisEvent in events)
                        {
                            foreach (var brand in brands)
                            {
                                decimal total = 0;
                                decimal profitTotal = 0;
                                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(thisEvent.Id).ToList();//inscription.event_id).ToList();
                                foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                                {
                                    List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == brand.Id).ToList();//inscription.brand_id).ToList();
                                    foreach (var detail in salesDetails)
                                    {
                                        total += detail.total;
                                        profitTotal += detail.profit;
                                    }
                                }

                                MetricsResume metric = new MetricsResume();
                                metric.Id = metrics.Count() + 1;
                                metric.event_name = thisEvent.name;//events.First(x => x.Id == inscription.event_id).name;
                                metric.brand_name = brand.name;//brands.First(x => x.Id == inscription.brand_id).name;
                                metric.total = total;
                                metric.profit = profitTotal;
                                metric.percentage = profitTotal.ToString("C");
                                metrics.Add(metric);
                            }                            
                        }
                      


                       

                        Chart1.DataBindCrossTable(metrics, "brand_name", "event_name", "profit", "Label=percentage");

                        gvProfit.DataSource = metrics;
                        gvProfit.DataBind();
                        //Chart1.DataBindCrossTable(metrics, "event_name", "brand_name", "profit", "Label=profit");
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