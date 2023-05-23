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
    public partial class AddNewProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Alta Producto");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    var user = (UsersDTO)Session["usuario"];
                    if (user != null && user.roleId != 1)
                    {
                        Response.Redirect("Products.aspx");
                    }

                    ddBrands.DataSource = ServiceHelper.ws.GetBrands();
                    ddBrands.DataTextField = "name";
                    ddBrands.DataValueField = "Id";
                    ddBrands.DataBind();
                    ddBrands.Items.Add("Seleccione");
                    ddBrands.Items.FindByText("Seleccione").Selected = true;

                    ddProductsType.DataSource = ServiceHelper.ws.GetProductTypes();
                    ddProductsType.DataTextField = "description";
                    ddProductsType.DataValueField = "Id";
                    ddProductsType.DataBind();
                    ddProductsType.Items.Add("Seleccione");
                    ddProductsType.Items.FindByText("Seleccione").Selected = true;
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btCrear_Click(object sender, EventArgs e)
        {
            try
            {
                var user = (UsersDTO)Session["usuario"];
                if (user != null)
                {
                    ProductsDTO product = new ProductsDTO();

                    product.brand_id = Convert.ToInt32(ddBrands.SelectedValue);
                    product.description = txDescription.Text;
                    product.price = Convert.ToDecimal(txPrice.Text);
                    product.product_type_id = Convert.ToInt32(ddProductsType.SelectedValue);
                    product.serial_number = txSerialNumber.Text;
                    product.state = "ACTIVO";

                    ServiceHelper.ws.SetProduct(product);

                    Response.Redirect("Products.aspx");
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}