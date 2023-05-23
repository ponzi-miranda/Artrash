
using ArtTrash.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class MetricsMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((Site)this.Master).SetTitle("Gráficos");
                lbMsg.Text = string.Empty;
                UsersDTO user = (UsersDTO)Session["usuario"];
                if (!Page.IsPostBack)
                {
                    if (user.roleId != 1)//es brand
                    {
                        plBrands.Visible = true;
                        plAdmin.Visible = false;
                    }
                    else
                    {
                        plBrands.Visible = false;
                        plAdmin.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ventasPorEventos_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/");
            }
            catch(Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ventasPorCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("PercentageProdutTypeSales.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void ventasPorProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("BrandProductSales.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void rentabilidadPorMarca_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("PercentageProfitMetrics.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
            //PercentageProfitMetrics.aspx
        }

        protected void ventasPorMarca_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("PercentageSalesMetrics.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
            //PercentageSalesMetrics.aspx
        }

        protected void rentabilidadPorEventos_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("TotalEventMetrics.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
            //PercentageProfitMetrics.aspx
        }

        protected void marcasPorEventos_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("TotalBrandsMetrics.aspx");
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
            //TotalBrandsMetrics.aspx
        }
    }
}