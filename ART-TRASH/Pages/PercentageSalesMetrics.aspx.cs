using ArtTrash.Helpers;
using ArtTrash.Models;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{

    public partial class MetricsDetails : System.Web.UI.Page
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

                List<double> ventas = new List<double>();
                List<string> inscripciones = new List<string>();
                List<MetricsResume> resume = new List<MetricsResume>();

                foreach (var inscription in inscriptions)
                {
                    int cantVentas = 0;
                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                    foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                    {
                        List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == inscription.brand_id).ToList();
                        foreach (var detail in salesDetails)
                        {
                            cantVentas += detail.quantity;
                        }
                    }

                    MetricsResume resumeItem = new MetricsResume();
                    resumeItem.brand_name = brands.First(x => x.Id == inscription.brand_id).name;
                    resumeItem.event_name = selectedEvent.name;
                    resumeItem.sales = cantVentas;
                    resumeItem.Id = resume.Count() + 1;
                    resume.Add(resumeItem);

                    ventas.Add(cantVentas);
                    inscripciones.Add(brands.First(x => x.Id == inscription.brand_id).name);
                }

                var totalVentas = ventas.Sum();
                foreach (var r in resume)
                {
                    r.percentage = (100 * r.sales / totalVentas).ToString("N1");
                }
                gvSales.DataSource = resume;
                gvSales.DataBind();

                double[] cantidadVentas = ventas.ToArray();
                string[] marcas = inscripciones.ToArray();
                Chart1.Series["Default"].Points.DataBindXY(marcas, cantidadVentas);
                Chart1.Series["Default"].ChartType = SeriesChartType.Doughnut;
                Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
                Chart1.Series["Default"].Label = "#PERCENT{P2}";
                Chart1.Series["Default"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].LegendStyle = LegendStyle.Column;
                Chart1.Legends[0].Docking = Docking.Right;
                Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                gvSales.DataSource = resume;
                gvSales.DataBind();
            }
        }

        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                List<MetricsResume> resume = new List<MetricsResume>();
                if (events != null)
                {
                    if (txDate1.Text != string.Empty && txDate2.Text != string.Empty)
                    {
                        DateTime dateStart = DateTime.Parse(txDate1.Text);
                        DateTime dateFinish = DateTime.Parse(txDate2.Text);
                        if (dateStart < dateFinish)
                        {
                            List<EventsDTO> selectedEvents = events.Where(x => x.finish_date < dateFinish && x.start_date > dateStart).ToList();

                            List<double> ventas = new List<double>();
                            List<string> inscripciones = new List<string>();
                            foreach (var selectedEvent in selectedEvents)
                            {
                                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(selectedEvent.Id).ToList();
                                foreach (var inscription in inscriptions)
                                {
                                    int cantVentas = 0;
                                    List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(inscription.brand_id).ToList();
                                    foreach (var detail in salesDetails)
                                    {
                                        cantVentas += detail.quantity;
                                    }

                                    if (!inscripciones.Contains(brands.First(o => o.Id == inscription.brand_id).name))
                                    {
                                        ventas.Add(cantVentas);
                                        inscripciones.Add(brands.First(p => p.Id == inscription.brand_id).name);
                                    }
                                }

                                foreach (var inscription in inscriptions)
                                {
                                    int cantVentas = 0;
                                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                                    foreach (var sale in sales)
                                    {
                                        List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == inscription.brand_id).ToList();
                                        foreach (var detail in salesDetails)
                                        {
                                            cantVentas += detail.quantity;
                                        }
                                    }

                                    MetricsResume resumeItem = new MetricsResume();
                                    resumeItem.brand_name = brands.First(x => x.Id == inscription.brand_id).name;
                                    resumeItem.event_name = selectedEvent.name;
                                    resumeItem.sales = cantVentas;
                                    resumeItem.Id = resume.Count() + 1;
                                    resume.Add(resumeItem);
                                }
                            }

                            var totalVentas = ventas.Sum();
                            foreach (var r in resume)
                            {
                                r.percentage = (100 * r.sales / totalVentas).ToString("N1");
                            }
                            gvSales.DataSource = resume;
                            gvSales.DataBind();


                            double[] cantidadVentas = ventas.ToArray();
                            string[] marcas = inscripciones.ToArray();
                            Chart1.Series["Default"].Points.DataBindXY(marcas, cantidadVentas);
                            Chart1.Series["Default"].ChartType = SeriesChartType.Doughnut;
                            Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
                            Chart1.Series["Default"].Label = "#PERCENT{P2}";
                            Chart1.Series["Default"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";
                            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                            Chart1.Legends[0].Enabled = true;
                            Chart1.Legends[0].LegendStyle = LegendStyle.Column;
                            Chart1.Legends[0].Docking = Docking.Right;
                            Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
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
                        List<double> ventas = new List<double>();
                        List<string> inscripciones = new List<string>();

                        foreach (var inscription in inscriptions)
                        {
                            int cantVentas = 0;
                            List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(inscription.brand_id).ToList();
                            foreach (var detail in salesDetails)
                            {
                                cantVentas += detail.quantity;
                            }

                            int ventasEvento = 0;
                            List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(inscription.event_id).ToList();
                            foreach (var sale in sales)
                            {
                                List<Sales_detailsDTO> details = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).ToList();
                                ventasEvento += details.Where(x => x.brand_id == inscription.brand_id).Sum(x => x.quantity);
                            }

                            MetricsResume resumeItem = new MetricsResume();
                            resumeItem.brand_name = brands.First(y => y.Id == inscription.brand_id).name;
                            resumeItem.event_name = ServiceHelper.ws.GetEventByID(inscription.event_id).name;
                            resumeItem.sales = ventasEvento;
                            resumeItem.Id = resume.Count() + 1;
                            resume.Add(resumeItem);

                            if (!inscripciones.Contains(brands.First(x => x.Id == inscription.brand_id).name))
                            {
                                ventas.Add(cantVentas);
                                inscripciones.Add(brands.First(x => x.Id == inscription.brand_id).name);
                            }
                        }

                        var totalVentas = ventas.Sum();
                        foreach (var r in resume)
                        {
                            r.percentage = (100 * r.sales / totalVentas).ToString("N1");
                        }
                        gvSales.DataSource = resume;
                        gvSales.DataBind();

                        double[] cantidadVentas = ventas.ToArray();
                        string[] marcas = inscripciones.ToArray();
                        Chart1.Series["Default"].Points.DataBindXY(marcas, cantidadVentas);
                        Chart1.Series["Default"].ChartType = SeriesChartType.Doughnut;
                        Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
                        Chart1.Series["Default"].Label = "#PERCENT{P2}";
                        Chart1.Series["Default"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";
                        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                        Chart1.Legends[0].Enabled = true;
                        Chart1.Legends[0].LegendStyle = LegendStyle.Column;
                        Chart1.Legends[0].Docking = Docking.Right;
                        Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
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