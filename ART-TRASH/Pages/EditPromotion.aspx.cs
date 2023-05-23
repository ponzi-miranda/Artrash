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
    public partial class EditPromotion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        UsersDTO user = (UsersDTO)Session["usuario"];
                        List<ProductsDTO> products = ServiceHelper.ws.GetProductsByBrandID(user.Id).ToList();

                        ddProducts.DataSource = products;
                        ddProducts.DataTextField = "description";
                        ddProducts.DataValueField = "Id";
                        ddProducts.DataBind();
                        ddProducts.Items.Add("Seleccione");
                        ddProducts.Items.FindByText("Seleccione").Selected = true;

                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        PromotionsDTO promotion = ServiceHelper.ws.GetPromotionById(id);

                        txDescription.Text = promotion.description;
                        txPrice.Text = promotion.price.ToString();

                        List<PromoModel> promoProducts = new List<PromoModel>();
                        List<Promotions_ProductsDTO> productsPromo = ServiceHelper.ws.GetProductsPromoByIdPromo(id).ToList();
                        foreach (var product in productsPromo)
                        {
                            PromoModel promo = new PromoModel();

                            promo.quantity = product.quantity;
                            promo.Id = product.idProduct;
                            promo.description = products.FirstOrDefault(x => x.Id == product.idProduct).description;

                            promoProducts.Add(promo);
                        }

                        ViewState["products"] = promoProducts;
                        gvProducts.SelectedIndex = 0;

                    }
                    else
                    {
                        Response.Redirect("/Pages/Products.aspx");
                    }
                }

                UpdateGrid();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void UpdateGrid()
        {
            List<PromoModel> list = (List<PromoModel>)ViewState["products"];
            gvProducts.DataSource = list;
            gvProducts.DataBind();
            gvProducts.HeaderRow.RowType = DataControlRowType.Header;
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<PromoModel> products = (List<PromoModel>)ViewState["products"];
                PromoModel promo = new PromoModel();

                if (gvProducts.SelectedIndex != 0)
                    promo = products.Find(x => x.Id == gvProducts.SelectedIndex);

                promo.quantity = Convert.ToInt32(txQuantity.Text);
                promo.Id = Convert.ToInt32(ddProducts.SelectedValue);
                promo.description = ddProducts.SelectedItem.Text;

                if (gvProducts.SelectedIndex == 0)
                    products.Add(promo);


                ViewState["products"] = products;

                UpdateGrid();

                

                gvProducts.SelectedIndex = 0;
                //txQuantity.Text = string.Empty;
                //ddProducts.Items.FindByText("Seleccione").Selected = true;
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


                UsersDTO user = (UsersDTO)Session["usuario"];
                PromotionsDTO promotion = new PromotionsDTO();
                promotion.Id = Convert.ToInt32(Request.QueryString["id"]);
                promotion.description = txDescription.Text;
                promotion.state = "ACTIVO";
                promotion.price = Convert.ToDecimal(txPrice.Text);
                promotion.idBrand = user.Id;

                promotion = ServiceHelper.ws.SetPromotion(promotion);

                List<PromoModel> products = (List<PromoModel>)ViewState["products"];

                ServiceHelper.ws.DeleteProductsPromoByIdPromo(promotion.Id);

                foreach (var product in products)
                {
                    Promotions_ProductsDTO productsPromo = new Promotions_ProductsDTO();

                    productsPromo.idProduct = product.Id;
                    productsPromo.idPromotion = ServiceHelper.ws.GetPromotions().LastOrDefault().Id;
                    productsPromo.quantity = product.quantity;

                    productsPromo = ServiceHelper.ws.SetProductsPromotions(productsPromo);
                }

                Response.Redirect("Products.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvProducts.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                List<PromoModel> products = (List<PromoModel>)ViewState["products"];

                if (e.CommandName == "Editar")
                {
                    gvProducts.SelectedIndex = id;

                    PromoModel promo = products.First(x => x.Id == id);

                    ddProducts.SelectedValue = promo.Id.ToString();

                    txQuantity.Text = promo.quantity.ToString();
                }
                if (e.CommandName == "Eliminar")
                {

                    products.Remove(products.First(x => x.Id == id));

                    ViewState["productos"] = products;

                    UpdateGrid();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}