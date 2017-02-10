using liberacaopedidos.controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace liberacaopedidos
{
    public partial class contaclientes : System.Web.UI.Page
    {
        public String NumPedido;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var lstConta = new PedBlqService().ContaCorrenteClie(Request.QueryString["CodClie"].ToString());
                if (lstConta != null && lstConta.Count > 0)
                {
                    this.grdCConta.DataSource = lstConta;
                    this.grdCConta.DataBind();
                }
                NumPedido = Request.QueryString["NumPedido"].ToString();

                this.lblNumPedido.Text = NumPedido;

                CarregarTotaisCliente(Request.QueryString["CodClie"].ToString());
            }

        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            NumPedido = Request.QueryString["NumPedido"].ToString();
            Response.Write("<script>window.location.href='DadosPedidos.aspx?NumPedido=" + this.lblNumPedido.Text + "';</script>");
        }

        private void CarregarTotaisCliente(String CodClie)
        {
            var listaTotais = new PedBlqService().CarregaTotais(CodClie);
            if (listaTotais != null)
            {
                this.lblTotRecebido.Text = "R$ " + listaTotais.TotalRecebido;
                this.lblTotReceber.Text = "R$ " + listaTotais.TotalReceber;
                this.lblTotGeral.Text = "R$ " + listaTotais.TotalGeral;
                this.lblMedAtraso.Text = listaTotais.MediaDias;
                this.lblCodClie.Text = listaTotais.CodCliente;
                this.lblCliente.Text = listaTotais.NomeCliente;
            }
        }

    }
}