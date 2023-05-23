using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Artrash Web");
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {                    
                    var user = (UsersDTO)Session["usuario"];
                    if (user != null)
                    {
                        if (user.roleId == 1)
                        {
                            btPerfil.Visible = false;
                        }
                        else
                        {
                            btMarcas.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btProducts_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx");
        }

        protected void btSales_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sales.aspx");
        }

        protected void btEvents_Click(object sender, EventArgs e)
        {
            Response.Redirect("Events.aspx");
        }

        protected void btMetrics_Click(object sender, EventArgs e)
        {
            Response.Redirect("MetricsMenu.aspx");
        }

        protected void btMarcas_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListBrands.aspx");
        }

        protected void btPerfil_Click(object sender, EventArgs e)
        {
            var user = (UsersDTO)Session["usuario"];
            Response.Redirect("/Pages/BrandDetails.aspx?id=" + user.Id);
        }

        protected void btStock_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Pages/Stock.aspx");
        }
    }
}