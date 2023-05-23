using ArtTrash.Helpers;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class Stock : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Stock");
                if (!Page.IsPostBack)
                {
                    List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                    events.Reverse();
                    ddEvents.DataSource = events;
                    ddEvents.DataValueField = "Id";
                    ddEvents.DataTextField = "name";
                    ddEvents.DataBind();

                    ddEventsAdmin.DataSource = events;
                    ddEventsAdmin.DataValueField = "Id";
                    ddEventsAdmin.DataTextField = "name";
                    ddEventsAdmin.DataBind();

                    List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                    ddBrands.DataSource = brands.OrderBy(x => x.name);
                    ddBrands.DataValueField = "Id";
                    ddBrands.DataTextField = "name";
                    ddBrands.DataBind();
                    ddBrands.Items.Add("TODAS");
                    ddBrands.Items.FindByText("TODAS").Selected = true;

                    List<ProductsDTO> products = ServiceHelper.ws.GetProducts().ToList();//.Where(x => x.state == "ACTIVO").ToList();

                    ViewState["UltimaTiendita"] = events.First();
                    ViewState["Productos"] = products;
                    //ddProducts.DataSource = products;
                    //ddProducts.DataValueField = "Id";
                    //ddProducts.DataTextField = "description";
                    //ddProducts.DataBind();

                    ReloadData(ddBrands.SelectedValue, string.Empty);

                   


                    List<UsersDTO> brandsIncripted = new List<UsersDTO>();
                    List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(events.First().Id).ToList();
                    foreach (var inscription in inscriptions)
                    {
                        UsersDTO brand = brands.First(x => x.Id == inscription.brand_id);
                        brandsIncripted.Add(brand);
                    }

                    ddBrandStock.DataSource = brandsIncripted.OrderBy(x => x.name);
                    ddBrandStock.DataValueField = "Id";
                    ddBrandStock.DataTextField = "name";
                    ddBrandStock.DataBind();
                    ddBrandStock.Items.Add("SELECCIONE");
                    ddBrandStock.Items.FindByText("SELECCIONE").Selected = true;
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ReloadData(string brand_id, string product_name)
        {
            UsersDTO user = (UsersDTO)Session["usuario"];

            List<UsersDTO> users = ServiceHelper.ws.GetBrands().ToList();
            List<Product_typeDTO> types = ServiceHelper.ws.GetProductTypes().ToList();
            EventsDTO evento = new EventsDTO();
            List<StockDTO> stock = new List<StockDTO>();
            List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];
            if (user.roleId == 2)
            {
                evento = ServiceHelper.ws.GetEventByID(Convert.ToInt32(ddEvents.SelectedValue));
                stock = ServiceHelper.ws.GetStockByEventID(evento.Id).Where(x => x.brand_id == user.Id).ToList();

                ddProducts.DataSource = products.Where(x => x.brand_id == user.Id && x.state == "ACTIVO").ToList();//ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();;
                ddProducts.DataTextField = "description";
                ddProducts.DataValueField = "Id";
                ddProducts.DataBind();

                plBrand.Visible = true;
                plAdmin.Visible = false;
                lbEvent.Text = evento.name;
            }
            else
            {
                 evento = ServiceHelper.ws.GetEventByID(Convert.ToInt32(ddEventsAdmin.SelectedValue));
                stock = ServiceHelper.ws.GetStockByEventID(evento.Id).ToList();
                //products = ServiceHelper.ws.GetProducts().ToList();
                plBrand.Visible = false;
                plAdmin.Visible = true;
                lbEvent.Text = evento.name;
            }

            List<Models.StockModel> stockProducts = new List<Models.StockModel>();
            foreach (var stockItem in stock)
            {
                Models.StockModel product = new Models.StockModel();
                product.Id = stockItem.Id;
                product.serial_number = products.First(x => x.Id == stockItem.product_id).serial_number;
                product.description = products.First(x => x.Id == stockItem.product_id).description;
                product.quantity = stockItem.quantity;
                product.brand_id = stockItem.brand_id;
                product.brand = users.First(x => x.Id == stockItem.brand_id).name;
                product.type = types.First(x => x.Id == products.First(o => o.Id == stockItem.product_id).product_type_id).description;
                product.product_id = products.First(x => x.Id == stockItem.product_id).Id;
                product.event_id = evento.Id;
                product.price = products.First(x => x.Id == stockItem.product_id).price;

                stockProducts.Add(product);
            }
            //lbBrand.Text = ServiceHelper.ws.GetBrandByID(brand).name;

            if (ddBrands.SelectedValue != "TODAS")
            {
                stockProducts = stockProducts.Where(x => x.brand_id == Convert.ToInt32(brand_id)).ToList();
            }
            if (!string.IsNullOrEmpty(product_name))
            {
                stockProducts = stockProducts.Where(x => x.description.Contains(product_name)).ToList();
            }



            ViewState["Stock"] = stockProducts;
            gvStock.DataSource = stockProducts.OrderBy(x => x.brand).ToList();
            gvStock.DataBind();
            gvStockAdmin.DataSource = stockProducts.OrderBy(x => x.brand).ToList();
            gvStockAdmin.DataBind();
        }

        protected void btDownload_Click(object sender, EventArgs e)
        {
            try
            {
                List<Models.StockModel> stockProducts = (List<Models.StockModel>)ViewState["Stock"];
                List<Models.StockPrintModel> stockPrints = new List<Models.StockPrintModel>();
                List<EventsDTO> eventos = ServiceHelper.ws.GetEvents().ToList();
                foreach (var item in stockProducts)
                {
                    Models.StockPrintModel stockPrint = new Models.StockPrintModel();

                    stockPrint.Cantidad = item.quantity;
                    stockPrint.Codigo = item.serial_number.Replace(',', '-');
                    stockPrint.Evento = eventos.First(x => x.Id == item.event_id).name.Replace(',', '-');
                    stockPrint.Marca = item.brand.Replace(',', '-');
                    stockPrint.Precio = item.price;
                    stockPrint.Producto = item.description.Replace(',','-');
                    stockPrint.Tipo = item.type.Replace(',', '-');

                    stockPrints.Add(stockPrint);
                }

                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                DataTable dt = converter.ToDataTable(stockPrints);

                ExportCsv(dt);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        private void ExportCsv(DataTable dt)
        {
            string decimalChar = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            string fieldChar = ",";
            if (decimalChar == ",")
                fieldChar = ";";

            BuildCsv(dt, fieldChar);
        }

        private void BuildCsv(DataTable dt, string fieldChar)
        {
            string aux = string.Empty;

            //Titulos
            foreach (DataColumn col in dt.Columns)
            {
                aux += col.ColumnName + fieldChar;
            }

            aux = aux.TrimEnd(fieldChar.ToCharArray()[0]);
            aux += Environment.NewLine;

            //Datos
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    aux += Convert.ToString(dr[col]) + fieldChar;
                }

                aux = aux.TrimEnd(fieldChar.ToCharArray()[0]);
                aux += Environment.NewLine;
            }

            byte[] bytes = Encoding.Unicode.GetBytes(aux);


            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader($"Content-Disposition", $"attachment; filename=\"StockTienditaAl{DateTime.Now.ToShortDateString()}.csv\"");
            Response.ContentType = "application/text";
            Response.BinaryWrite(bytes);
            //Response.Flush();
            //Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btAddStock_Click(object sender, EventArgs e)
        {
            try
            {
                btGuardar.Attributes.Add("id", "0");
                txCantidad.Text = string.Empty;
                ddProducts.Attributes.Remove("disabled");
                ddBrandStock
                    .Attributes.Remove("disabled");

                EventsDTO evento = (EventsDTO)ViewState["UltimaTiendita"];
                UsersDTO user = (UsersDTO)Session["usuario"];

                if (evento.state == "FINALIZADO")
                {
                    lbMsg.Text = "No puede modificar el stock de un evento finalizado.";
                    return;
                }

                if (user.roleId == 2)
                {
                    List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(evento.Id).ToList();
                    var inscripted = inscriptions.Where(x => x.brand_id == user.Id);
                    if (evento.state == "PENDIENTE" && inscripted.Count() > 0)
                    {
                        ddBrandStock.SelectedValue = user.Id.ToString();
                        ddBrandStock.Attributes.Add("disabled", "true");                   
                    }
                    else
                    {
                        lbMsg.Text = "Solo puede editar Stock si esta inscripto en un evento.";
                        return;
                    }
                }
                if (ddBrandStock.Items.Count == 0)
                {
                    lbMsg.Text = "Para cargar Stock en una tiendita primero debe inscribir alguna marca.";
                    return;
                }

                ScriptManager.RegisterClientScriptBlock(
                    this, this.GetType(), "none",
                    "<script>$('#exampleModalLong').modal('show');</script>", false);
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
                ReloadData(ddBrands.SelectedValue, txProduct.Text);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btBuscarAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                ReloadData(ddBrands.SelectedValue, txProductAdmin.Text);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                if (lastEvent.state != "FINALIZADO")
                {
                    StockDTO stock = new StockDTO();

                    stock.Id = Convert.ToInt32(btGuardar.Attributes["id"]);
                    stock.brand_id = Convert.ToInt32(ddBrandStock.SelectedValue);
                    stock.product_id = Convert.ToInt32(ddProducts.SelectedValue);
                    stock.quantity = Convert.ToInt32(txCantidad.Text);
                    stock.event_id = lastEvent.Id;


                    List<StockDTO> currentStock = ServiceHelper.ws.GetStockByEventID(lastEvent.Id).ToList();

                    if (currentStock.FirstOrDefault(x => x.product_id == stock.product_id) != null && stock.Id == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                        "#exampleModalLong", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#exampleModalLong').hide();", true);
                        lbMsg.Text = "Producto ya cargado en el stock";
                        return;
                    }
                    else
                    {
                        stock = ServiceHelper.ws.SetStock(stock);
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                    "#exampleModalLong", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#exampleModalLong').hide();", true);

                    

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                    "#exampleModalLong", "$('.modal-backdrop').remove();", true);

                    ReloadData(ddBrands.SelectedValue, string.Empty);
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ddBrandStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddBrandStock.SelectedValue != "SELECCIONE")
                {
                    List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];

                    ddProducts.DataSource = null;
                    ddProducts.DataSource = products.Where(x => x.brand_id == Convert.ToInt32(ddBrandStock.SelectedValue) && x.state == "ACTIVO").ToList();
                    ddProducts.DataTextField = "description";
                    ddProducts.DataValueField = "Id";
                    ddProducts.DataBind();
                }

                ScriptManager.RegisterClientScriptBlock(
                 this, this.GetType(), "none",
                 "<script>$('#exampleModalLong').modal('show');</script>", false);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(),
             "#exampleModalLong", "$('.modal-backdrop').remove();", true);

                ScriptManager.RegisterStartupScript(this, Page.GetType(),
                    "Popup", "$('#exampleModalLong').modal('show')", true);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvStockAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];
                EventsDTO evento = ServiceHelper.ws.GetEventByID(Convert.ToInt32(ddEventsAdmin.SelectedValue));//(EventsDTO)ViewState["UltimaTiendita"];
                UsersDTO user = (UsersDTO)Session["usuario"];

                if (evento.state == "FINALIZADO")
                {
                    lbMsg.Text = "No puede modificar el stock de un evento finalizado.";
                    return;
                }

                int id = Convert.ToInt32(gvStock.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                List<StockDTO> stock = ServiceHelper.ws.GetStockByEventID(evento.Id).ToList();

                ddBrandStock.SelectedValue = stock.First(x => x.Id == id).brand_id.ToString();
                ddBrandStock.Attributes.Add("disabled", "true");

                ddProducts.DataSource = null;
                ddProducts.DataSource = products.Where(x => x.Id == stock.First(y => y.Id == id).product_id).ToList();
                ddProducts.DataTextField = "description";
                ddProducts.DataValueField = "Id";
                ddProducts.DataBind();
                ddProducts.Attributes.Add("disabled", "true");

                txCantidad.Text = stock.First(y => y.Id == id).quantity.ToString();

                btGuardar.Attributes.Add("id", id.ToString());

                ScriptManager.RegisterClientScriptBlock(
                  this, this.GetType(), "none",
                  "<script>$('#exampleModalLong').modal('show');</script>", false);
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
                EventsDTO evento = ServiceHelper.ws.GetEventByID(Convert.ToInt32(ddEventsAdmin.SelectedValue)); //(EventsDTO)ViewState["UltimaTiendita"];
                UsersDTO user = (UsersDTO)Session["usuario"];

                if (evento.state == "FINALIZADO")
                {
                    lbMsg.Text = "No puede modificar el stock de un evento finalizado.";
                    return;
                }

                if (user.roleId == 2)
                {
                    List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(evento.Id).ToList();
                    var inscripted = inscriptions.Where(x => x.brand_id == user.Id);
                    if (evento.state == "PENDIENTE" && inscripted.Count() > 0)
                    {
                        ddBrandStock.SelectedValue = user.Id.ToString();
                        ddBrandStock.Attributes.Add("disabled", "true");
                        List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];


                        int id = Convert.ToInt32(gvStock.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                        List<StockDTO> stock = ServiceHelper.ws.GetStockByEventID(evento.Id).ToList();

                        ddProducts.DataSource = null;
                        ddProducts.DataSource = products.Where(x => x.Id == stock.First(y => y.Id == id).product_id).ToList();
                        ddProducts.DataTextField = "description";
                        ddProducts.DataValueField = "Id";
                        ddProducts.DataBind();
                        ddProducts.Attributes.Add("disabled", "true");

                        txCantidad.Text = stock.First(y => y.Id == id).quantity.ToString();

                        btGuardar.Attributes.Add("id", id.ToString());
                    }
                    else
                    {
                        lbMsg.Text = "Solo puede editar Stock si esta inscripto en un evento.";
                        return;
                    }
                }
                if (ddBrandStock.Items.Count == 0)
                {
                    lbMsg.Text = "Para cargar Stock en una tiendita primero debe inscribir alguna marca.";
                    return;
                }

                ScriptManager.RegisterClientScriptBlock(
                    this, this.GetType(), "none",
                    "<script>$('#exampleModalLong').modal('show');</script>", false);

            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btImportarStock_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/Pages/ImportStock.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btNuevoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                UsersDTO user = (UsersDTO)Session["usuario"];

                if (user.roleId == 1)
                {
                    Response.Redirect("/Pages/AddNewProduct.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Redirect("/Pages/NewProduct.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                 "#exampleModalLong", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#exampleModalLong').hide();", true);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                    "#exampleModalLong", "$('.modal-backdrop').remove();", true);
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }

    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}