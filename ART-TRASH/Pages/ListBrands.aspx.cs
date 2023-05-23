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
    public partial class ListBrands : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Lista Marcas");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    var user = (UsersDTO)Session["usuario"];
                    if (user.roleId != 2)
                    {
                        List <UsersDTO> brands = ServiceHelper.ws.GetBrands().Where(x => x.active == true).ToList();
                        ViewState["Marcas"] = brands;
                        gvMarcas.DataSource = ViewState["Marcas"];
                        gvMarcas.DataBind();
                    }
                    else
                    {
                        Response.Redirect("Menu.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvMarcas.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

                if (e.CommandName == "Detalles")
                {
                    Response.Redirect("/Pages/BrandDetails.aspx?id=" + id.ToString());
                }
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
                List<UsersDTO> brands = (List<UsersDTO>)ViewState["Marcas"];
                if (brands != null)
                {
                    var brand = brands.Where(x => x.name.Contains(txBrand.Text));

                    gvMarcas.DataSource = brand;
                    gvMarcas.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}