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
    public partial class EditProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Editar Producto");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        LoadData(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    else
                    {
                        Response.Redirect("Products.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void LoadData(int id)
        {
            EventsDTO lastEvent = ServiceHelper.ws.GetEvents().Last();
            ViewState["Event"] = lastEvent;


            if (lastEvent.state == "EN CURSO")
            {
                Response.Redirect("Products.aspx");
            }
            List<InscriptionsDTO> inscriptions = ServiceHelper.ws.GetInscriptionsByEventID(lastEvent.Id).ToList();
            var user = (UsersDTO)Session["usuario"];
            if (user != null)
            {
                var inscripted = inscriptions.Where(x => x.brand_id == user.Id);
                if (lastEvent.state == "PENDIENTE" && inscripted.Count() > 0)
                {
                }
                else
                {
                    lbMsg.Text = "Solo puede editar Stock si esta inscripto en un evento.";
                }
            }

            ddProductsType.DataSource = ServiceHelper.ws.GetProductTypes();
            ddProductsType.DataTextField = "description";
            ddProductsType.DataValueField = "Id";
            ddProductsType.DataBind();
            ddProductsType.Items.Add("Seleccione");
            ddProductsType.Items.FindByText("Seleccione").Selected = true;

            ProductsDTO product = ServiceHelper.ws.GetProductByID(id);
            if (product != null)
            {
                txDescription.Text = product.description;
                txPrice.Text = product.price.ToString();
                txSerialNumber.Text = product.serial_number;
                ddProductsType.SelectedValue = product.product_type_id.ToString();
                ddState.SelectedValue = product.state;

                if (product.state == "INACTIVO")
                {
                    txDescription.ReadOnly = true;
                    txPrice.ReadOnly = true;
                    txSerialNumber.ReadOnly = true;
                    ddProductsType.Enabled = false;
                }
            }
        }

        protected void btEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var user = (UsersDTO)Session["usuario"];
                if (user != null)
                {
                    ProductsDTO product = ServiceHelper.ws.GetProductByID(Convert.ToInt32(Request.QueryString["id"]));

                    product.state = "INACTIVO";

                    product = ServiceHelper.ws.SetProduct(product);

                    LoadData(Convert.ToInt32(Request.QueryString["id"]));
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }


        protected void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                var user = (UsersDTO)Session["usuario"];
                if (user != null)
                {
                    ProductsDTO product = ServiceHelper.ws.GetProductByID(Convert.ToInt32(Request.QueryString["id"]));

                    product.brand_id = user.Id;
                    product.description = txDescription.Text;
                    product.price = Convert.ToDecimal(txPrice.Text);
                    product.product_type_id = Convert.ToInt32(ddProductsType.SelectedValue);
                    product.serial_number = txSerialNumber.Text;
                    product.state = ddState.Text;

                    product = ServiceHelper.ws.SetProduct(product);


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