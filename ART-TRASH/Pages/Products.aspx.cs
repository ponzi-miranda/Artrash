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
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                ((Site)this.Master).SetTitle("Productos");
                if (!Page.IsPostBack)
                {
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
            UsersDTO user = (UsersDTO)Session["usuario"];
            List<Product_typeDTO> productTypes = ServiceHelper.ws.GetProductTypes().ToList();
            List<UsersDTO> brands = ServiceHelper.ws.GetBrands().ToList();
            if (user != null && user.roleId == 2)
            {
                var products = ServiceHelper.ws.GetProductsByBrandID(user.Id);
                List<Product> productsList = new List<Product>();
                foreach (var product in products)
                {
                    Product p = new Product();
                    p.brand_id = product.brand_id;
                    p.description = product.description;
                    p.Id = product.Id;
                    p.product_type = productTypes.FirstOrDefault(x => x.Id == product.product_type_id).description;
                    p.product_type_id = product.product_type_id;
                    p.serial_number = product.serial_number;
                    p.state = product.state;
                    p.price = product.price;
                    p.type = "Producto";

                    productsList.Add(p);
                }

                //var promotions = ServiceHelper.ws.GetPromotionsByIdBrand(user.Id);
                //foreach (var promotion in promotions)
                //{
                //    Product p = new Product();
                //    p.brand_id = promotion.idBrand;
                //    p.description = promotion.description;
                //    p.Id = promotion.Id;
                //    p.product_type = "PROMOCION";
                //    p.product_type_id = 0;
                //    p.serial_number = "PROMO";
                //    p.state = promotion.state;
                //    p.price = promotion.price;
                //    p.type = "Promocion";

                //    productsList.Add(p);
                //}


                ViewState["Products"] = productsList;
                gvProductos.DataSource = productsList.Where(x => x.state == "ACTIVO").OrderBy(x => x.brand);
                gvProductos.DataBind();

                ddProductsType.DataSource = productTypes;
                ddProductsType.DataTextField = "description";
                ddProductsType.DataValueField = "Id";
                ddProductsType.DataBind();
                ddProductsType.Items.Add("TODAS");
                ddProductsType.Items.Add("PROMOCION");
                ddProductsType.Items.FindByText("TODAS").Selected = true;

                plAdminFilters.Visible = false;
                gvProductosAdmin.Visible = false;
            }
            else if (user != null && user.roleId == 1)
            {
                var products = ServiceHelper.ws.GetProducts();
                List<Product> productsList = new List<Product>();
                foreach (var product in products)
                {
                    Product p = new Product();
                    p.brand_id = product.brand_id;
                    p.description = product.description;
                    p.Id = product.Id;
                    p.product_type = productTypes.FirstOrDefault(x => x.Id == product.product_type_id).description;
                    p.product_type_id = product.product_type_id;
                    p.serial_number = product.serial_number;
                    p.state = product.state;
                    p.price = product.price;
                    p.brand = brands.FirstOrDefault(x => x.Id == product.brand_id).name;
                    p.type = "Producto";

                    productsList.Add(p);
                }

                //var promotions = ServiceHelper.ws.GetPromotions();
                //foreach (var promotion in promotions)
                //{
                //    Product p = new Product();
                //    p.brand_id = promotion.idBrand;
                //    p.description = promotion.description;
                //    p.Id = promotion.Id;
                //    p.product_type = "PROMOCION";
                //    p.product_type_id = 0;
                //    p.serial_number = "PROMO";
                //    p.state = promotion.state;
                //    p.price = promotion.price;
                //    p.type = "Promocion";

                //    productsList.Add(p);
                //}

                ViewState["ProductsAdmin"] = productsList;
                gvProductosAdmin.DataSource = productsList.Where(x => x.state == "ACTIVO").OrderBy(x => x.brand);
                gvProductosAdmin.DataBind();

                ddProductsTypeAdmin.DataSource = productTypes;
                ddProductsTypeAdmin.DataTextField = "description";
                ddProductsTypeAdmin.DataValueField = "Id";
                ddProductsTypeAdmin.DataBind();
                ddProductsTypeAdmin.Items.Add("TODAS");
                ddProductsTypeAdmin.Items.Add("PROMOCION");
                ddProductsTypeAdmin.Items.FindByText("TODAS").Selected = true;

                ddBrands.DataSource = brands.OrderBy(x => x.name);
                ddBrands.DataTextField = "name";
                ddBrands.DataValueField = "Id";
                ddBrands.DataBind();
                ddBrands.Items.Add("TODAS");
                ddBrands.Items.FindByText("TODAS").Selected = true;

                plBrandFilters.Visible = false;
                gvProductos.Visible = false;
            }
        }

        protected void btNewProduct_Click(object sender, EventArgs e)
        {
            try
            {
                var user = (UsersDTO)Session["usuario"];

                if (user != null) 
                {
                    if (user.roleId == 2)
                    {
                        Response.Redirect("NewProduct.aspx");
                    }
                    else
                    {
                        Response.Redirect("AddNewProduct.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvProductos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);
                string type = (string)gvProductos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1];



                EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(lastEvent.Id).ToList();
                var user = (UsersDTO)Session["usuario"];

                if (user != null)
                {
                    var inscripted = inscriptions.Where(x => x.brand_id == user.Id);

                    if (lastEvent.state == "EN CURSO")
                    {
                        lbMsg.Text = "Evento en curso, no puede editar sus productos.";
                    }
                    else if (type == "Producto")
                    {
                        Response.Redirect("/Pages/EditProduct.aspx?id=" + id.ToString());
                    }
                    else
                    {
                        Response.Redirect("/Pages/EditPromotion.aspx?id=" + id.ToString());
                    }
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
                List<Product> productsList = (List<Product>)ViewState["Products"];
                List<Product> productsFiltered = new List<Product>();
                if (productsList != null)
                {
                    if (txNombreProducto.Text != string.Empty)
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.description.Contains(txNombreProducto.Text)).ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.description.Contains(txNombreProducto.Text)).ToList();
                        }
                    }
                    if (txCodigoProducto.Text != string.Empty)
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.serial_number.Contains(txCodigoProducto.Text)).ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.serial_number.Contains(txCodigoProducto.Text)).ToList();
                        }
                    }                    
                    if (ddProductsType.SelectedValue != "TODAS")
                    {
                        if (ddProductsType.SelectedItem.Text != "PROMOCION")
                        {
                            if (productsFiltered.Count() > 0)
                            {
                                productsFiltered = productsFiltered.Where(x => x.product_type_id == Convert.ToInt32(ddProductsTypeAdmin.SelectedValue)).ToList();
                            }
                            else
                            {
                                productsFiltered = productsList.Where(x => x.product_type_id == Convert.ToInt32(ddProductsTypeAdmin.SelectedValue)).ToList();
                            }
                        }
                        else
                        {
                            if (productsFiltered.Count() > 0)
                            {
                                productsFiltered = productsFiltered.Where(x => x.type == "Promocion").ToList();
                            }
                            else
                            {
                                productsFiltered = productsList.Where(x => x.type == "Promocion").ToList();
                            }
                        }
                    }
                    if (ddState.SelectedValue != "ACTIVO")
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.state == "INACTIVO").ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.state == "INACTIVO").ToList();
                        }
                    }
                    if (txNombreProducto.Text == string.Empty && txCodigoProducto.Text == string.Empty && ddProductsType.SelectedValue == "TODAS" && ddState.SelectedValue == "ACTIVO")
                    {
                        productsFiltered = productsList;
                    }
                    gvProductos.DataSource = productsFiltered;
                    gvProductos.DataBind();
                }
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
                List<Product> productsList = (List<Product>)ViewState["ProductsAdmin"];
                List<Product> productsFiltered = new List<Product>();
                if (productsList != null)
                {
                    if (txNombreProductoAdmin.Text != string.Empty)
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.description.Contains(txNombreProductoAdmin.Text)).ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.description.Contains(txNombreProductoAdmin.Text)).ToList();
                        }
                    }
                    if (txCodigoProductoAdmin.Text != string.Empty)
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.serial_number.Contains(txCodigoProductoAdmin.Text)).ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.serial_number.Contains(txCodigoProductoAdmin.Text)).ToList();
                        }
                    }
                    if (ddProductsTypeAdmin.SelectedValue != "TODAS")
                    {
                        if (ddProductsTypeAdmin.SelectedItem.Text != "PROMOCION")
                        {
                            if (productsFiltered.Count() > 0)
                            {
                                productsFiltered = productsFiltered.Where(x => x.product_type_id == Convert.ToInt32(ddProductsTypeAdmin.SelectedValue)).ToList();
                            }
                            else
                            {
                                productsFiltered = productsList.Where(x => x.product_type_id == Convert.ToInt32(ddProductsTypeAdmin.SelectedValue)).ToList();
                            }
                        }
                        else
                        {
                            if (productsFiltered.Count() > 0)
                            {
                                productsFiltered = productsFiltered.Where(x => x.type == "Promocion").ToList();
                            }
                            else
                            {
                                productsFiltered = productsList.Where(x => x.type == "Promocion").ToList();
                            }
                        }
                        
                    }
                    if (ddBrands.SelectedValue != "TODAS")
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.brand_id == Convert.ToInt32(ddBrands.SelectedValue)).ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.brand_id == Convert.ToInt32(ddBrands.SelectedValue)).ToList();
                        }
                    }
                    if (ddStateAdmin.SelectedValue != "ACTIVO")
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.state == "INACTIVO").ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.state == "INACTIVO").ToList();
                        }
                    }
                    else
                    {
                        if (productsFiltered.Count() > 0)
                        {
                            productsFiltered = productsFiltered.Where(x => x.state == "ACTIVO").ToList();
                        }
                        else
                        {
                            productsFiltered = productsList.Where(x => x.state == "ACTIVO").ToList();
                        }
                    }
                    if (txNombreProductoAdmin.Text == string.Empty && txCodigoProductoAdmin.Text == string.Empty && ddProductsTypeAdmin.SelectedValue == "TODAS" && ddStateAdmin.SelectedValue == "ACTIVO" && ddBrands.SelectedValue == "TODAS")
                    {
                        productsFiltered = productsList;
                    }

                    gvProductosAdmin.DataSource = productsFiltered;
                    gvProductosAdmin.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvProductosAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                int id = Convert.ToInt32(gvProductosAdmin.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);
                string type = (string)gvProductosAdmin.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1];



                EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(lastEvent.Id).ToList();
                var user = (UsersDTO)Session["usuario"];

                if (user != null)
                {
                    if (user != null)
                    {
                        if (user.roleId == 1)
                        {
                            if (type == "Producto")
                            {
                                Response.Redirect("/Pages/AdminEditProduct.aspx?id=" + id.ToString());
                            }
                            else
                            {
                                Response.Redirect("/Pages/AdminEditPromotion.aspx?id=" + id.ToString());
                            }
                        }
                    }
                }


                //int id = Convert.ToInt32(gvProductosAdmin.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                //EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
                //var user = (UsersDTO)Session["usuario"];

                //if (user != null)
                //{
                //    if (user.roleId == 1)
                //    {
                //        Response.Redirect("/Pages/AdminEditProduct.aspx?id=" + id.ToString());
                //    }

                //}
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btNewPromotion_Click(object sender, EventArgs e)
        {
            try
            {
                var user = (UsersDTO)Session["usuario"];

                if (user != null)
                {
                    if (user.roleId == 2)
                    {
                        Response.Redirect("NewPromotion.aspx");
                    }
                    else
                    {
                      //  Response.Redirect("AdminNewPromotion.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                UsersDTO user = (UsersDTO)Session["usuario"];
                if (user != null && user.roleId == 2)
                {
                    List<Product> productsList = (List<Product>)ViewState["Products"];
                    foreach (var product in productsList)
                    {
                        ProductsDTO productItem = new ProductsDTO();
                        productItem.Id = product.Id;
                        productItem.state = "INACTIVO";
                        productItem.brand_id = product.brand_id;
                        productItem.description = product.description;
                        productItem.price = product.price;
                        productItem.product_type_id = product.product_type_id;
                        productItem.serial_number = product.serial_number;

                        ServiceHelper.ws.SetProduct(productItem);
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