using liberacaopedidos.controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace liberacaopedidos
{
    public partial class pedidosbloqueados : System.Web.UI.Page
    {
        int CodUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var listaPedidos = new PedBlqService().CarregarPedidos(0);
                if (listaPedidos != null && listaPedidos.Count > 0)
                {
                    this.grdDados.DataSource = listaPedidos;
                    this.grdDados.DataBind();
                }

                lblCodUsuario.Text = Request.QueryString["Id"];


            }
        }
        protected void grdDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                string idPedido = e.CommandArgument.ToString();
                if (!String.IsNullOrEmpty(idPedido))
                    this.Response.Redirect("DadosPedidos.aspx?NumPedido=" + idPedido + "&Id=" + lblCodUsuario.Text);
            }
        }


    }
}