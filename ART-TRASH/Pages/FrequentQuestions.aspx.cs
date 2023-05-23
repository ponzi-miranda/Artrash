using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtTrash.Pages
{
    public partial class FrequentQuestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)
                {
                    //
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btTienda_Click(object sender, EventArgs e)
        {
            try
            {
                lbAnswer.Text = string.Empty;
                lbAnswer.Text = "La Tiendita es un formado de feria SHOWROOM que se realiza un fin de semana completo una vez por mes," +
                "actualmente en plena Chacabuco, Nueva Córdoba. Es un formato creado a principio de la pandemia para seguir en movimiento" +
                "sin la necesidad de que cada marca o emprendimiento esté vendiendo en persona. Nosotrxs hacemos el armado del showroom con mobiliario, atendemos" +
                "y vendemos los producos de cada marca que participa. \n La tiendita abre un viernes y cierra un domingo. Usualmente los horarios de Tiendita son de 16 a 22hs. Pero pueden variar según el mes." +
                "Los productos se llevan un día antes de la apertura y el remanente de productos y dinero se retiran al cierre de la misma. \n" +
                "Para participar en la Tiendita los requisitos son los siguientes: \n" +
                "Las marcas de Indumentaria deben contar con perchero propio y en buen estado. Deben traer perchas identificadas," +
                "al igual que cada producto, con Precio y Codigo.";
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btComoInscribirse_Click(object sender, EventArgs e)
        {
            try
            {
                lbAnswer.Text = string.Empty;
                lbAnswer.Text = "Para participar de una edición de Art Trash Tendienta, te invitamos a seguir las redes oficiales para estar al tanto de las fechas." +
                    "El siguiente paso es comunicarse vía WhatsApp o Mensaje Directo, indicando en que evento se quiere participar y en el caso de haber cupo se contactaran con la Marca" +
                    "para coordinar precio de inscripción y Link del Sitio para registrarse.";
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btQueHacer_Click(object sender, EventArgs e)
        {
            try
            {
                lbAnswer.Text = string.Empty;
                lbAnswer.Text = "Una vez realizada la inscripción en un evento y registradas la marca en el sitio, deberá dar de Alta el detalle de todos " +
                    "sus productos. NOTA: LOS PRODUCTOS PODRÁN SER CARGADOS Y EDITADOS HASTA EL DÍA ANTERIOR DEL EVENTO.";
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btStock_Click(object sender, EventArgs e)
        {
            try
            {
                lbAnswer.Text = string.Empty;
                lbAnswer.Text = "Para cualquier modificación de Stock durante la ejecución de una Tiendita, debera ponerse en contacto con lxs organizadores para que " +
                    "actualizen el Stock de sus productos.";
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btSugerencia_Click(object sender, EventArgs e)
        {
            try
            {
                lbAnswer.Text = string.Empty;
                lbAnswer.Text = "Cualquier sugerencia sobre el sitio es bien recibida e invitamos a enviarlas a la siguiente dirección de correo: ponzimiranda@gmail.com";
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
    }
}