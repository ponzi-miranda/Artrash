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
    public partial class NewPromotion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    UsersDTO user = (UsersDTO)Session["usuario"];
                    ddProducts.DataSource = ServiceHelper.ws.GetProductsByBrandID(user.Id);
                    ddProducts.DataTextField = "description";
                    ddProducts.DataValueField = "Id";
                    ddProducts.DataBind();
                    ddProducts.Items.Add("Seleccione");
                    ddProducts.Items.FindByText("Seleccione").Selected = true;

                    List<PromoModel> products = new List<PromoModel>();

                    ViewState["products"] = products;
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

                ProductsDTO product = ServiceHelper.ws.GetProductByID(Convert.ToInt32(ddProducts.SelectedValue));

                PromoModel promo = new PromoModel();
                promo.quantity = Convert.ToInt32(txQuantity.Text);
                promo.Id = product.Id;
                promo.description = product.description;

                products.Add(promo);

                ViewState["products"] = products;

                UpdateGrid();
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

                promotion.description = txDescription.Text;
                promotion.state = "ACTIVO";
                promotion.price = Convert.ToDecimal(txPrice.Text);
                promotion.idBrand = user.Id;

                promotion = ServiceHelper.ws.SetPromotion(promotion);

                List<PromoModel> products = (List<PromoModel>)ViewState["products"];

                foreach (var product in products)
                {
                    Promotions_ProductsDTO productPromo = new Promotions_ProductsDTO();

                    productPromo.idProduct = product.Id;
                    productPromo.idPromotion = ServiceHelper.ws.GetPromotions().LastOrDefault().Id;
                    productPromo.quantity = product.quantity;

                    productPromo = ServiceHelper.ws.SetProductsPromotions(productPromo);
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
                List<PromoModel> products = (List<PromoModel>)ViewState["products"];

                products.Remove(products.First(x => x.Id == Convert.ToInt32(gvProducts.DataKeys[Convert.ToInt32(e.CommandArgument)].Value)));

                ViewState["products"] = products;

                UpdateGrid();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}