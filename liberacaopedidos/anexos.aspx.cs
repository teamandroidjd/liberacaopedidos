using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace liberacaopedidos
{
    public partial class anexos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            
            Response.Write("<script>window.location.href='DadosPedidos.aspx?NumPedido=" + 10569 + "';</script>");
        }
    }
}