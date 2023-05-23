using ArtTrash.Helpers;
using ArtTrash.Models;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class PercentageProdutTypeSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Tipo de Productos Vendidos");
                if (!Page.IsPostBack)
                {
                    DataTable Datos = (DataTable)ViewState["dtDatos"];
                    if (Datos == null)
                    {
                        Datos = new DataTable();

                        Datos.Columns.Add(new DataColumn("Productos", typeof(string)));
                        Datos.Columns.Add(new DataColumn("Cant", typeof(string)));

                        ViewState["dtDatos"] = Datos;
                    }

                    List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                    UsersDTO user = (UsersDTO)Session["usuario"];
                    EventsDTO selectedEvent = (EventsDTO)ViewState["Event"];
                    if (selectedEvent == null)
                    {
                        List<InscriptionsDTO> allInscriptions = ServiceHelper.ws.GetInscriptions().ToList();
                        selectedEvent = events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id));
                    }

                    ddEvents.DataSource = events;
                    ddEvents.DataTextField = "name";
                    ddEvents.DataValueField = "Id";
                    ddEvents.DataBind();
                    ddEvents.Items.Add("TODOS LOS EVENTOS");
                    ddEvents.Items.FindByText(selectedEvent.name).Selected = true;


                    ActualizarGrilla();
                }
                //lbMsg.Text = string.Empty;
                //UsersDTO user = (UsersDTO)Session["usuario"];
                //if (!Page.IsPostBack)
                //{
                //    if (user.roleId != 1)
                //    {
                //        //LoadData();
                //    }
                //}
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected string obtenerDatos()
        {
            List<MetricsResume> metrics = (List<MetricsResume>)ViewState["Metrics"];
            UsersDTO user = (UsersDTO)Session["usuario"];
            List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
            List<Product_typeDTO> types = ServiceHelper.ws.GetProductTypes().ToList();
            DataTable Datos = (DataTable)ViewState["dtDatos"];

            if (metrics == null)
            {
                metrics = new List<MetricsResume>();
                if (events != null)
                {
                    EventsDTO selectedEvent = (EventsDTO)ViewState["Event"];
                    if (selectedEvent == null)
                    {
                        List<InscriptionsDTO> allInscriptions = ServiceHelper.ws.GetInscriptions().ToList();
                        selectedEvent = events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id));
                    }

                    List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(selectedEvent.Id).ToList();
                    List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                    List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();

                    List<double> ventas = new List<double>();
                    List<string> typesName = new List<string>();
                    List<Sales_detailsDTO> sales_Details = ServiceHelper.ws.GetSalesDetailsByBrandID(user.Id).ToList();
                    foreach (var type in types)
                    {
                        int cantVentas = 0;
                        foreach (var product in products.Where(x => x.product_type_id == type.Id))
                        {
                            List<MetricsResume> resume = new List<MetricsResume>();
                            List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                            foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                            {
                                List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == sale.Id).ToList();
                                foreach (var detail in salesDetails.Where(x => x.product_id == product.Id))
                                {
                                    cantVentas += detail.quantity;
                                }
                            }
                        }


                        MetricsResume resumeItem = new MetricsResume();
                        resumeItem.Id = metrics.Count() + 1;
                        resumeItem.event_name = selectedEvent.name;
                        resumeItem.product_name = type.description;
                        resumeItem.sales = cantVentas;
                        metrics.Add(resumeItem);

                        ventas.Add(cantVentas);
                        typesName.Add(type.description);
                    }

                    var totalVentas = ventas.Sum();


                    double[] cantidadVentas = ventas.ToArray();
                    string[] typeProducts = typesName.ToArray();

                    foreach (var r in metrics)
                    {
                        Datos.Rows.Add(new object[] { r.product_name, r.sales });
                        r.percentage = (100 * r.sales / totalVentas).ToString("N1");
                    }
                }
            }
            else
            {
                if (ddEvents.SelectedValue != "TODOS LOS EVENTOS")
                {
                    metrics = new List<MetricsResume>();

                    EventsDTO selectedEvent = events.First(x => x.Id == Convert.ToInt32(ddEvents.SelectedValue));
                    if (selectedEvent == null)
                    {
                        List<InscriptionsDTO> allInscriptions = ServiceHelper.ws.GetInscriptions().ToList();
                        selectedEvent = events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id));
                    }

                    List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(selectedEvent.Id).ToList();
                    List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                    List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();

                    List<double> ventas = new List<double>();
                    List<string> typesName = new List<string>();
                    List<Sales_detailsDTO> sales_Details = ServiceHelper.ws.GetSalesDetailsByBrandID(user.Id).ToList();
                    foreach (var type in types)
                    {
                        int cantVentas = 0;
                        foreach (var product in products.Where(x => x.product_type_id == type.Id))
                        {
                            List<MetricsResume> resume = new List<MetricsResume>();
                            List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
                            foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                            {
                                List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == sale.Id).ToList();
                                foreach (var detail in salesDetails.Where(x => x.product_id == product.Id))
                                {
                                    cantVentas += detail.quantity;
                                }
                            }
                        }

                        MetricsResume resumeItem = new MetricsResume();
                        resumeItem.Id = metrics.Count() + 1;
                        resumeItem.event_name = selectedEvent.name;
                        resumeItem.product_name = type.description;
                        resumeItem.sales = cantVentas;
                        metrics.Add(resumeItem);

                        ventas.Add(cantVentas);
                        typesName.Add(type.description);
                    }
                }

                foreach (var type in types)
                {
                    int quantity = 0;
                    foreach (var r in metrics.Where(x => x.product_name == type.description))
                    {
                        quantity += r.sales;
                    }

                    Datos.Rows.Add(new object[] { type.description, quantity});
                }
            }


            ViewState["Metrics"] = metrics;


            string strDatos = "[['Productos','Cant. Vendida'],";

            foreach (DataRow dr in Datos.Rows)
            {
                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1];
                strDatos = strDatos + "],";
            }


            ActualizarGrilla();

            strDatos = strDatos + "]";
            return strDatos;
        }

        protected void ActualizarGrilla()
        {
            var metrics = ViewState["Metrics"];

            gvSales.DataSource = metrics;
            gvSales.DataBind();
        }




        //protected void LoadData()
        //{
        //    UsersDTO user = (UsersDTO)Session["usuario"];
        //    List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
        //    if (events != null)
        //    {
        //        EventsDTO selectedEvent = (EventsDTO)ViewState["Event"];
        //        if (selectedEvent == null)
        //        {
        //            List<InscriptionsDTO> allInscriptions = ServiceHelper.ws.GetInscriptions().ToList();
        //            selectedEvent = events.First(x => x.Id == (allInscriptions.Last(y => y.brand_id == user.Id).event_id));
        //        }

        //        ddEvents.DataSource = events;
        //        ddEvents.DataTextField = "name";
        //        ddEvents.DataValueField = "Id";
        //        ddEvents.DataBind();
        //        ddEvents.Items.Add("TODOS LOS EVENTOS");
        //        ddEvents.Items.FindByText(selectedEvent.name).Selected = true;

        //        List<MetricsResume> metrics = new List<MetricsResume>();
        //        List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(selectedEvent.Id).ToList();
        //        List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
        //        List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();
        //        List<Product_typeDTO> types = ServiceHelper.ws.GetProductTypes().ToList();

        //        List<double> ventas = new List<double>();
        //        List<string> typesName = new List<string>();

        //        foreach (var type in types)
        //        {
        //            int cantVentas = 0;
        //            foreach (var product in products.Where(x => x.product_type_id == type.Id))
        //            {
        //                List<MetricsResume> resume = new List<MetricsResume>();
        //                List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(selectedEvent.Id).ToList();
        //                foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
        //                {
        //                    List<Sales_detailsDTO> salesDetails = ServiceHelper.ws.GetSalesDetailsBySaleID(sale.Id).Where(x => x.brand_id == user.Id).ToList();
        //                    foreach (var detail in salesDetails.Where(x => x.product_id == product.Id))
        //                    {
        //                        cantVentas += detail.quantity;
        //                    }
        //                }                       
        //            }

        //            MetricsResume resumeItem = new MetricsResume();
        //            resumeItem.Id = metrics.Count() + 1;
        //            resumeItem.event_name = selectedEvent.name;
        //            resumeItem.product_name = type.description;
        //            resumeItem.sales = cantVentas;
        //            metrics.Add(resumeItem);

        //            ventas.Add(cantVentas);
        //            typesName.Add(type.description);
        //        }

        //        var totalVentas = ventas.Sum();
        //        foreach (var r in metrics)
        //        {
        //            r.percentage = (100 * r.sales / totalVentas).ToString("N1");
        //        }

        //        double[] cantidadVentas = ventas.ToArray();
        //        string[] typeProducts = typesName.ToArray();
        //        Chart1.Series["Default"].Points.DataBindXY(typeProducts, cantidadVentas);
        //        Chart1.Series["Default"].ChartType = SeriesChartType.Doughnut;
        //        Chart1.Series["Default"]["PieLabelStyle"] = "Disabled";
        //        Chart1.Series["Default"].Label = "#PERCENT{P2}";
        //        Chart1.Series["Default"].LegendText = "#VALX" + " (" + "#PERCENT{P1}" + ")";
        //        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        //        Chart1.Legends[0].Enabled = true;
        //        Chart1.Legends[0].LegendStyle = LegendStyle.Column;
        //        Chart1.Legends[0].Docking = Docking.Right;
        //        Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

        //        gvSales.DataSource = metrics;
        //        gvSales.DataBind();
        //    }
        //}

        protected void btFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                List<MetricsResume> metrics = new List<MetricsResume>();
                UsersDTO user = (UsersDTO)Session["usuario"];
                List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();
                List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                List<Product_typeDTO> types = ServiceHelper.ws.GetProductTypes().ToList();
                List<Sales_detailsDTO> sales_Details = ServiceHelper.ws.GetSalesDetailsByBrandID(user.Id).ToList();
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
                            List<string> typesName = new List<string>();
                            foreach (var selectedEvent in selectedEvents)
                            {
                                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(selectedEvent.Id).ToList();
                                foreach (var inscription in inscriptions.Where(x => x.brand_id == user.Id))
                                {
                                    foreach (var type in types)
                                    {
                                        int cantVentas = 0;
                                        foreach (var product in products.Where(x => x.product_type_id == type.Id))
                                        {
                                            List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(inscription.event_id).ToList();
                                            foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                                            {
                                                List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == sale.Id).ToList();
                                                foreach (var detail in salesDetails.Where(x => x.product_id == product.Id))
                                                {
                                                    cantVentas += detail.quantity;
                                                }
                                            }
                                        }

                                        MetricsResume resumeItem = new MetricsResume();
                                        resumeItem.Id = metrics.Count() + 1;
                                        resumeItem.event_name = events.First(x => x.Id == inscription.event_id).name;
                                        resumeItem.product_name = type.description;
                                        resumeItem.sales = cantVentas;
                                        metrics.Add(resumeItem);
                                    }
                                }
                            }
                            foreach (var type in types)
                            {
                                double cantVentas = metrics.Where(x => x.product_name == type.description).Sum(x => x.sales);
                                ventas.Add(cantVentas);
                                typesName.Add(type.description);
                            }

                            double[] cantidadVentas = ventas.ToArray();
                            string[] typeProducts = typesName.ToArray();
                        }
                        else
                        {
                            lbMsg.Text = "La Fecha de Fin no puede ser menor a la fecha de inicio.";
                            return;
                        }
                    }              
                    else
                    {
                        List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByBrandID(user.Id).ToList();
                        List<double> ventas = new List<double>();
                        List<string> typesName = new List<string>();
                        foreach (var inscription in inscriptions)
                        {
                            foreach (var type in types)
                            {
                                int cantVentas = 0;
                                foreach (var product in products.Where(x => x.product_type_id == type.Id))
                                {
                                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(inscription.event_id).ToList();
                                    foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                                    {
                                        List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == sale.Id).ToList();
                                        foreach (var detail in salesDetails.Where(x => x.product_id == product.Id))
                                        {
                                            cantVentas += detail.quantity;
                                        }
                                    }
                                }

                                MetricsResume resumeItem = new MetricsResume();
                                resumeItem.Id = metrics.Count() + 1;
                                resumeItem.event_name = events.First(x => x.Id == inscription.event_id).name;
                                resumeItem.product_name = type.description;
                                resumeItem.sales = cantVentas;
                                metrics.Add(resumeItem);
                            }
                        }

                        foreach (var type in types)
                        {
                            int cantVentas = 0;
                            foreach (var inscription in inscriptions)
                            {
                                foreach (var product in products.Where(x => x.product_type_id == type.Id))
                                {
                                    List<SalesDTO> sales = ServiceHelper.ws.GetSalesByEventID(inscription.event_id).ToList();
                                    foreach (var sale in sales.Where(x => x.state == "CONFIRMADA"))
                                    {
                                        List<Sales_detailsDTO> salesDetails = sales_Details.Where(x => x.sale_id == sale.Id).ToList();
                                        foreach (var detail in salesDetails.Where(x => x.product_id == product.Id))
                                        {
                                            cantVentas += detail.quantity;
                                        }
                                    }
                                }
                            }

                            ventas.Add(cantVentas);
                            typesName.Add(type.description);
                        }

                        double[] cantidadVentas = ventas.ToArray();
                        string[] typeProducts = typesName.ToArray();

                    }

                    ViewState["Metrics"] = metrics;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "drawChart()", true);
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}