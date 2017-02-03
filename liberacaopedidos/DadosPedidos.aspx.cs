using liberacaopedidos.controles;
using liberacaopedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace liberacaopedidos
{
    public partial class DadosPedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["NumPedido"] != null)
                    this.CarregarDadosPedido(Convert.ToInt32(Request.QueryString["NumPedido"]));
            }
        }

        private void CarregarDadosPedido(int NumPedido)
        {

            var listaPedidos = new PedBlqService().CarregaDadosPedidos(NumPedido);
            if (listaPedidos != null)
            {
                this.lblNumPedido.Text  = listaPedidos.NumPedido;
                this.lblTotal.Text      = listaPedidos.ValorTotal;                

            }
        }
    }
}