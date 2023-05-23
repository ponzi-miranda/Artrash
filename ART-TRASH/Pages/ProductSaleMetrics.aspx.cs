using ArtTrash.Helpers;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class ProductSaleMetrics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                UsersDTO user = (UsersDTO)Session["usuario"];
                if (!Page.IsPostBack)
                {
                    if (user.roleId != 1)
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
                UsersDTO user = (UsersDTO)Session["usuario"];

                var isInscripted = inscriptions.Where(x => x.event_id == selectedEvent.Id).ToList();

                if (isInscripted.First(x => x.brand_id == user.Id) != null) //is inscripted
                {
                    List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();
                    List<double> ventas = new List<double>();
                    List<string> productos = new List<string>();
                    foreach (var product in products)
                    {
                        int cantVentas = 0;
                        List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                        foreach (var sale in sales)
                        {
                            List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == user.Id && x.product_id == product.Id).ToList();
                            foreach (var detail in salesDetails)
                            {
                                cantVentas += detail.quantity;
                            }
                        }
                        ventas.Add(cantVentas);
                        productos.Add(product.description);
                    }
                    double[] cantidadVentas = ventas.ToArray();
                    string[] productosList = productos.ToArray();
                    Chart1.Series["Default"].Points.DataBindXY(productosList, cantidadVentas);
                    Chart1.Series["Default"].ChartType = SeriesChartType.Pie;
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
                    lbMsg.Text = "No estuvo inscripto en el evento seleccionado.";
                }
            }
        }

        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
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




                            //List<double> ventas = new List<double>();
                            //List<string> inscripciones = new List<string>();
                            //foreach (var selectedEvent in selectedEvents)
                            //{
                            //    List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(selectedEvent.Id).ToList();
                            //    foreach (var inscription in inscriptions)
                            //    {
                            //        int cantVentas = 0;
                            //        List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsByBrandID(inscription.brand_id).ToList();
                            //        foreach (var detail in salesDetails)
                            //        {
                            //            cantVentas += detail.quantity;
                            //        }

                            //        if (!inscripciones.Contains(brands.First(x => x.Id == inscription.brand_id).name))
                            //        {
                            //            ventas.Add(cantVentas);
                            //            inscripciones.Add(brands.First(x => x.Id == inscription.brand_id).name);
                            //        }
                            //    }
                            //}
                            //double[] cantidadVentas = ventas.ToArray();
                            //string[] marcas = inscripciones.ToArray();
                            //Chart1.Series["Default"].Points.DataBindXY(marcas, cantidadVentas);
                            //Chart1.Series["Default"].ChartType = SeriesChartType.Doughnut;
                            //Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
                            //Chart1.Series["Default"].Label = "#PERCENT{P2}";
                            //Chart1.Series["Default"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";
                            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                            //Chart1.Legends[0].Enabled = true;
                            //Chart1.Legends[0].LegendStyle = LegendStyle.Column;
                            //Chart1.Legends[0].Docking = Docking.Right;
                            //Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
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
                        UsersDTO user = (UsersDTO)Session["usuario"];
                        List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptions().Where(x => x.brand_id == user.Id).ToList();
                        List<double> ventas = new List<double>();
                        List<string> productos = new List<string>();
                        foreach (var inscription in inscriptions)
                        {
                            List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();                          
                            foreach (var product in products)
                            {
                                int cantVentas = 0;
                                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(inscription.event_id).ToList();
                                foreach (var sale in sales)
                                {
                                    List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == user.Id && x.product_id == product.Id).ToList();
                                    foreach (var detail in salesDetails)
                                    {
                                        cantVentas += detail.quantity;
                                    }
                                }
                                if (!productos.Contains(product.description))
                                {
                                    ventas.Add(cantVentas);
                                    productos.Add(product.description);
                                }
                                
                            }                           
                        }                 

                        double[] cantidadVentas = ventas.ToArray();
                        string[] productosList = productos.ToArray();
                        Chart1.Series["Default"].Points.DataBindXY(productosList, cantidadVentas);
                        Chart1.Series["Default"].ChartType = SeriesChartType.Pie;
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