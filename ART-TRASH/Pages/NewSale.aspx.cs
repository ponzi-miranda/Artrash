using ArtTrash.Helpers;
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
    public partial class NewSale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Nueva Venta");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    ddPaymentType.DataSource = ServiceHelper.ws.GetPayments();
                    ddPaymentType.DataTextField = "description";
                    ddPaymentType.DataValueField = "Id";
                    ddPaymentType.DataBind();

                    ddPaymentType1.DataSource = ServiceHelper.ws.GetPayments();
                    ddPaymentType1.DataTextField = "description";
                    ddPaymentType1.DataValueField = "Id";
                    ddPaymentType1.DataBind();

                    ddPaymentType2.DataSource = ServiceHelper.ws.GetPayments();
                    ddPaymentType2.DataTextField = "description";
                    ddPaymentType2.DataValueField = "Id";
                    ddPaymentType2.DataBind();


                    LoadData();                    
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void LoadData()
        {

            EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
            List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
            List<UsersDTO> brandsInscripted = new List<UsersDTO>();
            if (lastEvent != null)
            {
                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(lastEvent.Id).ToList();
                foreach (var inscription in inscriptions)
                {
                    brandsInscripted.Add(brands.First(x => x.Id == inscription.brand_id));
                }
            }

            ddBrand.DataSource = brandsInscripted.OrderBy(x => x.name);
            ddBrand.DataTextField = "name";
            ddBrand.DataValueField = "Id";
            ddBrand.DataBind();
            ddBrand.Items.Add("Seleccione");
            ddBrand.Items.FindByText("Seleccione").Selected = true;

            DataTable products = (DataTable)ViewState["dtProducts"];
            gvProductos.DataSource = products;
            gvProductos.DataBind();
            if (products != null)
            {
                if (products.Rows.Count > 0)
                {
                    decimal total = 0;
                    gvProductos.FooterRow.Cells[4].Text = "Total";
                    gvProductos.FooterRow.Cells[5].Font.Bold = true;
                    gvProductos.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;

                    foreach (DataRow dr in products.Rows)
                    {
                        total = total + Convert.ToDecimal(dr["subtotal"]);
                        gvProductos.FooterRow.Cells[5].Text = "$ " + total.ToString();
                        gvProductos.FooterRow.Cells[5].Font.Bold = true;
                        gvProductos.FooterRow.Cells[4].Font.Bold = true;
                    }
                }
            }

            ddProducts.Items.Clear();
            ddProducts.Items.Add("SELECCIONE MARCA");
            ddProducts.Items.FindByText("SELECCIONE MARCA").Selected = true;

            txQuantity.Text = string.Empty;
            txDiscount.Text = string.Empty;
        }
        protected void btAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ProductsDTO product = new ProductsDTO();
                PromotionsDTO promotion = new PromotionsDTO();
                StockDTO stock = new StockDTO();
                decimal subtotal = 0;
                decimal profit = 0;

                if (ddProducts.SelectedValue != "Seleccione" && ddPromotions.SelectedValue != "Seleccione")
                {
                    lbMsg.Text = "No puede seleccionar un producto y una promoción al mismo tiempo.";
                    return;
                }
                else if (ddProducts.SelectedValue != "Seleccione")
                {
                    product = ServiceHelper.ws.GetProductByID(Convert.ToInt32(ddProducts.SelectedValue));
                    stock = ServiceHelper.ws.GetStockByProductID(product.Id).Last();
                    subtotal = product.price * Convert.ToInt32(txQuantity.Text);
                    profit = subtotal * Convert.ToDecimal(0.20);

                    if (txDiscount.Text != string.Empty)
                    {
                        subtotal -= Convert.ToDecimal(txDiscount.Text);
                        profit = subtotal * Convert.ToDecimal(0.20);
                    }

                    stock.quantity = stock.quantity - Convert.ToInt32(txQuantity.Text);
                    if (stock.quantity < 0)
                    {
                        lbMsg.Text = $"El Stock del Producto: {product.description} no puede ser menor a 0.";
                        return;
                    }
                    else
                    {
                        stock = ServiceHelper.ws.SetStock(stock);
                    }
                }
                else
                {
                    promotion = ServiceHelper.ws.GetPromotionById(Convert.ToInt32(ddPromotions.SelectedValue));
                    List<Promotions_ProductsDTO> productsPromo = ServiceHelper.ws.GetProductsPromoByIdPromo(promotion.Id).ToList();
                    List<StockDTO> stockProducts = new List<StockDTO>();

                    foreach (var productPromo in productsPromo)
                    {
                        ProductsDTO producto = ServiceHelper.ws.GetProductByID(Convert.ToInt32(productPromo.idProduct));
                        StockDTO stockProduct = ServiceHelper.ws.GetStockByProductID(producto.Id).Last();

                        stockProduct.quantity -= (productPromo.quantity * Convert.ToInt32(txQuantity.Text));

                        if (stockProduct.quantity < 0)
                        {
                            lbMsg.Text = $"El Stock del Producto: {producto.description} no puede ser menor a 0.";

                            ddPromotions.Items.FindByText("Seleccione").Selected = true;
                            ddPromotions.Items.FindByText("Seleccione").Selected = true;
                            txQuantity.Text = string.Empty;

                            return;
                        }
                        else
                        {
                            stockProducts.Add(stock);
                        }
                    }

                    subtotal = promotion.price * Convert.ToInt32(txQuantity.Text);
                    profit = subtotal * Convert.ToDecimal(0.20);

                }

                DataTable products = (DataTable)ViewState["dtProducts"];
                if (products == null)
                {
                    products = new DataTable();
                    products.Columns.Add("Id", typeof(Int32));
                    products.Columns.Add("product_id", typeof(Int32));
                    products.Columns.Add("promotion_id", typeof(Int32));
                    products.Columns.Add("brand_id", typeof(Int32));
                    products.Columns.Add("description", typeof(string));
                    products.Columns.Add("price", typeof(decimal));
                    products.Columns.Add("product_type", typeof(string));
                    products.Columns.Add("quantity", typeof(int));
                    products.Columns.Add("subtotal", typeof(decimal));
                    products.Columns.Add("profit", typeof(decimal));

                    if (product.Id > 0)
                    {
                        DataRow dr = products.Rows.Add(products.Rows.Count + 1, product.Id, 0,  Convert.ToInt32(ddBrand.SelectedValue), product.description, product.price, ServiceHelper.ws.GetProductTypeByID(product.product_type_id).description,
                        Convert.ToInt32(txQuantity.Text), subtotal, profit);
                    }
                    else
                    {
                        DataRow dr = products.Rows.Add(products.Rows.Count + 1, 0, promotion.Id, Convert.ToInt32(ddBrand.SelectedValue), product.description, product.price, "PROMOCIÓN",
                        Convert.ToInt32(txQuantity.Text), subtotal, profit);
                    }
                }
                else
                {
                    if (product.Id > 0)
                    {
                        DataRow dr = products.Rows.Add(products.Rows.Count + 1, product.Id, 0, Convert.ToInt32(ddBrand.SelectedValue), product.description, product.price, ServiceHelper.ws.GetProductTypeByID(product.product_type_id).description,
                        Convert.ToInt32(txQuantity.Text), subtotal, profit);
                    }
                    else
                    {
                        DataRow dr = products.Rows.Add(products.Rows.Count + 1, 0, promotion.Id, Convert.ToInt32(ddBrand.SelectedValue), product.description, product.price, "PROMOCIÓN",
                        Convert.ToInt32(txQuantity.Text), subtotal, profit);
                    }
                }

                ViewState["dtProducts"] = products;

                gvProductos.DataSource = products;
                gvProductos.DataBind();

                LoadData();


                //DataTable products = (DataTable)ViewState["dtProducts"];
                //if (products != null)
                //{            
                //    DataRow dr = products.Rows.Add(products.Rows.Count + 1,product.Id,Convert.ToInt32(ddBrand.SelectedValue), product.description, product.price, ServiceHelper.ws.GetProductTypeByID(product.product_type_id).description,
                //        Convert.ToInt32(txQuantity.Text), subtotal, profit);
                //}
                //else
                //{
                //    products = new DataTable();
                //    products.Columns.Add("Id", typeof(Int32));
                //    products.Columns.Add("product_id", typeof(Int32));
                //    products.Columns.Add("brand_id", typeof(Int32));
                //    products.Columns.Add("description", typeof(string));
                //    products.Columns.Add("price", typeof(decimal));
                //    products.Columns.Add("product_type", typeof(string));
                //    products.Columns.Add("quantity", typeof(int));
                //    products.Columns.Add("subtotal", typeof(decimal));
                //    products.Columns.Add("profit", typeof(decimal));

                //    DataRow dr = products.Rows.Add(products.Rows.Count + 1, product.Id,Convert.ToInt32(ddBrand.SelectedValue), product.description, product.price, ServiceHelper.ws.GetProductTypeByID(product.product_type_id).description,
                //        Convert.ToInt32(txQuantity.Text), subtotal, profit);
                //}


            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btConfirmSale_Click(object sender, EventArgs e)
        {
            try
            {
                //SalesDTO sale = new SalesDTO();
                //decimal total = 0;
                //decimal profit = 0;
                //DataTable products = (DataTable)ViewState["dtProducts"];
                //if (products != null)
                //{
                //    EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                //    ViewState["Event"] = lastEvent;

                //    if (lastEvent.state != "EN CURSO")
                //    {
                //        lbMsg.Text = "No puede cargar ventas si no hay eventos en curso.";
                //        btAddProduct.Enabled = false;
                //        btConfirmSale.Enabled = false;
                //        products = null;
                //        ViewState["dtProducts"] = products;
                //        LoadData();
                //        return;
                //    }
                //    sale.state = "CONFIRMADA";
                //    sale.date = DateTime.Today;
                //    sale.event_id = lastEvent.Id;

                //    ServiceHelper.ws.SetSale(sale);

                //    SalesDTO lastSale = ServiceHelper.ws.GetSales().Last();

                //    foreach (DataRow row in products.Rows)
                //    {
                //        Sales_detailsDTO sale_detail = new Sales_detailsDTO();

                //        sale_detail.product_id = Convert.ToInt32(row["product_id"].ToString());
                //        sale_detail.brand_id = Convert.ToInt32(row["brand_id"].ToString());
                //        sale_detail.profit = Convert.ToDecimal(row["profit"].ToString());
                //        sale_detail.quantity = Convert.ToInt32(row["quantity"].ToString());
                //        sale_detail.sale_id = lastSale.Id;
                //        sale_detail.total = Convert.ToDecimal(row["subtotal"].ToString());

                //        total += Convert.ToDecimal(row["subtotal"].ToString());
                //        profit += Convert.ToDecimal(row["profit"].ToString());


                //        ServiceHelper.ws.SetSaleDetail(sale_detail);
                //    }

                //    lastSale.total = total;
                //    lastSale.profit = profit;

                //    ServiceHelper.ws.SetSale(lastSale);

                DataTable products = (DataTable)ViewState["dtProducts"];
                if (products != null)
                {
                    if (products.Rows.Count > 0)
                    {
                        decimal total = 0;

                        foreach (DataRow dr in products.Rows)
                        {
                            total = total + Convert.ToDecimal(dr["subtotal"]);
                        }


                            txAmount.Text = total.ToString();
                            txAmount1.Text = total.ToString();

                    }
                }

                btAddProduct.Enabled = false;
                btConfirmSale.Enabled = false;

                plFormasPago.Visible = true;
                //    //lbMsg.Text = "Venta creada con éxito.";

                //    //Response.Redirect("Sales.aspx");
                //}
                //else
                //{
                //    lbMsg.Text = "Debe añadir productos para crear una venta.";
                //    return;
                //}
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ddBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {               
                if (ddBrand.SelectedValue != "Seleccione")
                {
                    List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(Convert.ToInt32(ddBrand.SelectedValue)).ToList();
                    List<StockDTO> stock = ServiceHelper.ws.GetStockByBrandID(Convert.ToInt32(ddBrand.SelectedValue)).ToList();
                    EventsDTO events = ServiceHelper.ws.GetEvents().Last();
                    ViewState["products"] = products;
                    if (products != null && stock != null)
                    {
                        List<ProductsDTO> productsFiltered = new List<ProductsDTO>();
                        foreach (var product in products)
                        {
                            if (stock.Where(x => x.event_id == events.Id).Any(x => x.product_id == product.Id))
                            {
                                productsFiltered.Add(product);
                            }
                        }
                        var datasource = from product in productsFiltered
                                         select new 
                                         {
                                             product.Id,
                                             product.serial_number,
                                             product.description,
                                             DisplayField = String.Format("{0} - {1} - Cant: {2}", product.serial_number, product.description, stock.Where(x => x.event_id == events.Id).First(x => x.product_id == product.Id).quantity)
                                         };
                        ddProducts.DataSource = datasource;
                        ddProducts.DataTextField = "DisplayField";
                        ddProducts.DataValueField = "Id";
                        ddProducts.DataBind();
                        ddProducts.Items.Add("Seleccione");
                        ddProducts.Items.FindByText("Seleccione").Selected = true;

                    }

                    List<PromotionsDTO> promotions = ServiceHelper.ws.GetPromotionsByIdBrand(Convert.ToInt32(ddBrand.SelectedValue)).ToList();
                    ddPromotions.DataSource = promotions;
                    ddPromotions.DataTextField = "Description";
                    ddPromotions.DataValueField = "Id";
                    ddPromotions.DataBind();
                    ddPromotions.Items.Add("Seleccione");
                    ddPromotions.Items.FindByText("Seleccione").Selected = true;

                }
                else
                {
                    ddProducts.Items.Clear();
                    ddProducts.Items.Add("SELECCIONE MARCA");
                    ddProducts.Items.FindByText("SELECCIONE MARCA").Selected = true;

                    ddPromotions.Items.Clear();
                    ddPromotions.Items.Add("SELECCIONE MARCA");
                    ddPromotions.Items.FindByText("SELECCIONE MARCA").Selected = true;
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void txProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<ProductsDTO> products = (List<ProductsDTO>)ViewState["products"];
                if (!string.IsNullOrEmpty(txProduct.Text))
                {
                    var productsFilter = products.Where(x => x.serial_number.Contains(txProduct.Text) || x.description.Contains(txProduct.Text)).ToList();

                    if (productsFilter != null)
                    {
                        var datasource = from product in productsFilter
                                         select new
                                         {
                                             product.Id,
                                             product.serial_number,
                                             product.description,
                                             DisplayField = String.Format("{0} - {1} - Cant: {2}", product.serial_number, product.description, ServiceHelper.ws.GetStockByProductID(product.Id).First().quantity)
                                         };
                        ddProducts.DataSource = datasource;
                        ddProducts.DataTextField = "DisplayField";
                        ddProducts.DataValueField = "Id";
                        ddProducts.DataBind();
                        ddProducts.Items.Add("Seleccione");
                        ddProducts.Items.FindByText("Seleccione").Selected = true;
                    }
                }
                else
                {
                    var datasource = from product in products
                                     select new
                                     {
                                         product.Id,
                                         product.serial_number,
                                         product.description,
                                         DisplayField = String.Format("{0} - {1} - Cant: {2}", product.serial_number, product.description, ServiceHelper.ws.GetStockByProductID(product.Id).First().quantity)
                                     };
                    ddProducts.DataSource = datasource;
                    ddProducts.DataTextField = "DisplayField";
                    ddProducts.DataValueField = "Id";
                    ddProducts.DataBind();
                    ddProducts.Items.Add("Seleccione");
                    ddProducts.Items.FindByText("Seleccione").Selected = true;
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = e.RowIndex + 1;
                DataTable products = (DataTable)ViewState["dtProducts"];

                DataRow dr = products.Rows[e.RowIndex];

                //products.Columns.Add("product_id", typeof(Int32));
                //products.Columns.Add("promotion_id", typeof(Int32));

                if (Convert.ToInt32(dr["product_id"]) != 0)
                {
                    StockDTO stock = ServiceHelper.ws.GetStockByProductID(Convert.ToInt32(dr["product_id"])).Last();
                    stock.quantity = stock.quantity + Convert.ToInt32(dr["quantity"].ToString());
                    ServiceHelper.ws.SetStock(stock);
                }
                else
                {
                    List<Promotions_ProductsDTO> productsPromo = ServiceHelper.ws.GetProductsPromoByIdPromo(Convert.ToInt32(dr["IdPromocion"])).ToList();

                    foreach (var productPromo in productsPromo)
                    {
                        StockDTO stock = ServiceHelper.ws.GetStockByProductID(productPromo.idProduct).Last();
                        stock.quantity = stock.quantity + (productPromo.quantity * Convert.ToInt32(dr["quantity"].ToString()));
                        ServiceHelper.ws.SetStock(stock);
                    }
                }

                products.Rows.Remove(dr);
                products.AcceptChanges();

                ViewState["dtProducts"] = products;

                LoadData();

                //DataRow[] dr = products.Select($"ID= {id}");
                //StockDTO stock = ServiceHelper.ws.GetStockByProductID(Convert.ToInt32(dr[0]["product_id"])).Last();
                //stock.quantity = stock.quantity + Convert.ToInt32(dr[0]["quantity"].ToString());
                //ServiceHelper.ws.SetStock(stock);

                //products.Rows.Remove(dr[0]);
                //products.AcceptChanges();

                //LoadData();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ddPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddPaymentType.SelectedItem.Text != "Mixta")
                {
                    plMixta.Visible = false;
                }
                else
                {
                    plMixta.Visible = true;

                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                SalesDTO sale = new SalesDTO();
                decimal total = 0;
                decimal profit = 0;
                DataTable products = (DataTable)ViewState["dtProducts"];
                if (products != null)
                {
                    EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                    ViewState["Event"] = lastEvent;

                    if (lastEvent.state != "EN CURSO")
                    {
                        lbMsg.Text = "No puede cargar ventas si no hay eventos en curso.";
                        btAddProduct.Enabled = false;
                        btConfirmSale.Enabled = false;
                        products = null;
                        ViewState["dtProducts"] = products;
                        LoadData();
                        return;
                    }
                    sale.state = "CONFIRMADA";
                    sale.date = DateTime.Today;
                    sale.event_id = lastEvent.Id;

                    ServiceHelper.ws.SetSale(sale);

                    SalesDTO lastSale = ServiceHelper.ws.GetSales().Last();

                    foreach (DataRow row in products.Rows)
                    {
                        Sales_detailsDTO sale_detail = new Sales_detailsDTO();

                        sale_detail.product_id = Convert.ToInt32(row["product_id"].ToString());
                        sale_detail.brand_id = Convert.ToInt32(row["brand_id"].ToString());
                        sale_detail.profit = Convert.ToDecimal(row["profit"].ToString());
                        sale_detail.quantity = Convert.ToInt32(row["quantity"].ToString());
                        sale_detail.sale_id = lastSale.Id;
                        sale_detail.total = Convert.ToDecimal(row["subtotal"].ToString());

                        total += Convert.ToDecimal(row["subtotal"].ToString());
                        profit += Convert.ToDecimal(row["profit"].ToString());


                        ServiceHelper.ws.SetSaleDetail(sale_detail);
                    }

                    lastSale.total = total;
                    lastSale.profit = profit;

                    if (ddPaymentType.SelectedItem.Text != "Mixta")
                    {
                        Sales_PaymentsDTO ventasFormasPago = new Sales_PaymentsDTO();

                        ventasFormasPago.IdPayments = Convert.ToInt32(ddPaymentType.SelectedValue);
                        ventasFormasPago.IdSale = lastSale.Id;
                        ventasFormasPago.amount = Convert.ToDecimal(txAmount.Text);

                        ServiceHelper.ws.SetSalePayment(ventasFormasPago);
                    }
                    else
                    {
                        Sales_PaymentsDTO ventasFormasPago = new Sales_PaymentsDTO();

                        ventasFormasPago.IdPayments = Convert.ToInt32(ddPaymentType1.SelectedValue);
                        ventasFormasPago.IdSale = lastSale.Id;
                        ventasFormasPago.amount = Convert.ToDecimal(txAmount1.Text);

                        ServiceHelper.ws.SetSalePayment(ventasFormasPago);

                        Sales_PaymentsDTO ventasFormasPago1 = new Sales_PaymentsDTO();

                        ventasFormasPago1.IdPayments = Convert.ToInt32(ddPaymentType2.SelectedValue);
                        ventasFormasPago1.IdSale = lastSale.Id;
                        ventasFormasPago1.amount = Convert.ToDecimal(txAmount2.Text);

                        ServiceHelper.ws.SetSalePayment(ventasFormasPago1);
                    }

                    lastSale.payment_method_id = Convert.ToInt32(ddPaymentType.SelectedValue);

                    if (lastSale.payment_method_id == 0)
                    {
                        lbMsg.Text = "Error en la forma de pago. Volver a confirmar.";
                        return;
                    }


                    ServiceHelper.ws.SetSale(lastSale);


                    Response.Redirect("/Pages/Sales.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();


                    //Response.Redirect("Sales.aspx");
                }
                else
                {
                    lbMsg.Text = "Debe añadir productos para crear una venta.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}