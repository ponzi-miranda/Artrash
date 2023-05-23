using ArtTrash.Helpers;
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class ImportStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Importar Stock");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    List<EventsDTO> events = ServiceHelper.ws.GetEvents().ToList();
                    events.Reverse();
                    ddEvents.DataSource = events;
                    ddEvents.DataValueField = "Id";
                    ddEvents.DataTextField = "name";
                    ddEvents.DataBind();
                    ddEvents.Items.Add("SELECCIONE");
                    ddEvents.Items.FindByText("SELECCIONE").Selected = true;

                    ddEventsAdmin.DataSource = events;
                    ddEventsAdmin.DataValueField = "Id";
                    ddEventsAdmin.DataTextField = "name";
                    ddEventsAdmin.DataBind();
                    ddEventsAdmin.Items.Add("SELECCIONE");
                    ddEventsAdmin.Items.FindByText("SELECCIONE").Selected = true;

                    List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
                    ddBrands.DataSource = brands;
                    ddBrands.DataValueField = "Id";
                    ddBrands.DataTextField = "name";
                    ddBrands.DataBind();
                    ddBrands.Items.Add("TODAS");
                    ddBrands.Items.FindByText("TODAS").Selected = true;

                    List<ProductsDTO> products = ServiceHelper.ws.GetProducts().ToList();

                    ViewState["UltimaTiendita"] = events.First();
                    ViewState["Productos"] = products;


                    ReloadData(ddBrands.SelectedValue, string.Empty);




                    //List<UsersDTO> brandsIncripted = new List<UsersDTO>();
                    //List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(events.First().Id).ToList();
                    //foreach (var inscription in inscriptions)
                    //{
                    //    UsersDTO brand = brands.First(x => x.Id == inscription.brand_id);
                    //    brandsIncripted.Add(brand);
                    //}

                    //ddBrandStock.DataSource = brandsIncripted;

                    ddBrandStock.DataSource = brands;
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
            List<StockDTO> stock = new List<StockDTO>();
            List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];//new List<ProductsDTO>();
            if (user.roleId == 2)
            {
                lbEvent.Text = string.Empty;
                products = products.Where(x => x.brand_id == user.Id).ToList();//ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();
                plBrand.Visible = true;
                plAdmin.Visible = false;
                if (ddEvents.SelectedValue != "SELECCIONE")
                {
                    EventsDTO evento = ServiceHelper.ws.GetEventByID(Convert.ToInt32(ddEvents.SelectedValue));
                    stock = ServiceHelper.ws.GetStockByEventID(evento.Id).Where(x => x.brand_id == user.Id).ToList();
                    lbEvent.Text = "Evento: " + evento.name;
                }
            }
            else
            {
                lbEvent.Text = string.Empty;
                products = (List<ProductsDTO>)ViewState["Productos"];//ServiceHelper.ws.GetProducts().ToList();
                plBrand.Visible = false;
                plAdmin.Visible = true;
                if (ddEvents.SelectedValue != "SELECCIONE")
                {
                    EventsDTO evento = ServiceHelper.ws.GetEventByID(Convert.ToInt32(ddEventsAdmin.SelectedValue));
                    stock = ServiceHelper.ws.GetStockByEventID(evento.Id).ToList();
                    lbEvent.Text = "Evento: " + evento.name;
                }
            }

            List<Models.StockModel> stockProducts = new List<Models.StockModel>();
            if (ddEvents.SelectedValue != "SELECCIONE" || ddEventsAdmin.SelectedValue != "SELECCIONE")
            {
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
                    product.product_id = stockItem.product_id;

                    stockProducts.Add(product);
                }
            }
            else
            {
                foreach (var product in products.Where(x => x.state == "ACTIVO"))
                {
                    Models.StockModel productStock = new Models.StockModel();
                    productStock.Id = product.Id;
                    productStock.serial_number = product.serial_number;
                    productStock.description = product.description;
                    productStock.quantity = 1;
                    productStock.brand_id = product.brand_id;
                    productStock.brand = users.First(x => x.Id == product.brand_id).name;
                    productStock.type = types.First(x => x.Id == product.product_type_id).description;
                    productStock.product_id = product.Id;

                    stockProducts.Add(productStock);
                }
            }

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

        protected void btImportar_Click(object sender, EventArgs e)
        {
            try
            {
                UsersDTO user = (UsersDTO)Session["usuario"];
                List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];
                EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                List<Models.StockModel> stockProducts = (List<Models.StockModel>)ViewState["Stock"];
                List<Models.StockModel> stockImportar = new List<Models.StockModel>();
                List<StockDTO> stockEvent = ServiceHelper.ws.GetStockByEventID(lastEvent.Id).ToList();
                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(lastEvent.Id).ToList();
                if (user.roleId == 1)
                {
                    foreach (GridViewRow row in gvStockAdmin.Rows)
                    {
                        CheckBox CheckRow = (row.Cells[0].FindControl("cbStockAdmin") as CheckBox);
                        if (CheckRow.Checked)
                        {
                            Models.StockModel stock = stockProducts.First(x => x.Id == Convert.ToInt32(gvStockAdmin.DataKeys[row.RowIndex]["Id"]));

                            if (inscriptions.Find(x => x.brand_id == stock.brand_id) != null)
                            {

                                stockImportar.Add(stock);
                            }
                            else
                            {
                                lbMsg.Text = "Solo puede importar el Stock de marcas inscriptas al ultimo evento creado.";
                                return;
                            }

                        }
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvStock.Rows)
                    {
                        CheckBox CheckRow = (row.Cells[0].FindControl("cbStock") as CheckBox);
                        if (CheckRow.Checked)
                        {
                            if (inscriptions.Find(x => x.brand_id == Convert.ToInt32(gvStock.DataKeys[row.RowIndex]["Id"])) == null)
                            {
                                Models.StockModel stock = stockProducts.First(x => x.Id == Convert.ToInt32(gvStock.DataKeys[row.RowIndex]["Id"]));

                                stockImportar.Add(stock);
                            }
                            else
                            {
                                lbMsg.Text = "No se encuentra inscripto en el ultimo evento.";
                                return;
                            }

                        }
                    }
                }

                if (stockImportar.Count > 0)
                {
                    foreach (var stock in stockImportar)
                    {
                        StockDTO newStock = new StockDTO();

                        newStock.brand_id = stock.brand_id;
                        newStock.event_id = lastEvent.Id;
                        newStock.product_id = stock.product_id;
                        newStock.quantity = stock.quantity;
                        if (stockEvent.Any(x => x.product_id == stock.product_id))
                        {
                            newStock.Id = stockEvent.First(x => x.product_id == stock.product_id).Id;
                        }
                        if (products.First(x => x.Id == stock.product_id).state == "ACTIVO")
                        {
                            ServiceHelper.ws.SetStock(newStock);
                        }
                    }
                }

                Response.Redirect("/Pages/Stock.aspx");

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
                List<Models.StockModel> stockProducts = (List<Models.StockModel>)ViewState["Stock"];

                int id = Convert.ToInt32(btGuardar.Attributes["id"]);

                Models.StockModel stock = stockProducts.First(x => x.Id == id);

                stock.quantity = Convert.ToInt32(txCantidad.Text);


                ViewState["Stock"] = stockProducts;
                gvStock.DataSource = stockProducts.OrderBy(x => x.brand).ToList();
                gvStock.DataBind();
                gvStockAdmin.DataSource = stockProducts.OrderBy(x => x.brand).ToList();
                gvStockAdmin.DataBind();

                btGuardar.Attributes.Add("id", "0");

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

        protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                UsersDTO user = (UsersDTO)Session["usuario"];
                List<Models.StockModel> stockProducts = (List<Models.StockModel>)ViewState["Stock"];
                List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];

                int id = Convert.ToInt32(gvStock.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                if (user.roleId == 2)
                {
                    ddBrandStock.SelectedValue = user.Id.ToString();
                    ddBrandStock.Attributes.Add("disabled", "true");

                    ddProducts.DataSource = null;
                    ddProducts.DataSource = products.Where(x => x.Id == stockProducts.First(y => y.Id == id).product_id).ToList();
                    ddProducts.DataTextField = "description";
                    ddProducts.DataValueField = "Id";
                    ddProducts.DataBind();
                    ddProducts.Attributes.Add("disabled", "true");

                    txCantidad.Text = stockProducts.First(y => y.Id == id).quantity.ToString();

                    btGuardar.Attributes.Add("id", id.ToString());
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

        protected void gvStockAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                List<Models.StockModel> stockProducts = (List<Models.StockModel>)ViewState["Stock"];
                List<ProductsDTO> products = (List<ProductsDTO>)ViewState["Productos"];

                int id = Convert.ToInt32(gvStock.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                ddBrandStock.SelectedValue = stockProducts.First(x => x.Id == id).brand_id.ToString();
                ddBrandStock.Attributes.Add("disabled", "true");

                ddProducts.DataSource = null;
                ddProducts.DataSource = products.Where(x => x.Id == stockProducts.First(y => y.Id == id).product_id).ToList();
                ddProducts.DataTextField = "description";
                ddProducts.DataValueField = "Id";
                ddProducts.DataBind();
                ddProducts.Attributes.Add("disabled", "true");

                txCantidad.Text = stockProducts.First(y => y.Id == id).quantity.ToString();

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

        protected void btSeleccionarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                UsersDTO user = (UsersDTO)Session["usuario"];
                if (user.roleId == 2) 
                {
                    foreach (GridViewRow row in gvStock.Rows)
                    {
                        CheckBox cb = (CheckBox)row.FindControl("cbStock");
                        cb.Checked = true;
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvStockAdmin.Rows)
                    {
                        CheckBox cb = (CheckBox)row.FindControl("cbStockAdmin");
                        cb.Checked = true;
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